using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private bool _gameStarted = false;
    private bool _gamePaused = false;
    public bool _gameReplay = false;

    private int pointsEarnedRound = 0;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    public void AddPoint()
    {
        pointsEarnedRound++;
    }

    public void SubstractPoint()
    {
        pointsEarnedRound--;
    }

    public void CheckWin()
    {
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        RoundManager.Instance.EndRound();
        if(pointsEarnedRound >= 3)
        {
            if(RoundManager.Instance.currentRound >= 10)
            {
                GameOver();
            }else
            {
                WinRound();
            }
            
        }else
        {
            LoseRound();
        }
    }

    public void CheckGameOver()
    {
        if(RoundManager.Instance.currentRound >= 10)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        _gameStarted = true;
        _gamePaused = false;
        _gameReplay = false;
        CoinsManager.Instance.Initialize();
        UIManager.Instance.OpenPanel(UIManager.Instance.gamePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.mainMenuPanel);
        RoundManager.Instance.StartRound();
        AudioManager.Instance.PlaySound(SoundList.Go);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        CardManager.Instance.InitializeCards();
    }

    public void PauseGame()
    {
        _gamePaused = true;
        _gameStarted = false;
        _gameReplay = false;
        RoundManager.Instance.PauseRound();
        UIManager.Instance.OpenPanel(UIManager.Instance.pausePanel);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
    }

    public void ResumeGame()
    {
        _gamePaused = false;
        _gameStarted = true;
        _gameReplay = false;
        RoundManager.Instance.ResumeRound();
        UIManager.Instance.ClosePanel(UIManager.Instance.pausePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.overlayPanel);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
    }

    public void MainMenu()
    {
        _gamePaused = false;
        _gameStarted = false;
        _gameReplay = false;
        UIManager.Instance.ClosePanel(UIManager.Instance.gamePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.winPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.losePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.gameOverPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.pausePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.overlayPanel);
        UIManager.Instance.OpenPanel(UIManager.Instance.mainMenuPanel);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
    }

    // When you lose the current round
    public void LoseRound()
    {
        _gamePaused = false;
        _gameStarted = false;
        _gameReplay = false;
        UIManager.Instance.OpenPanel(UIManager.Instance.losePanel);
        UIManager.Instance.OpenPanel(UIManager.Instance.overlayPanel);
        //AudioManager.Instance.PlaySound(SoundList.Lose);
    }   

    // When you win the current round
    public void WinRound()
    {
        _gamePaused = false;
        _gameStarted = false;
        _gameReplay = false;
        StarsManager.Instance.CalculateStars();
        CoinsManager.Instance.CalculateCoins();
        if(StarsManager.Instance.starsEarned <= 2)
        {
            UIManager.Instance.EnableButton();
        }else
        {
            UIManager.Instance.DisableButton();
        }
        UIManager.Instance.OpenPanel(UIManager.Instance.winPanel);
        UIManager.Instance.OpenPanel(UIManager.Instance.overlayPanel);
        //AudioManager.Instance.PlaySound(SoundList.Win);
    }

    // When all 10 rounds are over
    public void GameOver()
    {
        _gamePaused = false;
        _gameStarted = false;
        _gameReplay = false;
        StarsManager.Instance.ShowEndGameStars();
        CoinsManager.Instance.ShowEndGameCoins();
        UIManager.Instance.OpenPanel(UIManager.Instance.gameOverPanel);
        UIManager.Instance.OpenPanel(UIManager.Instance.overlayPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.winPanel);
        AudioManager.Instance.PlaySound(SoundList.Win);
    }

    // When restart game
    public void RestartGame()
    {
        _gamePaused = false;
        _gameStarted = true;
        _gameReplay = false;
        CardManager.Instance.InitializeCards();
        UIManager.Instance.ClosePanel(UIManager.Instance.overlayPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.gameOverPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.pausePanel);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        RoundManager.Instance.completedRound = 0;
        RoundManager.Instance.currentRound = 1;
        StartGame();
    }

    // When restart round
    public void RestartRound()
    {
        _gamePaused = false;
        _gameStarted = true;
        _gameReplay = true;
        pointsEarnedRound = 0;
        UIManager.Instance.ClosePanel(UIManager.Instance.losePanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.winPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.overlayPanel);
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        RoundManager.Instance.StartRound();
    }

    // Next round
    public void StartNextRound()
    {
        _gamePaused = false;
        _gameStarted = true;
        _gameReplay = false;
        RoundManager.Instance.completedRound++;
        RoundManager.Instance.currentRound++;
        pointsEarnedRound = 0;
        AudioManager.Instance.PlaySound(SoundList.ButtonClick);
        RoundManager.Instance.StartRound();
        UIManager.Instance.ClosePanel(UIManager.Instance.overlayPanel);
        UIManager.Instance.ClosePanel(UIManager.Instance.winPanel);
    }
}
