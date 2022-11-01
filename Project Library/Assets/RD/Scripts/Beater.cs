using UnityEngine;

namespace RD.Scripts
{
    public class Beater : MonoBehaviour
    {
        [SerializeField] private Transform obj;
        [SerializeField] private float baseSize = 1;
        [SerializeField] private float multiplier;
        [SerializeField] private int sampleToAnalyze = 15;

        private void Awake()
        {
            if (obj == null)
            {
                obj = GetComponent<Transform>();
            }
        }

        private void Update()
        {
            obj.localScale = new Vector3(
                baseSize + SpectrumAnalysis.instance.samples[sampleToAnalyze] * multiplier,
                baseSize + SpectrumAnalysis.instance.samples[sampleToAnalyze] * multiplier,
                baseSize + SpectrumAnalysis.instance.samples[sampleToAnalyze] * multiplier);
        }
    }
}