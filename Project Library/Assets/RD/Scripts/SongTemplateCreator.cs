using System.Collections.Generic;
using System.Linq;
using RD.Scripts.Scriptable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RD.Scripts
{
    public class SongTemplateCreator : MonoBehaviour
    {
        [Title("Template")] public bool createTemplate;

        [ReadOnly] [SerializeField] private float timer;

        [ReadOnly] [SerializeField] private float timingCooldown;

        [SerializeField] private float secondsBetweenAdding;

        [Title("", "Template data")] public Vector2 analysisRange = new(0, 50);

        [Tooltip("higher is more datapoints per second ")] [Range(.01f, .4f)]
        public float detectionThreshold = .03f;

        [SerializeField] private bool overrideTemplate;

        public SongTemplate songTemplate;

        [ReadOnly] public List<float> newTemplateList;


        [Title("", "Debug Noise")] public AudioSource beepAudioSource;

        [ShowIfGroup("createTemplate")] public AudioClip beepSound;


// Update is called once per frame
        private void Update()
        {
            if (!createTemplate) return;
            SetTimers();
            CheckTemplateContents();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                beepAudioSource.PlayOneShot(beepSound);
            }
        }

        private void SetTimers()
        {
            timer += Time.deltaTime;
            if (timingCooldown > 0)
            {
                timingCooldown -= Time.deltaTime;
            }
        }

        private void CheckTemplateContents()
        {
            if (!(timer <= songTemplate.audioClip.length)) return;
            if (!overrideTemplate)
            {
                if (songTemplate.peakPoints.Count > 1) //check if template array already contains values
                {
                    Debug.LogWarning("template already contains values");
                }
                else //if array does not contain values override anyway
                {
                    SendToScriptable();
                }
            }
            else
            {
                SendToScriptable();
            }
        }

        private void SendToScriptable()
        {
            for (var i = (int) analysisRange.x; i < (int) analysisRange.y; i++)
            {
                if (!(timingCooldown <= 0)) continue;
                if (!(SpectrumAnalysis.instance.samples[i] >= detectionThreshold))
                    continue; //only save values above threshold
                timingCooldown = secondsBetweenAdding;
                beepAudioSource.PlayOneShot(beepSound);
                newTemplateList.Add(timer);
                newTemplateList = newTemplateList.Distinct().ToList(); // check for duplicates in list
                songTemplate.peakPoints = newTemplateList;
            }
        }
    }
}