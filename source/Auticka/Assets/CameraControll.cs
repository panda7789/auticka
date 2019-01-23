using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

    public GameObject player;
	Vector3 offset;

	void Start() {
		offset = new Vector3 (0, -30, 45);
	}

	void LateUpdate () {
		float desiredAngle = player.transform.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = player.transform.position - (rotation * offset);
		transform.LookAt (player.transform);
	}
	public void StartGame(){
		StartCoroutine(AnimationToStart());
	}
	public IEnumerator AnimationToStart(){
		this.GetComponent<Animator> ().CrossFadeInFixedTime ("toStart", 3f);
		yield return new WaitForSeconds(3);
		this.GetComponent<Animator> ().enabled = false;
	}
	public void EndGame(){
		this.GetComponent<Animator> ().enabled = true;
		this.GetComponent<Animator> ().CrossFadeInFixedTime ("toEnd", 3f);
		this.enabled = false;
	}

}
