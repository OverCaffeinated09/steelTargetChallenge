using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class pumpFireControl : MonoBehaviour
{
    public GameObject projectilePrefab = null;
    public Transform startPoint;
    
    private bool roundChambered = false;

    public float launchSpeed = 1.0f;

    public GameObject grabCenter;

    public float recoilForce = 0;
    
    bool backPumpReached = false;
    bool frontPumpReached = false;

    private int shellCount = 0;

    public XRBaseInteractor socketInteractor;

    public AudioSource audioSource;
    public AudioClip fireSFX;
    public AudioClip emptySFX;
    public AudioClip reloadSFX;

    public GameObject[] shotLoad;

    public GameObject projectileCollider;

    public XRGrabInteractable bodyGrip;
    public GameObject leftHandGrabInteractor;
    public GameObject rightHandGrabInteractor;
    public GameObject grabCenter_l;
    public GameObject grabCenter_r;

    public Transform grabAttach_l;
    public Transform grabAttach_r;

    void Start() {
        for (int i = 0; i < 8; i++) {
            shotLoad = new GameObject[8];
        }
    }

    public void Fire() {
        //Debug.Log("Fire() called");

        
        if (roundChambered && shellCount > 0) {
            for (int i = 0; i < 8; i++) {
                float randXDel = Random.Range(-0.15f, 0.15f);
                float randYDel = Random.Range(-0.15f, 0.15f);
                Vector3 posToApply = startPoint.position;
                posToApply.x += randXDel;
                posToApply.y += randYDel;
                GameObject newObject = Instantiate(projectilePrefab, posToApply, startPoint.rotation);
                shotLoad[i] = newObject;
                Destroy(newObject, 5);
                Rigidbody rigidBody = newObject.GetComponent<Rigidbody>();
                ApplyForce(rigidBody);
                audioSource.PlayOneShot(fireSFX, 1);
                recoil(grabCenter.GetComponent<Rigidbody>());
            }
            GameObject collider = Instantiate(projectileCollider, startPoint.position, startPoint.rotation);
            collider.transform.Rotate(90f, 0f, 0f);
            Destroy(collider, 5);
            Rigidbody collider_rb = collider.GetComponent<Rigidbody>();
            ApplyForce(collider_rb);
            audioSource.PlayOneShot(fireSFX, 1);
            recoil(grabCenter.GetComponent<Rigidbody>());

            backPumpReached = false;
            frontPumpReached = false;
            roundChambered = false;
            shellCount--;
                
            
        } else {
            audioSource.PlayOneShot(emptySFX,1);
            // if (magazine == null) {
            //     Debug.Log("Null Magazine");
            // } else{
            //     Debug.Log("Magazine has a capacity of " + magazine.getCurrCap());

            // }

            if (roundChambered == false) {
                //Debug.Log("Round was never chambered");
            }
        }
    }

    public void deleteShotLoad() {
        for (int i = 0; i < 8; i++) {
            if (shotLoad[i] != null) {
                //Debug.Log("destroyed pellet");
                shotLoad[i].SetActive(false);
            } else {
                //Debug.Log("pellet null");
            }
            
        }
    }
    private void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = startPoint.forward * -launchSpeed;
        rigidBody.AddForce(force);
        // Debug.Log(force);
        

    }
    private void recoil(Rigidbody rigidBody) {
        float multiplier = 0.25f;
        Vector3 force = startPoint.forward * recoilForce * multiplier;
        force += startPoint.right * recoilForce * 0.25f * multiplier;
        Vector3 torque = startPoint.up * -100000000f * multiplier;
        rigidBody.AddTorque(torque);
        rigidBody.AddForce(force);
    }

    public void receiveBackPumpSignal() {
        if (frontPumpReached == false) {
            //Debug.Log("back pump signal received");
            backPumpReached = true;
            
        }
        
    }

    public void receiveFrontPumpSignal() {
        if (backPumpReached == true) {
            //Debug.Log("Front pump signal received");
            frontPumpReached = true;
            if (shellCount > 0) {
                roundChambered = true;
            }
            
        }
        //For the case where there is no shell,
        //reset able to fire status to 0 even
        //though pump sequence is completed
        if (shellCount == 0) {
            
        }
    }

    public void insertShell() {
        //magazine = obj.transform.GetComponent<Magazine>();
        IXRSelectInteractable obj = socketInteractor.GetOldestInteractableSelected();
        GameObject shell = obj.transform.gameObject;
        audioSource.PlayOneShot(reloadSFX, 1);
        if (shell == null) {
            //Debug.Log("Shell is null");
        }
        //Debug.Log(shell.transform.name + " in socket of " + transform.name);
        shellCount += 1;
        Destroy(shell);
    }

    public void determineWhichHandRecoils() {
         if (bodyGrip.hoveringInteractors[0].name == leftHandGrabInteractor.name
            ) {
            grabCenter = grabCenter_l;
            bodyGrip.attachTransform = grabAttach_l;
        }
        if (bodyGrip.hoveringInteractors[0].name == rightHandGrabInteractor.name
            ) {
            grabCenter = grabCenter_r;
            bodyGrip.attachTransform = grabAttach_r;
        }
    }
}
