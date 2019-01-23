using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour {


	Object[] myMusic; // declare this as Object array
	AudioSource audio;
	public GameObject text;
	int index=0;

	void Awake () {
		audio=this.GetComponent<AudioSource>();
		myMusic = Resources.LoadAll("Songs",typeof(AudioClip));
		audio.clip = myMusic[0] as AudioClip;
	}
		
	void Update () {
		if (!audio.isPlaying) {
			text.GetComponent<AutoScroll> ().ChangeSong (audio.clip.name);
			playRandomMusic ();
		}
	}

	public void playNext(){
		index++;
		audio.clip = myMusic [index] as AudioClip;
		text.GetComponent<AutoScroll> ().ChangeSong (audio.clip.name);
		audio.Play ();
	}

	void playRandomMusic() {
		index = Random.Range (0, myMusic.Length);
		audio.clip = myMusic[index] as AudioClip;
		audio.Play();
	}

	void Start (){
		text.GetComponent<AutoScroll> ().ChangeSong (audio.clip.name);
		audio.Play(); 
	}

	public void playPrevious(){
		index--;
		audio.clip = myMusic [index] as AudioClip;
		text.GetComponent<AutoScroll> ().ChangeSong (audio.clip.name);
		audio.Play ();
	}
}
