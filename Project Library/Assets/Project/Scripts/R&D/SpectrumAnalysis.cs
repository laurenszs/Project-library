using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumAnalysis : MonoBehaviour
{
    public static SpectrumAnalysis instance;
    private AudioSource _audioSource;
    public FFTWindow _fftWindow;
    public int sampleSize;

    [ReadOnly] public float[] samples;

    // Start is called before the first frame update
    void Start()
    {
        samples = new float[sampleSize];
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    [ExecuteInEditMode]
    void Update()
    {
        calcSamplesize();
        GetSpectrumData();
    }

    void calcSamplesize()
    {
        Mathf.ClosestPowerOfTwo(sampleSize);
    }

    void GetSpectrumData()
    {
        _audioSource.GetSpectrumData(samples, 1, _fftWindow);
    }
}