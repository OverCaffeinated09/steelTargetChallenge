using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class successfulPump : MonoBehaviour
{
    public float threshold = 0.025f;
    public Transform backTarget;
    public Transform frontTarget;
    public Transform currPos;
    public UnityEvent successfulPumpBack;
    public UnityEvent successfulPumpForward;

    private bool lastPositionWasForward = true;

    private bool wasReachedPreviously = false;

    public AudioSource audioSource;
    public AudioClip pumpSFX;
    private void FixedUpdate() {
        if (lastPositionWasForward) {
            checkForPumpBack();
        } else {
            checkForPumpForward();
        }

        
    }

    private void checkForPumpBack() {
        float distance = Vector3.Distance(currPos.position, backTarget.position);
        //Debug.Log(distance);
        if (distance < threshold && wasReachedPreviously == false) {
            //Reached the target
            audioSource.PlayOneShot(pumpSFX, 1);
           // Debug.Log("PUMP_BACK");
            successfulPumpBack.Invoke();
            wasReachedPreviously = true;
            lastPositionWasForward = false;
        } else if (distance >= threshold) {
            wasReachedPreviously = false;
        }
    }

    private void checkForPumpForward() {
        float distance = Vector3.Distance(currPos.position, frontTarget.position);
        //Debug.Log(distance);
        if (distance < threshold && wasReachedPreviously == false) {
            //Reached the target
            audioSource.PlayOneShot(pumpSFX, 1);
            //Debug.Log("PUMP_FORWARD");
            successfulPumpForward.Invoke();
            wasReachedPreviously = true;
            lastPositionWasForward = true;
        } else if (distance >= threshold) {
            wasReachedPreviously = false;
        }
    }
}
