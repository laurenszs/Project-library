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
        private float timer, delay, scoreDelay = .3f;

        [TabGroup("TimerData")] public SongTemplate songTemplate;

        [TabGroup("TimerData")] [SerializeField]
        private List<float> rhythmPoints;

        [TabGroup("TimerData")] [SerializeField]
        private int rhythmPointIndex;

        [TabGroup("Score")] [SerializeField] private int baseScore = 500, subtractionMultiplier = 100, score;

        [SerializeField] private GameObject rhythmIndicator;

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
            //  CalculateDelayScore();
            delay = timer - rhythmPoints[rhythmPointIndex];
            rhythmIndicator.transform.position = new Vector3(rhythmIndicator.transform.position.x, delay *= -1,
                rhythmIndicator.transform.position.z);
        }


        // private void CalculateDelayScore()
        // {
        //     if (!Input.GetKeyDown(key)) return;
        //     if (rhythmPointIndex >= rhythmPoints.Count)
        //     {
        //         rhythmPointIndex = 0;
        //         return;
        //     }
        //
        //     delay = timer - rhythmPoints[rhythmPointIndex];
        //     score += Mathf.RoundToInt(baseScore - delay * subtractionMultiplier);
        //
        //     if (!(timer > rhythmPoints[rhythmPointIndex] + scoreDelay)) return;
        //     score -= baseScore;
        //     rhythmPointIndex++;
        // }

        private void SetTimer()
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString();
        }


        private void SetText()
        {
            scoreText.text = delay.ToString();
            currentPoint.text = rhythmPoints[rhythmPointIndex].ToString();
        }
    }
}