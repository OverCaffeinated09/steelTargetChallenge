//Libraries:
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

//Other Scripts:
// using Mag;

//See this: keep in mind he doesn't know how a gun works:
//https://www.youtube.com/watch?v=gmaAK_BXC4c&ab_channel=Valem

public class ShootProjectile : MonoBehaviour
{
    //IO:
    //Input:
    //- User Presses trigger
    //- User inserts magazine
    //- User ejects mag
    //- User hits slide release or power strokes (implement attach point at back of slide using xr 
    //- User pulls slide back to chamber round (EDGE_CASE: Slide should stay locked back if mag is empty. Should eject round if chambered)
    
    //Output:
    //- Gun fires
    //- Gun doesn't fire/trigger is disabled
    //- Gun chambers another round
    //- Gun slide locks back on last shot

    //Gun as a game object variables
    // public GameObject proj_prefab = null; //Edit this from the interface
    // public Transform proj_origin = null; //ditto
    // public float launch_speed = 1.0f; //Might want to change to not be forced based but velocity based as speed is dependent on proj weight

    
    // //Ammo handling variables:
    // public HandleMagCapacity curr_mag = null; //Set to game object that is detected in the xr socket
    // private bool round_chambered = true; //Indicates if a round is chambered
    // private bool mag_follower = false; //A follower boolean that tells the gun the magazine is empty and the last round is chambered
    
    // //Gun manipulation variables:
    // private bool slide_locked_back = false; // change to true when mag is empty and the chambered round is fired
    // private bool trigger_reset = true; //Only changed to false if trigger is pulled and round isn't fired -> reset to true when slide is manipulated


    // public void setCurrMag(HandleMagCapacity insertedMag) {
    //     curr_mag = insertedMag;
    // }

    // public void ejectMag() {
    //     curr_mag = null;
    // }

    // public void Fire() {
    //     //Requirements to fire:
    //     // - round_chambered = true
    //     // - slide_locked_back = false
    //     // - Note that currMag can be null as a chambered round can still fire without mag inserted
    //     // - Note that slide_locked_back handles the case where a mag is inserted but slide is still locked back(a round isn't chambered in this case)
    //     // - slide_locked_back can only be disabled by the releaseSlide function
    //     // GameObject newObject = Instantiate(proj_prefab, proj_origin.position, proj_origin.rotation);
    //     // if (round_chambered == true) {
    //     //     if (newObject.TryGetComponent(out Rigidbody rigidBody)) {
    //     //         ApplyForce(rigidBody);
    //     //     } else {
    //     //         Debug.Log("Failure to fire");
    //     //     }
    //     //     if (curr_mag != null && curr_mag.getCurrCap() > 0) {
    //     //         //round_chambered remains true
    //     //         curr_mag.decrement();
    //     //         //check if mag is empty
    //     //         if (curr_mag.getCurrCap() == 0) {
    //     //             mag_follower = true;
    //     //         }
    //     //     } else {
    //     //         //Mag is inserted but empty, the last chambered round is fired and no round replaces it
    //     //         round_chambered = false;
    //     //         //fire last shot
    //     //         if (newObject.TryGetComponent(out Rigidbody rigidBody)) {
    //     //             ApplyForce(rigidBody);
    //     //         } else {
    //     //             Debug.Log("Failure to fire");
    //     //         }

    //     //         slide_locked_back = true;
    //     //         lockSlideBack();
    //     //     }
    //     // } else {
    //     //     //round isn't chambered but trigger is pulled
    //     //     trigger_reset = false;
    //     // }
    // }

    // private void ApplyForce(Rigidbody rigidBody) {
    //     Vector3 force = proj_origin.forward * launch_speed;
    //     rigidBody.AddForce(force);
    //     Debug.Log(force);
    // }

    // private void lockSlideBack() {
    //     //Lock slide back 
    //     //creating this function to change the state of the gun to its locked back state
    // }

    // private void releaseSlide() {
    //     //only called when gun is power_stroked or slide/bolt release is pressed while slide
    //     //Switch state to slide forward
    //     // if (curr_mag != null && curr_mag.getCurrCap > 0) {
    //     //     round_chambered = true;
    //     // }
    //     // slide_locked_back = false;
    // }
}
