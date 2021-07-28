using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _roundDuration;
    [SerializeField] private TextMeshProUGUI _timerRoundTxt;
    [SerializeField] private UnityEvent OnRoundTimerUp;

    private bool _timerRoundIsRunning = false;
    private bool _hurryUpTriggered = false;
    private float _timerRoundRemaining;
    

    private void Update()
    {
        UpdateRoundTimer();
    }

    public void StartRoundTimer()
    {
        _timerRoundIsRunning = true;
        _hurryUpTriggered = false;
    }

    public void ResetRoundTimer()
    {
        _timerRoundRemaining = _roundDuration;
        _timerRoundIsRunning = false;
    }

    private void UpdateRoundTimer()
    {
        if(_timerRoundIsRunning)
        {
            if(_timerRoundRemaining > 0)
            {
                if(_timerRoundRemaining < 10 && !_hurryUpTriggered)
                {
                    AudioManager.Instance.PlaySound(SoundList.HurryUp);
                    _hurryUpTriggered = true;
                }
                _timerRoundRemaining -= Time.deltaTime;
                DisplayRoundTime(_timerRoundRemaining);
            }else
            {
                TimerRoundUp();
            }
        }
    }

    public void EndTimer()
    {
        ResetRoundTimer();
    }

    public void PauseTimer()
    {
        _timerRoundIsRunning = false;
    }

    public void ResumeTimer()
    {
        _timerRoundIsRunning = true;
    }

    private void TimerRoundUp()
    {
        _timerRoundIsRunning = false;
        _timerRoundRemaining = 0;
        DisplayRoundTime(_timerRoundRemaining);
        OnRoundTimerUp?.Invoke();
        AudioManager.Instance.PlaySound(SoundList.TimeUp);
    }

    private void DisplayRoundTime(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        _timerRoundTxt.text = string.Format("Timer: {0:00}:{1:00}", minutes, seconds);
    }
}
