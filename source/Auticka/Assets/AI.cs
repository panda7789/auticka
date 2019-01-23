using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour {

    public GameObject recordedPlayer;
    public GameObject player;
    public GameObject pointPrefab;
	public GameObject positionPrefab;
    public InputField input;
    private List<RecordPoint> record;
    private bool recording;
    private int frame = 60;
	private movement mov;
    public bool canRecord;
	public string pathName;


	void Start () {
        record = new List<RecordPoint>();
        mov = recordedPlayer.GetComponent<movement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (frame % 10 == 0)
        {
            frame = 0;
            if (recording)
            {

                //record.Add(new RecordPoint(Input.GetAxis("Vertical"),mov.brake,recordedPlayer.transform.position.x,recordedPlayer.transform.position.y,recordedPlayer.transform.position.z));
				record.Add(new RecordPoint(Input.GetAxis("Vertical"), mov.getBrake(), recordedPlayer.transform.position));
                //DebugPanel.Log("RecordPoint", "RecordPoints", record.ToArray());
            }
        }
        frame++;
	}

    public void StartRecording()
    {
        if(canRecord)
        recording = true;
    }

    public void StopRecording()
    {
		if (recording) {
			recording = false;
			string temp = "";
			foreach (RecordPoint rec in record) {
				temp += rec.ToString ();
			}
			File.WriteAllText (pathName, temp);
		}
    }

    public void ShowPointsOnMap()
    {
        List<RecordPoint> temp = new List<RecordPoint>();
        temp=AImovement.ReadFromFile(input.text);
        foreach(RecordPoint rp in temp)
        {
            Instantiate(pointPrefab, new Vector3(rp.x, rp.y, rp.z), Quaternion.identity);
        }
        
    }
}


