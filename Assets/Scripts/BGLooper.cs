using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLooper : MonoBehaviour {

    int numBGPanels = 3;

    void OnTriggerEnter2D(Collider2D collider)
    {
        float heightOfBGObject = ((BoxCollider2D)collider).size.y;

        Vector3 pos = collider.transform.position;

        pos.y += heightOfBGObject * numBGPanels;

        collider.transform.position = pos;
    }
}
