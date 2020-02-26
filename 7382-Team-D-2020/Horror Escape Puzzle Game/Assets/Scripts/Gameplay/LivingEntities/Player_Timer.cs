using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player_Timer : MonoBehaviour
{
    [SerializeField] private float _timerMax = 50f;
    [SerializeField] private bool _isTimerCalcPause = false;
    [SerializeField] private float _curTime = 0;
    [SerializeField] private float _pauseTimeCutDown = 1;
    [SerializeField] private float _curPauseTimeCutDown = 0;
    [SerializeField] private bool _isPauseTimeCutDown = false;
    [SerializeField] private bool _isNeedPressQ = false;

    public float GetCurTime
    {
        get => _curTime;
    }

    public void StartCalcTime()
    {
        _curTime = _timerMax;
        _isTimerCalcPause = false;
    }
    public void PauseCalcTime(bool flag)
    {
        _isTimerCalcPause = flag;
    }
    private void Start()
    {
        StartCalcTime();
    }

    private void PauseTimeByKeyDown()
    {
        _curPauseTimeCutDown = _pauseTimeCutDown;
        PauseCalcTime(true);
        _isPauseTimeCutDown = true;
    }

    public void ChangePauseTimeKeyToQ(bool flag)
    {
        _isNeedPressQ = flag;
    }
    private void Update()
    {
        if (!_isPauseTimeCutDown)
        {
            if (_isNeedPressQ)
            {
                if (Input.GetKeyDown(KeyCode.Q) )
                {
                    PauseTimeByKeyDown();
                }
            }
            else
            {
                if ( Input.GetKeyDown(KeyCode.E))
                {
                    PauseTimeByKeyDown();
                }
            }
           
        }

        if (!_isTimerCalcPause)
        {
            _curTime -= Time.deltaTime;
            if (_curTime <= 0)
            {
                _isTimerCalcPause = true;
                _curTime = 0;
                //todo dead
            }
        }

        if (_isPauseTimeCutDown)
        {
            _curPauseTimeCutDown -= Time.deltaTime;
            if (_curPauseTimeCutDown <= 0)
            {
                _curPauseTimeCutDown = 0;
                _isPauseTimeCutDown = false;
                PauseCalcTime(false);
            }
        }
    }

}