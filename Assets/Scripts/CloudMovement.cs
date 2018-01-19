using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {

    public int direction;
    public float speed;
    Vector3 pos;
    Renderer r;

    bool seen;

	// Use this for initialization
	void Start () {
        pos = transform.position;
        r = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        pos.x += direction * speed * Time.deltaTime;
        transform.position = pos;

        if (r.isVisible)
            seen = true;

        if(seen && !r.isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        direction *= -1;
    }
}
