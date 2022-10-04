using System.Collections.Generic;
using System.Linq;
using RD.Scripts.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SongTemplateCreator : MonoBehaviour
    {
        [ReadOnly] [SerializeField] private float[] peakPoints;
        [ReadOnly] [SerializeField] private float timer;
        public bool createTemplate;

        public int bassRange = 50;

        public float detectionThreshold = .03f;

        [ReadOnly] public List<float> newTemplateList;

        public SongTemplate songTemplate;

        public AudioClip beepSound;

        [SerializeField] private AudioSource audioSource;

        private AudioSource localAudiosource;

        // Start is called before the first frame update
        void Start()
        {
            localAudiosource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (createTemplate)
            {
                SetTimer();
            }

            for (var i = 0; i < bassRange; i++)
            {
                if (SpectrumAnalysis.samples[i] >= detectionThreshold)
                {
                    audioSource.PlayOneShot(beepSound);
                    Debug.Log("added");
                    newTemplateList.Add(timer);
                    newTemplateList = newTemplateList.Distinct().ToList();
                    songTemplate.peakPoints = newTemplateList;
                }
            }
        }

        private void SetTimer()
        {
            timer += Time.deltaTime;
        }

        private void sendToScriptable()
        {
            songTemplate.peakPoints = newTemplateList;
        }
    }
}