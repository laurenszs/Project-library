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


        // Start is called before the first frame update
        private void Start()
        {
            key = KeyCode.Space;
            timer = 0;
            rhythmPointIndex = 0;

            if (!songTemplate)
            {
                Debug.LogWarning("No template added");
                return;
            }

            rhythmPoints = songTemplate.peakPoints;
        }

        // Update is called once per frame
        private void Update()
        {
            SetTimer();
            SetScore();
            CalculateDelay();
            SetTexts();
            if (rhythmPointIndex > rhythmPoints.Count)
            {
                rhythmPointIndex = 0;
            }
        }

        private void CalculateDelay()
        {
            if (!(timer > rhythmPoints[rhythmPointIndex] + scoreDelay))//when timer exceeds rhythmpoint add to index
            {
                rhythmPointIndex = 0;
                return;
            }

            if (rhythmPointIndex >= rhythmPoints.Count) return;
            Mathf.Clamp(rhythmPointIndex++, 0, rhythmPoints.Count);

            delay = timer - rhythmPoints[rhythmPointIndex];
        }

        private void SetTimer()
        {
            timer += Time.deltaTime;
        }

        private void SetTexts()
        {
            timerText.text = timer.ToString();
            currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
            scoreText.text = score.ToString();
        }

        private void SetScore()
        {
            if (Input.GetKeyDown(key))
            {
                score += Mathf.RoundToInt(delay);
            }
        }
    }
}