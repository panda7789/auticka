using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour {
	private string scrollingText="";

	public void ChangeSong(string text){
		this.scrollingText = "Now playing: "+text;
		this.GetComponent<Text> ().text = this.scrollingText;
		this.GetComponent<Animator> ().Play ("ScrollingText",-1,0f);
	}

}
