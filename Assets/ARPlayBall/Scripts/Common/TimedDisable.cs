using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDisable : MonoBehaviour
{

    public float time;
    void OnEnable()
    {
        Invoke("Disable", time);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
