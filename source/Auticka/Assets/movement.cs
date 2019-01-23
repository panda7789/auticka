using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : Car{
	
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMaxSpeed = maxSpeed;
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = rb.velocity.magnitude;
        float throttleForce = Input.GetAxis("Vertical");
        if (currentSpeed < maxSpeed)
        {
            rb.AddForce(transform.forward * maxSpeed * throttleForce *200);
        }
        brake = (throttleForce<0) ? true:false;
        if (brake)
        {
            rb.drag = 1.5f;
        }
        else rb.drag = 1;
        rb.transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
		GetComponentInParent<AudioSource> ().pitch = currentSpeed/80;
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag == "checkpoint") {
			GameObject.Find("start").transform.tag = "finish";
		}
        if (other.tag == "start")
        {
            GameObject.Find("AI").GetComponent<AI>().StartRecording();
           
        }
        else if (other.tag == "finish")
        {
            GameObject.Find("AI").GetComponent<AI>().StopRecording();
			GameBoss gb = GameObject.Find ("GameBoss").GetComponent<GameBoss>();
			gb.countOfFinishedCars++;
			gb.UpdatePosition ();
        }
    }

}






