using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public BallGame[] BallGames;

    public Text scoreDisplay;
    public Text highScoreDisplay;


    private GameDataManager gameDataManager;
    private int playerScore;
    private int currentGameIndex;
    private bool isInitialized;
    // Use this for initialization
    void Start () {
        gameDataManager = FindObjectOfType<GameDataManager>();                              // Store a reference to the GameDataManager so we can request the data we need for this round
        highScoreDisplay.text = gameDataManager.GetHighestPlayerScore().ToString();

        for (int i = 0; i < BallGames.Length; i++)
        { 
            BallGames[i].OnGameWon.AddListener(WonGame);
            BallGames[i].OnGameLost.AddListener(LostGame);
            BallGames[i].Deactivate();
        }

        isInitialized = true;
   
    }

    private void SetRandomGame()
    {
        //clears the last game
        BallGames[currentGameIndex].Deactivate();
        //sets a new game
        currentGameIndex = Random.Range(0, BallGames.Length);
        BallGames[currentGameIndex].Activate();
    }


    public void StartGame()
    {
        SetRandomGame();
    }


    void WonGame()
    {
        ChangeScore(10);
        SetRandomGame();
    }

    void ChangeScore(int points)
    {
        playerScore = playerScore + points;
        scoreDisplay.text = playerScore.ToString();
        gameDataManager.SubmitNewPlayerScore(playerScore);
        highScoreDisplay.text = gameDataManager.GetHighestPlayerScore().ToString();
    }

    void LostGame()
    {
        ChangeScore(-10);
        SetRandomGame();
    }


    private void OnEnable()
    {
        if(isInitialized)
        SetRandomGame();
    }
}
