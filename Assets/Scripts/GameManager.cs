using System;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private NetworkVariable<int> player1Pts = new NetworkVariable<int>(0);
    private NetworkVariable<int> player2Pts = new NetworkVariable<int>(0);
    private NetworkVariable<Boolean> gameOver = new NetworkVariable<bool>(true);
    private NetworkVariable<int> winner = new NetworkVariable<int>(1);
    void Start()
    {
        
    }
    void Update()
    {
        if (IsServer)
        {
            if (player1Pts.Value>4 || player2Pts.Value >4)
            {
                activateGameOver();
            }
        }
    }

    public void startGame()
    {
        if (IsServer) 
        {
            player1Pts.Value = 0;
            player2Pts.Value = 0;
            gameOver.Value = false;
            BallMovement ball = FindFirstObjectByType<BallMovement>();
            ball.startGame();
        }

    }

    void activateGameOver()
    {
        gameOver.Value = true;
        if (getP1Pts() == 3)
        {
            winner.Value = 1;
        }
        else
        {
            winner.Value = 2;
        }

        BallMovement ball = FindFirstObjectByType<BallMovement>();
        ball.endGame();
    }
    public bool getGameOver()
    {
        return gameOver.Value;
    }

    public int getWinner()
    {
        return winner.Value;
    }

    public int getP1Pts()
    {
        return player1Pts.Value;
    }
    public int getP2Pts()
    {
        return player2Pts.Value;
    }

    public void addToP1()
    {
        player1Pts.Value++;
    }

    public void addToP2()
    {
        player2Pts.Value++;
    }

    
}
