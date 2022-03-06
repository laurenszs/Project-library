using UnityEngine;

public class AudioSync : MonoBehaviour
{
    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    private float m_previewsAudioValue;
    private float m_audioValue;
    private float m_timer;

    protected bool m_isBeat;


    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        m_previewsAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        if (m_previewsAudioValue > bias && m_audioValue <= bias)
        {
            if (m_timer > timeStep)
                OnBeat();
        }


        if (!(m_previewsAudioValue <= bias) && (m_audioValue > bias))
        {
            if (m_timer > timeStep)
                OnBeat();
        }

        m_timer += Time.deltaTime;
    }

    public virtual void OnBeat()
    {
        Debug.Log("beat");
        m_timer = 0;
        m_isBeat = true;
    }
}