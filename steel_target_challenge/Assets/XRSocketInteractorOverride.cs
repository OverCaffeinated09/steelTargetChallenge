using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRSocketInteractorOverride : XRSocketInteractor
{
    // Start is called before the first frame update
    public string targetTag;

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // GameObject toCheck = interactable.transform.GetComponent<GameObject>();

        return base.CanSelect(interactable);
    }
}
