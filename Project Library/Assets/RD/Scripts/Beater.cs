using RD.Scripts;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;


public class Beater : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private bool objIsSelf;
    [SerializeField] private float baseSize = 1;
    [SerializeField] private float multiplier;
    [SerializeField] private int sampleToAnalyze = 15;
    private float _audioSampleValue;

    private void Awake()
    {
        if (obj == null || objIsSelf)
        {
            obj = GetComponent<Transform>();
        }
    }

    private void Update()
    {
        _audioSampleValue = SpectrumAnalysis.instance.samples[sampleToAnalyze];

        SampleToScale(ScaleX(), ScaleY(), ScaleZ());
    }


    private void SampleToScale(float scaleX, float scaleY, float scaleZ)
    {
        obj.localScale = new Vector3(
            scaleX,
            scaleY,
            scaleZ);
    }

    [FoldoutGroup("scale")] [SerializeField]
    private bool scaleX, scaleY, scaleZ;

    private float ScaleX()
    {
        if (scaleX)
        {
            return baseSize + _audioSampleValue * multiplier;
        }

        return obj.localScale.x;
    }

    private float ScaleY()
    {
        if (scaleY)
        {
            return baseSize + _audioSampleValue * multiplier;
        }

        return obj.localScale.y;
    }

    private float ScaleZ()
    {
        if (scaleZ)
        {
            return baseSize + _audioSampleValue * multiplier;
        }

        return obj.localScale.z;
    }
}