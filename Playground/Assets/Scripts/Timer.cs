﻿
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class Timer :  MonoBehaviour
{
    [SerializeField] private float time = 0f;

    /// <summary>
    /// Default settings false.
    /// </summary>
    /// <param name="value"> Insert true or false </param>
    public bool Paused = false;
    public float RemainingTime = 0; // redundant - in order to reset the timer to its original time

    private Text timerField = null;

    private StringBuilder builder = new StringBuilder();

    public UnityEvent OnTimerFinished = new UnityEvent();

    private void Awake()
    {
        timerField = GetComponent<Text>();
    }

    public void Enable()
    {
        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        RemainingTime = time;

        while(RemainingTime > 0)
        {
            if (!Paused)
            {
                yield return new WaitForSecondsRealtime(Time.deltaTime);
                RemainingTime -= Time.deltaTime;
                DisplayTime();
            }
            else
            {
                yield return null;
            }  
        }
        OnTimerFinished?.Invoke();
    }

    public void ResetTime()
    {
        StopAllCoroutines();
        Enable();
    }

    private void DisplayTime()
    {
        int front = (int)RemainingTime;

        builder.Append(front);
        builder.Append(":");
        builder.Append(Convert.ToInt32((RemainingTime - front) * 100).ToString("D2"));

        timerField.text = builder.ToString();
        builder.Clear();
    }
}
