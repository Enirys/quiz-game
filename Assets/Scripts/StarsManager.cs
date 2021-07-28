using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarsManager : MonoBehaviour
{
    public static StarsManager Instance;
    public List<GameObject> stars = new List<GameObject>();

    public int starsEarned = 0;
    private int _totalStarsEarned = 0;

    [SerializeField] private TextMeshProUGUI _totalStarsEarnedTxt;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    public void CalculateStars()
    {
        int nbAttempts = RoundManager.Instance.nbAttempts;
        starsEarned = 0;
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }

        if(nbAttempts == 1)
        {
            starsEarned = 3;
        }else if(nbAttempts > 1 && nbAttempts < 4)
        {
            starsEarned = 2;
        }else if(nbAttempts > 3 && nbAttempts < 6)
        {
            starsEarned = 1;
        }else if(nbAttempts > 5)
        {
            starsEarned = 0;
        }
        _totalStarsEarned += starsEarned;
        ShowEarnedStars(starsEarned);
    }

    public void ShowEndGameStars()
    {
        ShowStars(_totalStarsEarnedTxt, _totalStarsEarned);
    }    
    
    private void ShowStars(TextMeshProUGUI starsTxt, int stars)
    {
        starsTxt.text = stars.ToString();
    }

    private void ShowEarnedStars(int nbStars)
    {
        for(int i = 0; i < nbStars; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
