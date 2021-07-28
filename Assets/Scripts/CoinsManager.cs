using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsManager : MonoBehaviour
{
    public static CoinsManager Instance;

    private int _starsEarned;
    private int _coinsEarned;
    private int _totalCoinsEarned;

    [SerializeField] private TextMeshProUGUI _earnedCoinsTxt;
    [SerializeField] private TextMeshProUGUI _totalcoinsEarnedTxt;
    [SerializeField] private TextMeshProUGUI _totalcoinsEarnedGameTxt;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    public void Initialize()
    {
        _totalCoinsEarned = 0;
        ShowCoins(_totalcoinsEarnedTxt, _totalCoinsEarned);
    }

    public void CalculateCoins()
    {
        _starsEarned = StarsManager.Instance.starsEarned;
        if(_starsEarned == 3)
        {
            _coinsEarned = 100;
        }else if(_starsEarned == 2)
        {
            _coinsEarned = 60;
        }else if(_starsEarned == 1)
        {
            _coinsEarned = 30;
        }else
        {
            _coinsEarned = 0;
        }
        _totalCoinsEarned += _coinsEarned;
        ShowCoins(_totalcoinsEarnedTxt, _totalCoinsEarned);
        ShowCoins(_earnedCoinsTxt, _coinsEarned);
    }

    private void ShowCoins(TextMeshProUGUI coinsTxt, int coins)
    {
        coinsTxt.text = coins.ToString();
    }

    public void ShowEndGameCoins()
    {
        ShowCoins(_totalcoinsEarnedGameTxt, _totalCoinsEarned);
    }
}
