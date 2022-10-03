using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    public class InstatiateCubes : MonoBehaviour
    {
        public GameObject cubePrefab;
        public float maxYScale;

        [BoxGroup("cubeSize")] public float cubeSizeX = 10, cubeSizeZ = 10;

        public float startingSize = 2;

        public float detectionThreshold;

        private GameObject[] _cubeList;

        private float _angle = 0.3515625f;


        // Start is called before the first frame update
        private void Start()
        {
            _cubeList = new GameObject[SpectrumAnalysis.sampleSize];
            for (var i = 0; i < _cubeList.Length; i++)
            {
                var instancedCube = Instantiate(cubePrefab, this.transform);

                instancedCube.transform.position = transform.position;

                instancedCube.name = "instanceCube" + i;
                transform.eulerAngles = new Vector3(0, -_angle * i, 0);
                instancedCube.transform.position = Vector3.forward * 100;
                _cubeList[i] = instancedCube;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            for (var i = 0; i < _cubeList.Length; i++)
            {
                if (_cubeList != null)
                {
                    _cubeList[i].transform.localScale =
                        new Vector3(cubeSizeX, (SpectrumAnalysis.samples[i] * maxYScale) + startingSize,
                            cubeSizeZ);
                    if (SpectrumAnalysis.samples[i] > detectionThreshold)
                    {
                        _cubeList[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    }
                    else if (SpectrumAnalysis.samples[i] > detectionThreshold / 2)
                    {
                        _cubeList[i].GetComponent<MeshRenderer>().material.color = Color.yellow;
                    }
                    else
                    {
                        _cubeList[i].GetComponent<MeshRenderer>().material.color = Color.grey;
                    }
                }
            }
        }

    }
}