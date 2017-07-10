using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
/// <summary>
/// Similar script found in the Penguin example
/// Slightly different implementation than the DefaultTrackableEventHandler class in two ways:
/// 1. We turn off all its children components only when SmartTerrain Trackable is lost - not here
/// 2. We reset everything to identity (position and rotation) when this trackable is lost as a 'pose correction' mechanism
/// </summary>
public class ImageTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{


    public UnityEvent OnImageTrackableFoundFirstTime;
    private bool toggleOnStateChange;

    private TrackableBehaviour mTrackableBehaviour;
    private bool m_TrackableDetectedForFirstTime;

    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public bool ToggleOnStateChange
    {
        get { return toggleOnStateChange; }
        set { toggleOnStateChange = value; ToggleComponenets(value);  }
    }

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        if(toggleOnStateChange)
         ToggleComponenets(true);
        //for UIStateManager to know when to play iceberg animation around soda can
        if (!m_TrackableDetectedForFirstTime)
        {
            OnImageTrackableFoundFirstTime.Invoke();
             m_TrackableDetectedForFirstTime = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }

    //On TrackingLost, we reset the position and rotation of the soda can. 
    //This corrects any pose shifting that may have taken place
    private void OnTrackingLost()
    {
        if (toggleOnStateChange)
            ToggleComponenets(false);

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");

   



    }

    void ToggleComponenets(bool enabled)
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
        Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);
        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = enabled;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = enabled;
        }

        //Enable Canvases
        foreach (Canvas component in canvasComponents)
        {
            component.enabled = enabled;
        }
    }
}
