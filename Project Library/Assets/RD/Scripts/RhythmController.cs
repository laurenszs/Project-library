using System;
using System.Collections.Generic;
using RD.Scripts.Scriptable;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using static UnityEngine.Mathf;

namespace RD.Scripts
{
    public class RhythmController : MonoBehaviour
    {
        [TabGroup("Input")] [SerializeField] private KeyCode key;

        [TabGroup("TimerData")] [ReadOnly] [SerializeField]
        private float timer;

        [TabGroup("TimerData")] public SongTemplate songTemplate;

        [TabGroup("TimerData")] [SerializeField]
        private List<float> rhythmPoints;

        [TabGroup("TimerData")] [SerializeField]
        private int rhythmPointIndex;

        [TabGroup("TimerData")] [SerializeField]
        private float rhythmOvertime = .7f;

        [TabGroup("TimerData")] [SerializeField]
        private bool overtime;

        [TabGroup("Visual")] [SerializeField]
        public TextMeshProUGUI timerText, scoreText, currentPoint, delayText, delaySnapText, highScore;

        [TabGroup("Visual")] [SerializeField] private GameObject cube;
        [TabGroup("Visual")] [SerializeField] private Transform cubeContainer;

        private List<GameObject> _cubeList;
        private int _score;
        private bool _scoringEnabled = true;

        private void Awake()
        {
            if (!songTemplate)
            {
                Debug.LogWarning($"No template added to {this}");
                return;
            }

            AdjustPointsForDelay();
        }

        private void AdjustPointsForDelay()
        {
            foreach (var point in songTemplate.peakPoints)
            {
                rhythmPoints.Add(point + songTemplate.delay);
            }
        }

        private void Start()
        {
            timer = 0;
            rhythmPointIndex = 0;
            _cubeList = new List<GameObject>();
            RhythmVisualizer();
            highScore.text = songTemplate.highScore.ToString();
        }

        private void Update()
        {
            SetText();
            if (rhythmPointIndex < rhythmPoints.Count)
            {
                SetTimer();
                CalculateDelay();
            }

            SetHighScore();
            cubeContainer.localPosition =
                new Vector3(cubeContainer.localPosition.x, -timer);
        }

        public float RhythmPointIndexDifference()
        {
            float indexDiff;
            if (rhythmPointIndex > 1)
            {
                indexDiff = (rhythmPoints[rhythmPointIndex] - timer) /
                            (rhythmPoints[rhythmPointIndex] - rhythmPoints[rhythmPointIndex - 1]);
            }
            else
            {
                indexDiff = (rhythmPoints[rhythmPointIndex] - timer) / rhythmPoints[rhythmPointIndex];
            }


            return indexDiff;
        }

        private void CalculateDelay()
        {
            if (timer >= rhythmPoints[rhythmPointIndex] + rhythmOvertime) //when timer exceeds index go next
            {
                _scoringEnabled = true;
                rhythmPointIndex++;
            }


            // overtime = timer >= rhythmPoints[rhythmPointIndex] &&
            //            timer <= rhythmPoints[rhythmPointIndex] + rhythmOvertime;

            if (rhythmPointIndex > rhythmPoints.Count) return;
            if (!Input.GetKeyDown(key)) return;


            if (!(rhythmPoints[rhythmPointIndex] > timer) || !_scoringEnabled) return;
            CalculateScore();
            RhythmVisuals.instance.UpdateVisuals(_cubeList, rhythmPointIndex);
            _scoringEnabled = false;
            if (rhythmPointIndex <= rhythmPoints.Count) return;

            rhythmPointIndex = rhythmPoints.Count;
        }


        private void CalculateScore()
        {
            var calculatedScore = rhythmPointIndex switch
            {
                > 0 => GlobalValues.BaseScore - RhythmPointIndexDifference() * GlobalValues.BaseScore,
                _ => GlobalValues.BaseScore - (rhythmPoints[rhythmPointIndex] - timer) /
                    rhythmPoints[rhythmPointIndex] * GlobalValues.BaseScore
            };

            _score += (int) calculatedScore;
            delaySnapText.text = $"+ {RoundToInt(calculatedScore).ToString()}";
        }

        private void RhythmVisualizer()
        {
            foreach (var point in rhythmPoints)
            {
                var newCube = Instantiate(cube, cubeContainer);
                newCube.transform.localPosition = new Vector3(newCube.transform.localPosition.x, point);
                _cubeList.Add(newCube);
            }

            cubeContainer.localPosition = new Vector3(cubeContainer.localPosition.x, rhythmPoints[^1]);
        }

        private void SetTimer()
        {
            timer += Time.deltaTime;
        }

        private void SetHighScore()
        {
            if (rhythmPointIndex == rhythmPoints.Count)
            {
                if (songTemplate.highScore < _score)
                {
                    songTemplate.highScore = _score;
                    highScore.text = _score.ToString();
                }
            }
        }

        private void SetText()
        {
            scoreText.text = _score.ToString();
            delayText.text = (GlobalValues.BaseScore - RhythmPointIndexDifference() * GlobalValues.BaseScore)
                .ToString();
            var timeSpan = TimeSpan.FromSeconds(timer);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            //  currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
        }
    }
}