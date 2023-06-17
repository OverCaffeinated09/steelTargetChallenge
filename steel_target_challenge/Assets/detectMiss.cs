using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class detectMiss : MonoBehaviour
{
    public UnityEvent onHit;
    public AudioSource audioSource;
    public AudioClip miss_sfx;
    private bool hits_enabled;

    void Start() {
        hits_enabled = true;
    }
   void OnCollisionEnter(Collision other) {
        if (hits_enabled == true) {
            //Debug.Log("Collision triggered");
            audioSource.PlayOneShot(miss_sfx, 1);
            onHit.Invoke();
        }
            
    }

    public void temporarilyDisableCollision() {
        //This method is for the shotgun so that a target hit will 
        //not count the other non-deleted pellets as misses
        //as the hitting pellet is deleted on target hit
        StartCoroutine(disableHits());

    }
    IEnumerator disableHits() {
        hits_enabled = false;
        yield return  new WaitForSeconds(1f);
        hits_enabled = true;
    }
}
