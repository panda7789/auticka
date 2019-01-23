using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

	[SerializeField]
    protected float maxSpeed;
    protected float originalMaxSpeed;
	[SerializeField]
    protected float currentSpeed;
	[SerializeField]
    protected bool brake;
	[SerializeField]
    protected bool offroad;
    protected Rigidbody rb;
    protected Quaternion originalRotation;

    void Start()
    {
        
        originalMaxSpeed = maxSpeed;
        originalRotation = transform.rotation;
    }

    void FixedUpdate()
    {
		
        if (brake)
        {
            rb.drag = 1.5f;
        }
        else rb.drag = 1;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "offroad")
        {
            maxSpeed = originalMaxSpeed / 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "offroad")
        {
            maxSpeed = originalMaxSpeed;
        }
    }
	public bool getBrake(){
		return brake;
	}

}
