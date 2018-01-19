using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenScript : MonoBehaviour {

    static bool sawOnce = false;

    // Use this for initialization
    void Start()
    {
        if (!sawOnce)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        sawOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
