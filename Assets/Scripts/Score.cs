using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    static public int height;
    static public int highestHeight;
    RocketMovement rocket;
    

    void Start()
    {
        GameObject player_go = GameObject.FindGameObjectWithTag("Player");
        if (player_go == null)
        {
            Debug.LogError("Could not find an object with tag 'Player'.");
        }
        rocket = player_go.GetComponent<RocketMovement>();
        height = 0;
        highestHeight = PlayerPrefs.GetInt("highestHeight", 0);
    }
    
    void OnDestroy()
    {
        PlayerPrefs.SetInt("highestHeight", highestHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if(!rocket.dead)
            height = Mathf.Max(Mathf.RoundToInt(rocket.transform.position.y / 2), 0);

        if (height > highestHeight)
        {
            highestHeight = height;
        }

        GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "Highest: " + highestHeight + "\nHeight: " + height;
    }
}
