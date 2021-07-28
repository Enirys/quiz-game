using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gamePanel;
    public GameObject mainMenuPanel;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject overlayPanel;

    public Button btn;
    public Sprite disabledSprite;
    public Sprite enabledSprite;

    public static UIManager Instance;

    private void Awake()
    {
        if(Instance != null) return;
        Instance = this;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void DisableButton()
    {
        btn.interactable = false;
        btn.GetComponent<Image>().sprite = disabledSprite;
    }

    public void EnableButton()
    {
        btn.interactable = true;
        btn.GetComponent<Image>().sprite = enabledSprite;
    }
}
