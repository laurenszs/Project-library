using System.Collections.Generic;
using System.Linq;
using RD.Scripts.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    public class SongTemplateCreator : SpectrumAnalysis
    {
        [Title("Template")] public bool createTemplate;

        [ShowIfGroup("createTemplate")] [ReadOnly] [SerializeField]
        private float timer;

        [Title("", "Template modifiers")] [ShowIfGroup("createTemplate")] [ShowIfGroup("createTemplate")]
        public Vector2 analysisRange = new(0, 50);

        [ShowIfGroup("createTemplate")] public float detectionThreshold = .03f;

        [ShowIfGroup("createTemplate")] public AudioSource beepAudioSource;
        [ShowIfGroup("createTemplate")] public AudioClip beepSound;

        [Title("", "Template data")] [ShowIfGroup("createTemplate")]
        public AudioSource templateAudioSource;

        [ShowIfGroup("createTemplate")] public SongTemplate songTemplate;

        [ShowIfGroup("createTemplate")] [ReadOnly]
        public List<float> newTemplateList;


        // Start is called before the first frame update
        private void Start()
        {
            // songTemplate.audioClip
        }

        // Update is called once per frame
        private void Update()
        {
            if (!createTemplate) return;
            SetTimer();
            SendToScriptable();
        }

        private void SetTimer()
        {
            timer += Time.deltaTime;
        }

        private void SendToScriptable()
        {
            for (var i = (int) analysisRange.x; i < (int) analysisRange.y; i++)
            {
                if (samples[i] >= detectionThreshold)
                {
                    beepAudioSource.PlayOneShot(beepSound);
                    Debug.Log("added");
                    newTemplateList.Add(timer);
                    newTemplateList = newTemplateList.Distinct().ToList();
                    songTemplate.peakPoints = newTemplateList;
                }
            }
        }
    }
}