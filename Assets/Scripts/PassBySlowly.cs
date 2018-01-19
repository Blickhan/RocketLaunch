using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBySlowly : MonoBehaviour {

    Rigidbody2D player;
    float playerHeight;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .9f);

        GameObject player_go = GameObject.FindGameObjectWithTag("Player");

        if (player_go == null)
        {
            Debug.LogError("Couldn't find an object with tag 'Player'!");
            return;
        }

        player = player_go.GetComponent<Rigidbody2D>();
        playerHeight = player.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (player.position.y > transform.position.y - 4 && player.transform.position.y > playerHeight)
        {
            float vel = Mathf.Max(0, player.velocity.y * .95f);

            transform.position = transform.position + Vector3.up * vel * Time.deltaTime;
            playerHeight = player.transform.position.y;
        }
    }
}
