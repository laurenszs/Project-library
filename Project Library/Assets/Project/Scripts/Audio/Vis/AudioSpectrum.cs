using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    private float[] m_audioSpectrum;
    [SerializeField] private int s_listSize = 128;
    [SerializeField] private int spectrumMult = 100;


    private void Start()
    {
        m_audioSpectrum = new float[s_listSize];
    }

    private void Update()
    {
        AudioListener.GetSpectrumData(m_audioSpectrum, 0, FFTWindow.Hamming);

        if (m_audioSpectrum is {Length: > 0})
        {
            spectrumValue = m_audioSpectrum[0] * spectrumMult;
        }
    }
}