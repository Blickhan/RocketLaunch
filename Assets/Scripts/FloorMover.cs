using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMover : MonoBehaviour {

    Vector3 pos;

    // Use this for initialization
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.y = Mathf.Max(pos.y, transform.position.y);
        transform.position = pos;
    }
}
