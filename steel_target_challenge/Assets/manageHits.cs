using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class manageHits : MonoBehaviour
{

    public TMP_Text remainingHitsText;
    private int remainingHits;
    // Start is called before the first frame update
    void Start()
    {
        remainingHits = Random.Range(0, 9);
        remainingHitsText.text = remainingHits.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
