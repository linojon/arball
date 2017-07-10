using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {

    private PlayerProgress playerProgress;

    public void Awake()
    {
        LoadPlayerProgress();
    }
    public void SubmitNewPlayerScore(int newScore)
    {
        // If newScore is greater than playerProgress.highestScore, update playerProgress with the new value and call SavePlayerProgress()
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void LoadPlayerProgress()
    {
        // Create a new PlayerProgress object
        playerProgress = new PlayerProgress();

        // If PlayerPrefs contains a key called "highestScore", set the value of playerProgress.highestScore using the value associated with that key
        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    // This function could be extended easily to handle any additional data we wanted to store in our PlayerProgress object
    private void SavePlayerProgress()
    {
        // Save the value playerProgress.highestScore to PlayerPrefs, with a key of "highestScore"
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }
}
