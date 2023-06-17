using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class manageGameplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject weapon;
    public dumpPouch dumpPouch;

    public Magazine reloadable;


    void Start()
    {
        // dumpPouch.setReloadable(reloadable);
        // Debug.Log(reloadable.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
