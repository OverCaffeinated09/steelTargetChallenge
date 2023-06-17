using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class chooseCorrectHand : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public Transform leftHandAttach;
    public Transform rightHandAttach;

    public GameObject leftHandInteractor;
    public GameObject rightHandInteractor;
    private bool isGrabbed = false;

    void Start() {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void SwapHands() {
        if (grabInteractable.hoveringInteractors[0].name == leftHandInteractor.name
            ) {
            grabInteractable.attachTransform = leftHandAttach;
            //Debug.Log("LEFT");
        }
        if (grabInteractable.hoveringInteractors[0].name == rightHandInteractor.name
            ) {
            grabInteractable.attachTransform = rightHandAttach;
            //Debug.Log("RIGHT");
        }
    }
}
