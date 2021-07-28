using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameProgress : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _progressTxt;
    
	private float _currentProgress;
	private int _completedRound;
    private int _totalRounds;

    public void UpdateGameProgress()
    {
		_completedRound = RoundManager.Instance.completedRound;
		_totalRounds = RoundManager.Instance.totalRounds;
        _currentProgress = (float) _completedRound / _totalRounds;
		_progressTxt.text = _currentProgress * 100 + "%";
        _progressBar.value = _currentProgress;
    }
}
