using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RD.Scripts
{
    public class RhythmVisuals : MonoBehaviour
    {
        private RhythmController _rhythmController;
        public static RhythmVisuals instance;

        [SerializeField] private ParticleSystem pSystem;
        private ParticleSystem.MainModule _particleSystemMain;
        private ParticleSystem.EmissionModule _particleSystemEmission;

        [SerializeField] private TextMeshProUGUI delayText;


        // Start is called before the first frame update
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        private void Start()
        {
            _rhythmController = GetComponent<RhythmController>();
            _particleSystemMain = pSystem.main;
            _particleSystemEmission = pSystem.emission;
        }

        public void UpdateVisuals(List<GameObject> cubeList, int rhythmPointIndex)
        {
            var t = cubeList[rhythmPointIndex];
            var tg = t.GetComponent<MeshRenderer>().material;
            switch (_rhythmController.RhythmPointIndexDifference())
            {
                case <= GlobalValues.ThresholdVeryEarly:
                    _particleSystemMain.startColor = GlobalValues.ColorVeryEarly;
                    _particleSystemEmission.rateOverTime = 50;
                    delayText.text = GlobalValues.VeryEarly;
                    tg.color = GlobalValues.ColorVeryEarly;
                    break;
                case < GlobalValues.ThresholdEarly:
                    _particleSystemMain.startColor = GlobalValues.ColorEarly;
                    _particleSystemEmission.rateOverTime = 100;
                    delayText.text = GlobalValues.Early;
                    tg.color = GlobalValues.ColorEarly;
                    break;
                case <= GlobalValues.ThresholdGood:
                    _particleSystemMain.startColor = GlobalValues.ColorGood;
                    _particleSystemEmission.rateOverTime = 250;
                    delayText.text = GlobalValues.Good;
                    tg.color = GlobalValues.ColorGood;
                    break;

                case <= GlobalValues.ThresholdPerfect:
                    _particleSystemMain.startColor = GlobalValues.ColorPerfect;
                    _particleSystemEmission.rateOverTime = 500;
                    delayText.text = GlobalValues.Perfect;
                    tg.color = GlobalValues.ColorPerfect;
                    break;
            }


            pSystem.Play();
        }
    }
}