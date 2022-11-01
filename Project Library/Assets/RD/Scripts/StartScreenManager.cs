using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RD.Scripts
{
    public class StartScreenManager : MonoBehaviour
    {
        public static StartScreenManager instance;
        [SerializeField] private KeyCode key;

        [SerializeField] private int index;
        [SerializeField] private TextMeshPro pressText;

        private void Awake()
        {
            if (pressText != null)
            {
                pressText.text = $"press {key} to start";
            }

            DontDestroyOnLoad(this);
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                SwitchScene(index);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SwitchScene(0);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SwitchScene(1);
            }
        }

        public void SwitchScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}