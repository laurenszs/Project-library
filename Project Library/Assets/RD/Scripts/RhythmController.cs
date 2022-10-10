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
        [TabGroup("Text")] [SerializeField] private TextMeshProUGUI timerText;
        [TabGroup("Text")] [SerializeField] private TextMeshProUGUI scoreText;
        [TabGroup("Text")] [SerializeField] private TextMeshProUGUI currentPoint;

        [TabGroup("TimerData")] [SerializeField]
        private float scoreDelay = .3f;

        [TabGroup("TimerData")] [ReadOnly] [SerializeField]
        private float timer;

        [TabGroup("TimerData")] [ReadOnly] [SerializeField]
        private float delay;

        [TabGroup("TimerData")] public SongTemplate songTemplate;


        [TabGroup("TimerData")] [SerializeField]
        private List<float> rhythmPoints;

        [TabGroup("TimerData")] [SerializeField]
        private int rhythmPointIndex;

        [TabGroup("Score")] [ReadOnly] [SerializeField]
        private int score;

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
            SetScore();
            CalculateDelayScore();
        }


        private void CalculateDelayScore()
        {
            if (rhythmPointIndex >= rhythmPoints.Count)
            {
                rhythmPointIndex = 0;
            }
            else if (timer > rhythmPoints[rhythmPointIndex] + scoreDelay) //when timer exceeds rhythmpoint add to index
            {
                delay = timer - rhythmPoints[rhythmPointIndex];
                currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
                rhythmPointIndex++;
            }
        }

        private void SetTimer()
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString();
        }


        private void SetScore()
        {
            if (!Input.GetKeyDown(key)) return;
            Debug.Log("erg0jmeprgpomergpom");
            score += (int) delay;
            scoreText.text = score.ToString();
        }
    }
}