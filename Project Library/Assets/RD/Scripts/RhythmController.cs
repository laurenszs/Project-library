using System;
using System.Collections.Generic;
using RD.Scripts.Scriptable;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace RD.Scripts
{
    public class RhythmController : MonoBehaviour
    {
        [TabGroup("Input")] [SerializeField] private KeyCode key;

        [TabGroup("Text")] [SerializeField]
        private TextMeshProUGUI timerText, scoreText, currentPoint, delayText, delaySnapText;

        [TabGroup("TimerData")] [ReadOnly] [SerializeField]
        private float timer;

        [TabGroup("TimerData")] public SongTemplate songTemplate;

        [TabGroup("TimerData")] [SerializeField]
        private List<float> rhythmPoints;

        [TabGroup("TimerData")] [SerializeField]
        private int rhythmPointIndex;

        [TabGroup("Score")] [SerializeField] private int score;

        [TabGroup("Score")] [SerializeField] [ReadOnly]
        private bool scoringEnabled = true;

        [SerializeField] private float rhythmOvertime = .7f;
        [SerializeField] private bool overtime;

        [SerializeField] private GameObject cube;
        [SerializeField] private Transform cubeContainer;

        private List<GameObject> _cubeList;


        private void Awake()
        {
            if (!songTemplate)
            {
                Debug.LogWarning($"No template added to {this}");
                return;
            }

            rhythmPoints = songTemplate.peakPoints;
        }

        private void Start()
        {
            timer = 0;
            rhythmPointIndex = 0;
            _cubeList = new List<GameObject>();
            RhythmVisualizer();
        }

        private void Update()
        {
            if (rhythmPointIndex < rhythmPoints.Count)
            {
                SetTimer();
                SetText();
                CalculateDelay();
            }

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
                scoringEnabled = true;
                rhythmPointIndex++;
            }


            // overtime = timer >= rhythmPoints[rhythmPointIndex] &&
            //            timer <= rhythmPoints[rhythmPointIndex] + rhythmOvertime;

            if (rhythmPointIndex > rhythmPoints.Count) return;
            if (!Input.GetKeyDown(key)) return;


            if (!(rhythmPoints[rhythmPointIndex] > timer) || !scoringEnabled) return;
            CalculateScore();
            RhythmVisuals.instance.UpdateVisuals(_cubeList, rhythmPointIndex);
            scoringEnabled = false;
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

            score += (int) calculatedScore;
            delaySnapText.text = $"+ {Mathf.RoundToInt(calculatedScore).ToString()}";
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


        private void SetText()
        {
            scoreText.text = score.ToString();
            delayText.text = (GlobalValues.BaseScore - RhythmPointIndexDifference() * GlobalValues.BaseScore)
                .ToString();
            var timeSpan = TimeSpan.FromSeconds(timer);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
        }
    }
}