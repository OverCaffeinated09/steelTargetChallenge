using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class dumpPouch : MonoBehaviour
{
    // Start is called before the first frame update
    public Magazine reloadable;
    public Transform reloadableSpawnTransform;

    private XRBaseInteractable interactable;
    bool initialSpawn = false;
    void Start()
    {
        //interactable.hoverExited.AddListener(spawnAnotherReloadable);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (initialSpawn == false) {
            Magazine newObject = Instantiate(reloadable, reloadableSpawnTransform.position, reloadableSpawnTransform.rotation);
            Magazine newObject2 = Instantiate(reloadable, reloadableSpawnTransform.position, reloadableSpawnTransform.rotation);

            //newObject.setCurrCap(17);
            //newObject2.setCurrCap(17);
            initialSpawn = true;
            
            
        }
    }
    
    //Setter and getter for type of reloadable stored in the dump pouch
    public void setReloadable(Magazine objectToSet) {
        reloadable = objectToSet;
    }

    public Magazine getReloadable() {
        return reloadable;
    }

    //Spawn another reloadable upon hover exit

    public void spawnAnotherReloadable(BaseInteractionEventArgs hover) {
        //First check if sufficient amount of "mana"

        Magazine newObject = Instantiate(reloadable, reloadableSpawnTransform.position, reloadableSpawnTransform.rotation);
    }

}
