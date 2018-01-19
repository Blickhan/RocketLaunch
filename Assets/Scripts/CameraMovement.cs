using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    GameObject player_go;
    Transform player;

    float offsetY;

	// Use this for initialization
	void Start () {
        player_go = GameObject.FindGameObjectWithTag("Player");

        if (player_go == null)
        {
            Debug.LogError("Can't find player.");
            return;
        }

        player = player_go.transform;

        offsetY = Camera.main.orthographicSize / 3;

    }
	
	// Update is called once per frame
	void LateUpdate () {
		if(player != null)
        {
            Vector3 pos;
            pos.x = Mathf.Max(transform.position.x, 0);
            pos.y = Mathf.Max(player.position.y + offsetY, transform.position.y);
            pos.z = transform.position.z;
            transform.position = pos;// player.position + offset;
        }
	}
}
