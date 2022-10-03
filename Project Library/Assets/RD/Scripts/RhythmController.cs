using System;
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


        [TabGroup("TimerData")] [SerializeField]
        private float[] rhythmPoints;

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
        }

        // Update is called once per frame
        private void Update()
        {
            SetTimer();
            setScore();
            CalculateDelay();
            SetTexts();
        }

        private void CalculateDelay()
        {
            if (timer > rhythmPoints[rhythmPointIndex] + scoreDelay)
                Mathf.Clamp(rhythmPointIndex++, 0, rhythmPoints.Length);

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

        private void setScore()
        {
            if (Input.GetKeyDown(key))
            {
                score += Mathf.RoundToInt(delay);
            }
        }
    }
}