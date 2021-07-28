using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    private Timer _timer;
    private GameProgress _gameProgress;

    public int totalRounds = 10;
    public int completedRound = 0;
    public int currentRound = 1;
    public int nbAttempts = 1;

    [SerializeField] private TextMeshProUGUI _roundTxt;

    public static RoundManager Instance;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    private void Start()
    {
        _timer = GetComponent<Timer>();
        _gameProgress = GetComponent<GameProgress>();
    }

    private void InitializeRound()
    {
        MatchManager.Instance.DestroyLines();
        _timer.ResetRoundTimer();
    }

    public void StartRound()
    {
        if(RoundManager.Instance.currentRound == 10)
        {
            AudioManager.Instance.PlaySound(SoundList.FinalRound);
        }

        if(GameController.Instance._gameReplay)
        {
            nbAttempts++;
        }else
        {
            nbAttempts = 1;
        }
        InitializeRound();
        _timer.StartRoundTimer();
        _roundTxt.text = "Round " + currentRound.ToString();
        CardManager.Instance.GenerateCards();
        _gameProgress.UpdateGameProgress();
    }

    public void EndRound()
    {
        _timer.EndTimer();
    }

    public void ResumeRound()
    {
        _timer.ResumeTimer();
    }

    public void PauseRound()
    {
        _timer.PauseTimer();
    }
}
