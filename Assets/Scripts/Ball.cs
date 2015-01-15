using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public AudioClip wallSound;
	public AudioClip clubSound;
	public AudioClip holeWallSound;
	public AudioClip sandSound;


	private Transform objectTouchingBall;

	private float maxOutOfBoundsSeconds = 3.0f;
	private float outOfBoundsTime = 0.0f;
	private bool isOutOfBounds = false;
	private bool resetShot = false;
	private Vector3 shotPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isOutOfBounds) {
			outOfBoundsTime -= Time.deltaTime;
			if (outOfBoundsTime <= 0.0f) {
				resetShot = true;
			}	
		}
	}

	public void StartShot() {
		resetShot = false;
		isOutOfBounds = false;
		shotPosition = transform.position;
	}

	public bool ResetShot() {
		if (resetShot) {
			transform.position = shotPosition;
		}
		return resetShot;
	}
	
	public Transform GetTouchingObject() {
		return objectTouchingBall;
	}
	
	// Keep track of what object our 
	void OnCollisionEnter(Collision collisionInfo) {
		objectTouchingBall = collisionInfo.transform;
		
		Debug.Log ("Ball collides with " + objectTouchingBall.name);
	
		PlayCollisionClip(collisionInfo.transform.tag);

		if (objectTouchingBall.tag == "Out of Bounds") {
			outOfBoundsTime = 3.0f;
			isOutOfBounds = true;
		}

		// TODO: Play a sound if the collisionInfo object has a sound
	}
	
	public void OnTriggerEnter(Collider collider) {
		PlayCollisionClip(collider.transform.tag);
	}
	
	private void PlayCollisionClip(string tag) {
		AudioClip hitClip = null;
		
		switch (tag) {
			case "Walls": hitClip = wallSound; break;
			case "Sand": hitClip = sandSound; break;
			case "Hole": hitClip = holeWallSound; break;
		}
		
		if (hitClip) {
			audio.PlayOneShot(hitClip);
		}
	}
	
	public void Hit() {
		if (clubSound) {
			audio.PlayOneShot(clubSound);
		}
	}
	
	void OnCollisionExit(Collision collisionInfo) {
		if (objectTouchingBall) {
			Debug.Log ("Ball is no longer touching " + objectTouchingBall.name);
		}
		
		//objectTouchingBall = null;
	}
}
