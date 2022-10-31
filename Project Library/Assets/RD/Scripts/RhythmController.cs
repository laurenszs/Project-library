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
        [TabGroup("Text")] [SerializeField] private TextMeshProUGUI timerText, scoreText, currentPoint, delayText;

        [TabGroup("TimerData")] [ReadOnly] [SerializeField]
        private float timer;

        [TabGroup("TimerData")] public SongTemplate songTemplate;

        [TabGroup("TimerData")] [SerializeField]
        private List<float> rhythmPoints;

        [TabGroup("TimerData")] [SerializeField]
        private int rhythmPointIndex;

        [TabGroup("Score")] [SerializeField] private int baseScore = 500, score;

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
            SetTimer();
            if (rhythmPointIndex <= rhythmPoints.Count)
            {
                SetText();
                CalculateDelay();
            }


            cubeContainer.localPosition =
                new Vector3(cubeContainer.localPosition.x, -timer);
        }


        private void CalculateDelay()
        {
            if (timer >= rhythmPoints[rhythmPointIndex] + rhythmOvertime) //when timer exceeds index go next
            {
                scoringEnabled = true;
                rhythmPointIndex++;
            }

            overtime = timer >= rhythmPoints[rhythmPointIndex] &&
                       timer <= rhythmPoints[rhythmPointIndex] + rhythmOvertime;

            if (rhythmPointIndex > rhythmPoints.Count) return;
            if (!Input.GetKeyDown(key)) return;

            DisplayDelay();
            if (!(rhythmPoints[rhythmPointIndex] > timer) || !scoringEnabled) return;
            CalculateScore();
            scoringEnabled = false;
        }


        private void CalculateScore()
        {
            var calculatedScore = rhythmPointIndex switch
            {
                > 0 => baseScore - RhythmPointIndexDifference() * baseScore,
                _ => baseScore - (rhythmPoints[rhythmPointIndex] - timer) / rhythmPoints[rhythmPointIndex] * baseScore
            };

            score += (int) calculatedScore;
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

        /// <summary>
        /// checks the difference between the current and previous (-1) index datapoint
        /// </summary>
        /// <returns></returns>
        private float RhythmPointIndexDifference()
        {
            var indexDiff = (rhythmPoints[rhythmPointIndex] - timer) /
                            (rhythmPoints[rhythmPointIndex] - rhythmPoints[rhythmPointIndex - 1]);
            return indexDiff;
        }

        private void DisplayDelay()
        {
            delayText.text = (RhythmPointIndexDifference()) switch
            {
                <= .25f => "Very early",
                < .75f => "Early",
                >= .75f => "Good",
                _ => delayText.text
            };
            foreach (var t in _cubeList)
            {
                t.GetComponent<MeshRenderer>().material.color = ( RhythmPointIndexDifference()) switch
                {
                    <= .25f => Color.red,
                    < .75f => Color.yellow,
                    >= .75f => Color.cyan,
                    _ => t.GetComponent<MeshRenderer>().material.color
                };

                if (overtime) t.GetComponent<MeshRenderer>().material.color = Color.black;
            }
        }

        // private void setDelayInfo(Color)
        // {
        //     
        // }

        private void SetTimer()
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString();
        }


        private void SetText()
        {
            scoreText.text = score.ToString();

            currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
        }
    }
}