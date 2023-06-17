using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testCustomInput : MonoBehaviour
{
    // Start is called before the first frame update

    public InputActionReference activationReference = null;
    public MeshRenderer activatedMeshReference = null;
    //Action based controller for user reference:
    //InputActionProperty
    private void Awake() {
        activatedMeshReference = GetComponent<MeshRenderer>();
    }

    private void Update() {
        float value = activationReference.action.ReadValue<float>();
        UpdateColor(value);
    }

    private void UpdateColor(float value){
        activatedMeshReference.material.color = new Color(value, value, value);
    }
}
