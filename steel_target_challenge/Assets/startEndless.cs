using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
public class startEndless : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed = 5;
    public float followAngleThreshold = 45;
    private bool freeze = false;
    private Vector3 initialLocalPos;
    private Vector3 offset;
    private Transform pokeAttachTransform;
    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    //below pertains to my activated/deactivated implementation
    private bool activated = false;
    private bool canChangeState = true; //This name isn't relevant it refers to when the button had two pressed states
    private Vector3 activatedMaxPos;
    private Vector3 activationPressDepth;
    public float buttonHeight;

    public UnityEvent startEndlessEvent;
    public UnityEvent endEndlessEvent;

    public GameObject theWholeButton;

    public bool endlessActive = false;



    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;

        //height of button untouched after activation
        //activatedMaxPos = visualTarget.localPosition;
        //activatedMaxPos.y -= 0.5f * buttonHeight;

        //Activation press depth that causes button to switch states
        activationPressDepth = visualTarget.localPosition;
        activationPressDepth.y -= 0.8f*buttonHeight;


        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze);
    }

    public void Follow(BaseInteractionEventArgs hover) {
        if(hover.interactorObject is XRPokeInteractor) {
            
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;
            freeze = false;
            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if (pokeAngle > followAngleThreshold) {
                isFollowing = false;
                freeze = true;
            }
            //Change state
            // if (offset.y < activationPressDepth.y && canChangeState) {
            //     //activated = !activated;
            //     canChangeState = false;
            //     if (!endlessActive) {
            //         startEndlessEvent.Invoke();
            //         endlessActive = true;
            //     } else {
            //         endEndlessEvent.Invoke();
            //         endlessActive = false;
            //     }
                
            //     //theWholeButton.SetActive(false);
            // }
        }
    }

    public void buttonActivate() {
        if (!endlessActive) {
            startEndlessEvent.Invoke();
            endlessActive = true;
        } else {
            endEndlessEvent.Invoke();
            endlessActive = false;
        }
}

    public void Reset(BaseInteractionEventArgs hover) {
        if (hover.interactorObject is XRPokeInteractor) {
            isFollowing = false;
            freeze = false;
            canChangeState = true;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover) {
        if (hover.interactorObject is XRPokeInteractor) {
            freeze = true;
            //Require loss of contact between poker and button to change state
            //canChangeState = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (freeze) {
            //If the button is frozen, the looping update call will never change the button position
            return;
        }

        if (isFollowing) {

           // if (pokeAttachTransform.position.y >= activationPressDepth.y) {
                //converts the position from world space to local space (the point in question is
                // the contact point between the poke interactor and the poked interactable)
                //REMINDER: world space = object's pos in the whole scene, local space = object's pos relative to other objects
                Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
                //projects the position along the local axis(which is specified as -y in this case)
                Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);
                //re-converts the point along the axis from local back into world space
                visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
            // } else {
            //     freeze = true;
            // }
            
        } else {
            //Changes max button return height based upon "activated" bool.
            // if (activated) {
            //     //Linearly interpolates between points kind of like the bisection method. Every update it does this:
            // //  Let a be current position, b is the target position, and t is the value to interpolate
            // // a = a + (b-a) * t
            // //Every update changes a by the difference between b and a multiplied by the time interval so increments get 
            // //smaller as a gets closer to b
            //     visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, activatedMaxPos, Time.deltaTime * resetSpeed);
                
            // } else {
            //     visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
            // }
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
            
        }
    }
}
