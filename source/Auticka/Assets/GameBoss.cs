using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoss : MonoBehaviour {
	public int NumberOfPlayers;
	private bool intro = true;
	public GameObject logo;
	public GameObject pressAnyKey;
	public GameObject cam;
	public GameObject position;
	public CameraControll camControll;
	private List<Car> cars = new List<Car> ();
	private Text countdown;
	private int seconds = 3;
	public Transform[] prefabCarArray;
	public string[] paths;
	private GameObject player;
	public GameObject debug;
	public AudioClip startSound;
	public int countOfFinishedCars = 0;
	//-528 -400
	//-888 -855
	void Awake () {
		for (int i = 0; i < NumberOfPlayers; i++) {
			var temp = Instantiate (prefabCarArray[Random.Range(0,prefabCarArray.Length)], new Vector3 (-528+20*i, 2.5f, Random.Range(-888,-855)), Quaternion.identity);
			temp.name = "car" + i;
			temp.GetComponent<AImovement> ().path = paths [Random.Range (0, paths.Length)];
			temp.eulerAngles = new Vector3 (0, -90);
			cars.Add (GameObject.Find ("car"+i).GetComponent<Car>());
			player = GameObject.Find ("car");
		}
		countdown=GameObject.Find("countdown").GetComponent<Text>();
		camControll = cam.GetComponent<CameraControll> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (intro) {
			if (Input.anyKey) {
				intro = false;
				countdown.enabled = true;
				logo.gameObject.SetActive (false);
				pressAnyKey.gameObject.SetActive (false);
				camControll.StartGame ();
				startCountdown ();
			}
		}
	}

	void startCountdown(){
		player.GetComponent<Car> ().GetComponent<AudioSource> ().enabled = true;
		foreach (Car c in cars) {
			c.GetComponent<AudioSource> ().enabled = true;
		}
		InvokeRepeating ("Countdown", 1.0f, 1.0f);
		this.GetComponent<AudioSource>().Play ();
	}
	void Countdown () {
		AudioSource audioSource = this.GetComponent<AudioSource> ();
		if (--seconds == 0) {
			CancelInvoke ("Countdown");
			countdown.enabled = false;
			StartRace ();
			audioSource.clip = startSound;
			audioSource.Play ();
		}
		countdown.text = seconds.ToString();
		audioSource.Play ();
		increaseVolume ();
	}

	void StartRace(){
		GameObject.Find("Camera").GetComponent<CameraControll> ().enabled = true;
		player.GetComponent<Car> ().GetComponent<movement>().enabled = true;

		foreach (Car c in cars) {
			c.GetComponent<AImovement> ().enabled = true;
		}
	}
	private void increaseVolume(){
		player.GetComponent<Car> ().GetComponent<AudioSource> ().volume = 1-seconds*0.25f;
		foreach (Car c in cars) {
			c.GetComponent<AudioSource> ().volume=1-seconds*0.20f;
		}
	}

	public void ShowDebug(){
		if (debug.activeSelf)
			debug.SetActive (false);
		else
			debug.SetActive (true);
	}

	public void UpdatePosition(){
		position.SetActive (true);
		position.GetComponent<Text> ().text = "You finished <color=\"red\">"+countOfFinishedCars+".</color>";
		player.GetComponent<Car> ().GetComponent<movement>().enabled = false;
		camControll.EndGame ();
	}
}
