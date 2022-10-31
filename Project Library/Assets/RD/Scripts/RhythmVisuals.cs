using UnityEngine;

namespace RD.Scripts
{
    public class RhythmVisuals : MonoBehaviour
    {
        private RhythmController _rhythmController;
        public static RhythmVisuals instance;

        [SerializeField] private ParticleSystem pSystem;

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
        }

        public void PlayParticles()
        {
            var particleSystemMain = pSystem.main;
            var particleSystemEmission = pSystem.emission;
            switch (_rhythmController.RhythmPointIndexDifference())
            {
                case <= GlobalValues.ThresholdVeryEarly:
                    particleSystemMain.startColor = GlobalValues.ColorVeryEarly;
                    particleSystemEmission.rateOverTime = 50;
                    break;
                case < GlobalValues.ThresholdEarly:
                    particleSystemMain.startColor = GlobalValues.ColorEarly;
                    particleSystemEmission.rateOverTime = 100;
                    break;
                case <= GlobalValues.ThresholdGood:
                    particleSystemMain.startColor = GlobalValues.ColorGood;
                    particleSystemEmission.rateOverTime = 250;
                    break;

                case <= GlobalValues.ThresholdPerfect:
                    particleSystemMain.startColor = GlobalValues.ColorPerfect;
                    particleSystemEmission.rateOverTime = 500;
                    break;
            }

            pSystem.Play();
        }
    }
}