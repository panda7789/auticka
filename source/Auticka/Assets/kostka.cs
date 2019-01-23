using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kostka : MonoBehaviour {
    int hodnota;

    public int Hodnota{
        get { return hodnota; }
        private set {
            if (value > 0 && value < 7) hodnota = value;
        }
    }

    public void hodKostkou()
    {
        Hodnota=Random.Range(1, 7);
        Debug.Log(Hodnota);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hodKostkou();

        }
    }
}
