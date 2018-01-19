using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarSpawner : MonoBehaviour {

    public GameObject ShootingStarGO;
    GameObject player_go;
    Transform player;
    RocketMovement rm;
    
    Vector3 stageDimensions;
    int height = 0;
    int spawnHeight;
    int distanceFromPlayer = 5;

    // Use this for initialization
    void Start () {
        stageDimensions = Vector3.right * .5f; // Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        player_go = GameObject.FindGameObjectWithTag("Player");

        if (player_go == null)
        {
            Debug.LogError("Can't find player.");
            return;
        }

        player = player_go.transform;
        rm = player_go.GetComponent<RocketMovement>();

        spawnHeight = Random.Range(10, 25);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        height = Mathf.Max(Score.height, height);
        
        if (height > spawnHeight)
        {
            if (!rm.godMode)
            {
                GameObject shootingStar = (GameObject)Instantiate(ShootingStarGO);

                Vector3 pos;
                pos.x = Random.Range(-stageDimensions.x, stageDimensions.x);
                pos.y = player.position.y + distanceFromPlayer;
                pos.z = 0;

                shootingStar.transform.position = pos;
            }
            spawnHeight += Random.Range(50,100);
        }
    }
}
