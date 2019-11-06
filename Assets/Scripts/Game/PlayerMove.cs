using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public static PlayerMove instance;
    public static float moveSpeedStart = 1f;
    public float moveSpeed;
    public bool isDead;

    new Collider collider;
    int inground;
    // Use this for initialization
    void Start () {
        Physics.gravity = new Vector3(0, -50, 0);
        collider = GetComponent<Collider>();
        inground = 0;
        instance = this;
        moveSpeed = moveSpeedStart;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (inground > 0) {
            //float mv = moveSpeed+ PlayerPrefs.GetInt("Score") / 100.0f;
            this.transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
            Debug.Log(moveSpeed);
        }

        // Player fallig down
        if (this.GetComponent<Rigidbody>().velocity.y < -30) {
            Debug.Log("Dead");
            isDead = true;
            inground = -1;
        }
    }

    void OnCollisionEnter(Collision col) {
        inground += 1;
        if (col.gameObject.tag == "turn_right")
        {
            gameObject.transform.Rotate(new Vector3(0, 90, 0));
        } else if (col.gameObject.tag == "turn_left") {
            gameObject.transform.Rotate(new Vector3(0, -90, 0));
        }

        // Collision with danger object, GAMEOVER
        if (col.gameObject.tag == "danger") {
            Debug.Log("Dead");
            isDead = true;
            inground = -1;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        inground -= 1;
    }
    const float SPEED_INCREASE_INTERVAL = 10f; // how often do you want the speed to change
    const float SPEED_CHANGE = 0.5f; // how much do you want the speed to change 
    float _IntervalTimer = SPEED_INCREASE_INTERVAL;

    void FixedUpdate()
    {
        if (moveSpeed <= 6)
        {
            _IntervalTimer -= Time.deltaTime;
            if (_IntervalTimer < 0)
            {
                moveSpeed += SPEED_CHANGE;

                _IntervalTimer = SPEED_INCREASE_INTERVAL;
            }
        }
    }


}
