using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordPoint {
	public float throttle;
	public bool brake;
	public float x;
	public float y;
	public float z;


	public RecordPoint(float throttle, bool brake, float x, float y,float z)
	{
		this.throttle = throttle;
		this.brake = brake;
		this.x = x;
		this.y = y;
		this.z = z;

	}

	public RecordPoint(float throttle, bool brake, Vector3 position)
	{
		this.throttle = throttle;
		this.brake = brake;
		this.x = position.x;
		this.y = position.y;
		this.z = position.z;
	}

	override
	public string ToString()
	{
		return throttle+","+brake+","+x+","+y+","+z +","+ "\n";
	}


}