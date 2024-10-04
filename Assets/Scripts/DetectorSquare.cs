using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorSquare : MonoBehaviour
{
 
    void OnTriggerEnter2D
        (Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log ("The Player has been detected");
        }
    }
}
