using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AImovement : Car {

    private List<RecordPoint> record=new List<RecordPoint>();
    private int indexOfRecord=0;
    private RecordPoint actualRecord;
    [SerializeField]
    public string path;
	private int maxTolerance=25;

    void Start () {
        record = ReadFromFile(path);
        actualRecord=record[0];
        rb = GetComponent<Rigidbody>();
        originalMaxSpeed = maxSpeed;
        originalRotation = transform.rotation;
    }

	void FixedUpdate () {
        currentSpeed = rb.velocity.magnitude;
        if (currentSpeed < maxSpeed)
        {
            accelerate();
        }
        if (equalPositions())
        {
            nextRecord();
        }
        else {
            transform.LookAt(new Vector3(actualRecord.x, actualRecord.y, actualRecord.z));
        }
	}
    private void accelerate()
    {
        if (actualRecord.throttle > 0)
            rb.AddForce(transform.forward * maxSpeed * actualRecord.throttle * 200);
    }
	private bool equalPositions()
	{
		Vector3 temp = transform.position;
		if(temp.x < actualRecord.x + maxTolerance && temp.x > actualRecord.x - maxTolerance)
		{
			if (temp.z < actualRecord.z + maxTolerance && temp.z > actualRecord.z - maxTolerance)
			{
				return true;
			}
		}
		return false;
	}

    private void nextRecord()
    {
		if (indexOfRecord < record.Count-1) {
			indexOfRecord++;
			actualRecord = record [indexOfRecord];
		} else {
			maxSpeed = 0;
			maxTolerance = 200;
		}
    }

    public static List<RecordPoint> ReadFromFile(string path)
    {
			
        List<RecordPoint> temp = new List<RecordPoint>();
		TextAsset input = Resources.Load(path) as TextAsset;
		string[] lines = input.text.Split("\n"[0]);

		int count = 0;
        foreach (string line in lines)
        {
            string[] splited = line.Split(',');

            float throttle = float.Parse(splited[0]);
            bool brake= bool.Parse(splited[1]);
            float x = float.Parse(splited[2]);
            float y = float.Parse(splited[3]);
            float z = float.Parse(splited[4]);
			count++;
			temp.Add(new RecordPoint(throttle, brake, x, y, z));

            
        }
        return temp;
    }
	void LateUpdate(){
		GetComponentInParent<AudioSource> ().pitch = Mathf.Pow(currentSpeed/100,3);
	}
    

	void OnTriggerEnter(Collider other) {
		if (other.tag == "finish") {
			GameBoss gb = GameObject.Find ("GameBoss").GetComponent<GameBoss>();
			gb.countOfFinishedCars++;
		}
	}
    
}
