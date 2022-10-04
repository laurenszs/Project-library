using System.Collections.Generic;
using UnityEngine;

namespace RD.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "song Template", menuName = "ScriptableObjects/Song Template", order = 1)]
    public class SongTemplate : ScriptableObject
    {
        public List<float> peakPoints;
        public AudioClip audioClip;
    }
}