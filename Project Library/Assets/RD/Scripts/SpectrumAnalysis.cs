using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SpectrumAnalysis : MonoBehaviour
    {
        public FFTWindow fftWindow;
        [ReadOnly] [ShowInInspector] public static int sampleSize = 1024;

        public static float[] samples;
        private AudioSource _audioSource;

        // Start is called before the first frame update
        void Start()
        {
            samples = new float[sampleSize];

            _audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            GetSpectrumData();
        }

        private void GetSpectrumData()
        {
            _audioSource.GetSpectrumData(samples, 1, fftWindow);
        }
    }
}