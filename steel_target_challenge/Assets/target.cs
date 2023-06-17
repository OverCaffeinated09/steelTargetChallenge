using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class target : MonoBehaviour
{
    // Start is called before the first frame update
    public int targetID;
    public TMP_Text IDDisplay;
    public Transform baseTransform;
    public bool isActive;

    public GameObject toCollideWith;
    public AudioSource audioSource;
    public AudioClip good_sfx;
    public AudioClip bad_sfx;

    
    public UnityEvent onHit;

    public Material unHighlightedMaterial;
    public Material highlightedMaterial;

    public bool booleanThatSetsMaterialOfIsActiveOnce = false;
    void Start()
    {
        IDDisplay.text = targetID.ToString();
        this.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && booleanThatSetsMaterialOfIsActiveOnce == false) {
            this.GetComponent<MeshRenderer>().material = highlightedMaterial;
            booleanThatSetsMaterialOfIsActiveOnce = true;
        }
    }

    void OnCollisionEnter(Collision other) {
        //Debug.Log("Collision");
        // Debug.Log("Collision with bb");
        if (isActive) {
            this.gameObject.SetActive(false);
            onHit.Invoke();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(good_sfx, 1);
            
            
        } else {
            //Debug.Log("Hit inactive target");
            audioSource.PlayOneShot(bad_sfx, 1);
            onHit.Invoke();
        }
        
    }
    
    public void reset() {
        generateNewPos();
        IDDisplay.text = targetID.ToString();
        
        booleanThatSetsMaterialOfIsActiveOnce = false;
        this.GetComponent<MeshRenderer>().material = unHighlightedMaterial;
    }

    private  void generateNewPos() {
        float new_x_pos = Random.Range(-5.0f, 5.0f);
        float new_y_pos = Random.Range(0.5f, 5.0f);
        float new_z_pos = baseTransform.position.z - (0.075f * targetID);
        this.transform.position = new Vector3(new_x_pos, new_y_pos, new_z_pos);
        //Generate x and y randomly within a range 
        //z coordinate is <targetID * increment>
        //This is to ensure that no later target overlaps a target that needs to be eliminated
        //before it.
        
    }
}
