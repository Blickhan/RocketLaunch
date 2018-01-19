using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingStarMovement : MonoBehaviour {

    Rigidbody2D rb2D;
    Renderer r;

    public Image WarningUI;
    Image warning;

    float accelerationPowerX;
    float maxPowerX = 5;
    float velocityY = 3;
    //float maxPowerY = 5;

    // Use this for initialization
    void Start()
    {

        r = GetComponentInChildren<Renderer>();
        rb2D = GetComponent<Rigidbody2D>();


        accelerationPowerX = Random.Range(-maxPowerX, maxPowerX);
        //velocityY = Random.Range(maxPowerY, maxPowerY);

        rb2D.AddForce(Vector2.right * accelerationPowerX);
        rb2D.velocity = rb2D.velocity + Vector2.up * velocityY;

        // create offscreen asteroid Warning
        warning = (Image)Instantiate(WarningUI);
        warning.transform.SetParent(GameObject.Find("Canvas").transform, false);

        Vector3 pos = warning.rectTransform.position;
        pos.x = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 0, 0)).x;
        warning.rectTransform.position = pos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (warning != null)
        {
            if (!r.isVisible)
            {
                Vector3 pos = warning.rectTransform.position;
                pos.x = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 0, 0)).x;
                warning.rectTransform.position = pos;

                Vector3 asteroidVelocity = rb2D.velocity;// Camera.main.WorldToScreenPoint(rb2D.velocity);
                asteroidVelocity.y = -Mathf.Abs(asteroidVelocity.y);
                //Debug.Log("x: " + asteroidVelocity.x + ", y: " + asteroidVelocity.y);
                warning.rectTransform.rotation = Quaternion.LookRotation(Vector3.forward, asteroidVelocity + Vector3.down * 2);
            }
            else
            {
                Destroy(warning.gameObject);
            }
        }


    }
}
