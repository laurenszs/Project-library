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

        [ReadOnly] public float[] sampleShower;

        // Start is called before the first frame update
        private void Start()
        {
            samples = new float[sampleSize];
            _audioSource = GetComponent<AudioSource>();
            _audioSource.PlayOneShot();
        }

        // Update is called once per frame
        private void Update()
        {
            sampleShower = samples; //display samples list
            GetSpectrumData();
        }

        private void GetSpectrumData()
        {
            _audioSource.GetSpectrumData(samples, 1, fftWindow);
        }
    }
}