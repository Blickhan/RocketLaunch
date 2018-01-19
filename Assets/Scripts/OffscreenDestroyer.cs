using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffscreenDestroyer : MonoBehaviour {

    bool seen;
    Renderer r;    

	// Use this for initialization
	void Start () {
        seen = false;
        r = GetComponentInChildren<Renderer>();
    }	
    
	// Update is called once per frame
	void Update () {
        if (r.isVisible)
        {
            seen = true;
        }

        if (seen && !r.isVisible)
        {
            Destroy(gameObject);
        }

        
    }
}
