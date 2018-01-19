using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject SmallAsteroidGO;
    public GameObject MediumAsteroidGO;
    public GameObject LargeAsteroidGO;
    GameObject player_go;
    Transform player;
    RocketMovement rm;

    int rng;    
    Vector3 stageDimensions;
    int height = 0;
    int spawnInterval = 5;
    int spawnHeight = 3;
    int distanceFromPlayer = 8;

    // Use this for initialization
    void Start () {
        stageDimensions = Vector3.right * .5f;//Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        player_go = GameObject.FindGameObjectWithTag("Player");

        if (player_go == null)
        {
            Debug.LogError("Can't find player.");
            return;
        }

        player = player_go.transform;
        rm = player_go.GetComponent<RocketMovement>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        height = Mathf.Max(Score.height, height);
        
        if (height > spawnHeight)
        {
            if (!rm.godMode)
            {
                GameObject asteroid;

                rng = Random.Range(0, 3);

                if (rng == 0)
                    asteroid = (GameObject)Instantiate(SmallAsteroidGO);
                else if (rng == 1)
                    asteroid = (GameObject)Instantiate(MediumAsteroidGO);
                else
                    asteroid = (GameObject)Instantiate(LargeAsteroidGO);

                Vector3 pos;
                pos.x = Random.Range(-stageDimensions.x, stageDimensions.x);
                pos.y = player.position.y + distanceFromPlayer;
                pos.z = 0;

                asteroid.transform.position = pos;
            }
            spawnHeight += spawnInterval;
        }
    }
}
