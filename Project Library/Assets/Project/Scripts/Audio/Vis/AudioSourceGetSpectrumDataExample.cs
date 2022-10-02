using Sirenix.OdinInspector;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioSourceGetSpectrumDataExample : MonoBehaviour
{
    public FFTWindow fftWindow;

    [Title("RED Params")] public int iMinus = 1;
    public int logPlus = 10;
    public float z = 0;

    public float yMult = 1;

    [Tooltip("multiple of 2")] public int spectrumArray;

    public bool red, cyan, green, blue;

    void Update()
    {
        float[] spectrum = new float[spectrumArray];


        AudioListener.GetSpectrumData(spectrum, 0, fftWindow);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            if (red)
            {
                Debug.DrawLine(new Vector3(i - 1, Mathf.Exp(spectrum[i * logPlus]) * yMult, z),
                    new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.red);
            }

            if (cyan)
            {
                Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, z),
                    new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            }

            if (green)
            {
                Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, z),
                    new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            }

            if (blue)
            {
                Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), z),
                    new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
            }
        }
    }
}