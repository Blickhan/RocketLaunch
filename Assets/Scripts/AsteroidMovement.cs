using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMovement : MonoBehaviour {

    Rigidbody2D rb2D;
    Renderer r;

    public Image WarningUI;
    Image warning;

    float accelerationPowerX;
    float maxPowerX = 50;
    float velocityY;
    float maxPowerY = 1;
    int rotationDirection;

    // Use this for initialization
    void Start () {

        r = GetComponentInChildren<Renderer>();
        rb2D = GetComponent<Rigidbody2D>();


        accelerationPowerX = Random.Range(-maxPowerX, maxPowerX);
        velocityY = Random.Range(-maxPowerY, -maxPowerY / 2);

        rb2D.AddForce(Vector2.right * accelerationPowerX);
        rb2D.velocity = rb2D.velocity + Vector2.up * velocityY;

        rotationDirection = Random.value > .5 ? -1 : 1;

        // create offscreen asteroid Warning
        warning = (Image)Instantiate(WarningUI);
        warning.transform.SetParent(GameObject.Find("Canvas").transform, false);

        Vector3 pos = warning.rectTransform.position;
        pos.x = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, 0, 0)).x;
        warning.rectTransform.position = pos;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        rb2D.MoveRotation(rb2D.rotation + (rotationDirection * accelerationPowerX * 20) * Time.fixedDeltaTime);

        if (warning != null) {
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
