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

        [SerializeField] private TextMeshProUGUI delayHint;
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");


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
                case < GlobalValues.ThresholdPerfect:
                    _particleSystemMain.startColor = GlobalValues.ColorPerfect;
                    _particleSystemEmission.rateOverTime = 500;

                    delayHint.text = GlobalValues.Perfect;
                    delayHint.color = GlobalValues.ColorPerfect;
                    _rhythmController.delaySnapText.color = GlobalValues.ColorPerfect;

                    tg.SetColor(EmissionColor, GlobalValues.ColorPerfect);
                    break;
                case < GlobalValues.ThresholdGreat:
                    _particleSystemMain.startColor = GlobalValues.ColorGreat;
                    _particleSystemEmission.rateOverTime = 500;

                    delayHint.text = GlobalValues.Great;
                    delayHint.color = GlobalValues.ColorGreat;
                    _rhythmController.delaySnapText.color = GlobalValues.ColorGreat;

                    tg.SetColor(EmissionColor, GlobalValues.ColorGreat);
                    break;
                case < GlobalValues.ThresholdGood:
                    _particleSystemMain.startColor = GlobalValues.ColorGood;
                    _particleSystemEmission.rateOverTime = 250;

                    delayHint.text = GlobalValues.Good;
                    delayHint.color = GlobalValues.ColorGood;
                    _rhythmController.delaySnapText.color = GlobalValues.ColorGood;

                    tg.SetColor(EmissionColor, GlobalValues.ColorGood);
                    break;
                case < GlobalValues.ThresholdEarly:
                    _particleSystemMain.startColor = GlobalValues.ColorEarly;
                    _particleSystemEmission.rateOverTime = 100;

                    delayHint.text = GlobalValues.Early;
                    delayHint.color = GlobalValues.ColorEarly;
                    _rhythmController.delaySnapText.color = GlobalValues.ColorEarly;

                    tg.SetColor(EmissionColor, GlobalValues.ColorEarly);
                    break;

                case <= GlobalValues.ThresholdVeryEarly:
                    _particleSystemMain.startColor = GlobalValues.ColorVeryEarly;
                    _particleSystemEmission.rateOverTime = 50;

                    delayHint.text = GlobalValues.VeryEarly;
                    delayHint.color = GlobalValues.ColorVeryEarly;
                    _rhythmController.delaySnapText.color = GlobalValues.ColorVeryEarly;

                    tg.SetColor(EmissionColor, GlobalValues.ColorVeryEarly);
                    break;
            }


            pSystem.Play();
        }
    }
}