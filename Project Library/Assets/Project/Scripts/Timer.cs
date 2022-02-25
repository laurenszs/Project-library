using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //timer, countdown
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float time;
    void SetTimer()
        {
            time -= Time.deltaTime;
        timerText.text = Mathf.CeilToInt(time).ToString();//set timer to text

            if (time <= 0) { SceneManager.LoadScene(""); }
        }
}
