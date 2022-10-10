using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SpectrumAnalysis : MonoBehaviour
    {
        public static SpectrumAnalysis instance;
        public FFTWindow fftWindow;
        [ReadOnly] [ShowInInspector] public int sampleSize = 1024;

        [ShowInInspector] [ReadOnly] public float[] samples;
        private AudioSource _audioSource;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        // Start is called before the first frame update
        private void Start()
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