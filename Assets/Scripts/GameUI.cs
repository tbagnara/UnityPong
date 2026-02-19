using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI startButtonText;
    private GameManager manager;
    
    public Button startButton;
    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
        startButton.enabled = true;
    }
    
    void Update()
    {
        scoreText.text = ""+ manager.getP1Pts() + " - " + manager.getP2Pts();
        if ( manager.getGameOver() )
        {
            winnerText.text = "Player " + manager.getWinner() + " Wins!";
            startButton.enabled = true;
            startButton.image.enabled = true;
            startButtonText.enabled = true;
        }
        else
        {
            winnerText.text = " ";
            startButton.enabled = false;
            startButton.image.enabled = false;
            startButtonText.enabled = false;
        }
    }

}
