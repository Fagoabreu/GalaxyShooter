using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] lives =null;
    [SerializeField]
    private Image livesImageDisplay=null;
    [SerializeField]
    private TextMeshProUGUI scoreTextDisplay = null;
    [SerializeField]
    private GameObject titleScreen=null;

    private int _score = 0;
   
   public void UpdateLives(int currentLives)
    {
        if(currentLives>= lives.Length)
        {
            currentLives = lives.Length - 1;
        }else if (currentLives < 0)
        {
            currentLives = 0;
        }
        livesImageDisplay.sprite = lives[currentLives];
    }
    public void UpdateScores(int pontos)
    {
        _score += pontos;
        scoreTextDisplay.text = "Score: "+ _score;
    }

    public void resetScore()
    {
        _score = 0;
    }

    public void showTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void hideTitleScreen()
    {
        titleScreen.SetActive(false);
        _score = 0;
        scoreTextDisplay.text = "Score: ";
    }
}
