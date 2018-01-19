using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RocketMovement : MonoBehaviour {

    Rigidbody2D rb2D;

    public float maxVelocity;
    //public float accelerationPower;
    static float baseAccelerationPower = 8.25f;
    float accelerationPower = baseAccelerationPower;
    float maxAccelerationPower = 12f;
    bool isAccelerating = false;

    Animator animator;
    public Animator clickAnimation;

    public GameObject ShittyExplosionGO;

    public bool dead = false;
    float deathCooldown;

    float clickLocation;
    public float TURN_POWER;
    float turnPower;
    bool changedDirection;
    int moveDirection = 1;
       
    public float POWERUP_TIME;
    float powerupTime;
    public bool godMode = false;
    float godModeFactor = 2f;
    bool powerupAcquired = false;
    
    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = transform.GetComponentInChildren<Animator>();

        turnPower = TURN_POWER;
        powerupTime = POWERUP_TIME;
    }

    // Do graphics and input updates here
    void Update()
    {
        if (dead)
        {
            deathCooldown -= Time.deltaTime;

            if (deathCooldown <= 0)
            {
                foreach (SpriteRenderer r in GameObject.FindGameObjectWithTag("RestartScreen").GetComponentsInChildren<SpriteRenderer>())
                {
                    r.enabled = true;
                }
                clickAnimation.SetTrigger("Show");

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    foreach (SpriteRenderer r in GameObject.FindGameObjectWithTag("RestartScreen").GetComponentsInChildren<SpriteRenderer>())
                    {
                        r.enabled = false;
                    }
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
        else
        {            

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                // set flag
                isAccelerating = true;

                // move side to side
                clickLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
                                
                if (clickLocation > transform.position.x)
                {
                    if (moveDirection != 1)
                    {
                        // set flag
                        changedDirection = true;
                    }
                    moveDirection = 1;
                }
                else if (clickLocation < transform.position.x)
                {
                    if (moveDirection != -1)
                    {
                        // set flag
                        changedDirection = true;
                    }
                    moveDirection = -1;
                }
                
                // reduce shakiness of the rocket
                if (Mathf.Abs(clickLocation - transform.position.x) < .05)
                {
                    turnPower = 3f;
                }
                else
                {
                    turnPower = TURN_POWER;
                }
                
            }
            else
            {
                // reset flag
                isAccelerating = false;
            }

            if (!godMode)
            {
                accelerationPower = baseAccelerationPower + Mathf.Max(0, (maxAccelerationPower - baseAccelerationPower) * transform.position.y / 2000);

                if (powerupAcquired)
                {
                    godMode = true;
                    powerupAcquired = false;
                    accelerationPower = accelerationPower * godModeFactor;
                    transform.Find("glow").GetComponent<SpriteRenderer>().enabled = true;

                    
                    //GameObject.FindGameObjectWithTag("PowerupSliderBackground").GetComponent<Image>().color = new Color(1, .91f, .55f);
                }
            }
            else
            {
                powerupTime -= Time.deltaTime;

                if (powerupTime < 0)
                {
                    godMode = false;
                    powerupTime = POWERUP_TIME;
                    accelerationPower = accelerationPower / godModeFactor;
                    transform.Find("glow").GetComponent<SpriteRenderer>().enabled = false;
                    if (gameObject.GetComponent<Rigidbody2D>().velocity.y > maxVelocity)
                    {
                        Vector2 v = gameObject.GetComponent<Rigidbody2D>().velocity;
                        v.y = maxVelocity / 2;
                        gameObject.GetComponent<Rigidbody2D>().velocity = v;
                    }
                }
            }

        }
    }

    // do physics engine updates here
    void FixedUpdate()
    {
        

        if (dead)
            return;

        if (isAccelerating)
        {
            Vector2 v = rb2D.velocity;

            if (changedDirection)
            {
                v.x = 0;
                changedDirection = false;
            }

            rb2D.AddForce(Vector2.right * turnPower * moveDirection);
            
            rb2D.AddForce(Vector2.up * accelerationPower);
            
            if(!godMode)
                v.y = Mathf.Min(rb2D.velocity.y, maxVelocity);

            rb2D.velocity = v;

            if (godMode)
                animator.SetTrigger("IsSuperAccelerating");
            else
                animator.SetTrigger("IsAccelerating");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (godMode)
        //    return;

        if (!dead)
        {
            // (collision.relativeVelocity.magnitude > 5 && collision.gameObject.tag != "Boundary")
            if ((collision.gameObject.tag == "Asteroid" && !godMode) || collision.gameObject.tag == "Floor")
            {
                gameObject.GetComponentInChildren<Renderer>().enabled = false;
                transform.Find("glow").GetComponent<SpriteRenderer>().enabled = false;
                animator.SetTrigger("Dead");
                PlayExplosion();
                dead = true;
                deathCooldown = .75f;
            }
            else if(collision.gameObject.tag == "ShootingStar")
            {
                GameObject.Destroy(collision.gameObject);
                powerupAcquired = true;
            }
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ShittyExplosionGO);
        explosion.transform.position = transform.position;
    }
}
