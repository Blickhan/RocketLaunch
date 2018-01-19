using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    Animator anim;
    bool seen = false;
    float duration = 0;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("IsAccelerating");
	}
	
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            EndSplashScreen();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {


        Vector3 pos = transform.position;
        pos.y += 1f * Time.deltaTime;
        transform.position = pos;


        if (transform.GetComponentInChildren<Renderer>().isVisible)
        {
            
            seen = true;
        }
        else if(!transform.GetComponentInChildren<Renderer>().isVisible && seen)
        {
            if (duration > .25f)
            {
                EndSplashScreen();
            }
            else
            {
                duration += Time.deltaTime;
            }
        }
	}

    void EndSplashScreen()
    {
        SceneManager.LoadScene("Game");
        GameObject.Destroy(gameObject);
    }
    
}
