using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setActive : MonoBehaviour
{
    public void deleteSelf() {
        this.gameObject.SetActive(false);
    }
}
