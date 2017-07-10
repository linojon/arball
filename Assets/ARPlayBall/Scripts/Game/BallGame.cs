using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallGame : MonoBehaviour
{
    public ThrowControl BallThrowControl;
    public GameObject SceneGameObject;
    public CollisionBehavior GoalCollisionBehavior;

    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;

    private bool wonGame;


    public void Start()
    {
        BallThrowControl.OnReset.AddListener(OnBallReset);
        GoalCollisionBehavior.OnHitGameObject.AddListener(CheckGoal);
    }

    public void Activate()
    {
        BallThrowControl.gameObject.SetActive(true);
        SceneGameObject.SetActive(true);
        GoalCollisionBehavior.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        BallThrowControl.gameObject.SetActive(false);
        SceneGameObject.SetActive(false);
        GoalCollisionBehavior.gameObject.SetActive(false);
    }

    void OnBallReset()
    {
        if (wonGame)
        {
            OnGameWon.Invoke();
        }
        else
        {
            OnGameLost.Invoke();
        }
        //Resets the game
        wonGame = false;
    }

    void CheckGoal(GameObject hitGameObject)
    {
        if (hitGameObject == BallThrowControl.gameObject)
            wonGame = true;
    }


}
