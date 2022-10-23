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
        [TabGroup("Text")] [SerializeField] private TextMeshProUGUI timerText, scoreText, currentPoint;

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

        [SerializeField] private float rhythmForgiveness = .7f;

        private void Awake()
        {
            if (!songTemplate)
            {
                Debug.LogWarning($"No template added to {this}");
                return;
            }

            rhythmPoints = songTemplate.peakPoints;
        }

        // Start is called before the first frame update
        private void Start()
        {
            timer = 0;
            rhythmPointIndex = 0;
        }

        // Update is called once per frame
        private void Update()
        {
            SetTimer();
            SetText();
            CalculateDelay();
        }


        private void CalculateDelay()
        {
            if (timer >= rhythmPoints[rhythmPointIndex] + rhythmForgiveness) //when timer exceeds index go next
            {
                scoringEnabled = true;
                rhythmPointIndex++;
            }

            if (!Input.GetKeyDown(key)) return;
            if (rhythmPoints[rhythmPointIndex] > timer && scoringEnabled)
            {
                CalculateScore();
                scoringEnabled = false;
            }
        }


        private void CalculateScore()
        {
            float calculatedScore;
            if (rhythmPointIndex > 0)
            {
                // calculate score based on % of available score
                calculatedScore = baseScore - ((rhythmPoints[rhythmPointIndex] - timer) /
                    (rhythmPoints[rhythmPointIndex] - rhythmPoints[rhythmPointIndex - 1]) * baseScore);
                // base score - (( timer dif ) / ( max time ) * base score)
            }
            else //fix for index null on index = 0
            {
                calculatedScore = baseScore - ((rhythmPoints[rhythmPointIndex] - timer) /
                    (rhythmPoints[rhythmPointIndex]) * baseScore);
            }

            score += (int) calculatedScore;
        }

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