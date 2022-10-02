using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

public class InstatiateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    public float maxYScale;

    [BoxGroup("cubeSize")] public float cubeSizeX = 10, cubeSizeZ = 10;

    public float startingSize = 2;

    private GameObject[] _cubeList;

    private float _angle = 0.703125f;


    // Start is called before the first frame update
    void Start()
    {
        _cubeList = new GameObject[SpectrumAnalysis.instance.sampleSize];
        for (int i = 0; i < _cubeList.Length; i++)
        {
            GameObject instancedCube = Instantiate(cubePrefab, this.transform);

            instancedCube.transform.position = transform.position;

            instancedCube.name = "instanceCube" + i;
            transform.eulerAngles = new Vector3(0, -_angle * i, 0);
            instancedCube.transform.position = Vector3.forward * 100;
            _cubeList[i] = instancedCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _cubeList.Length; i++)
        {
            if (_cubeList != null)
            {
                _cubeList[i].transform.localScale =
                    new Vector3(cubeSizeX, (SpectrumAnalysis.instance.samples[i] * maxYScale) + startingSize,
                        cubeSizeZ);
            }
        }
    }
}