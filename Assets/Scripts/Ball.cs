using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public AudioClip wallSound;
	public AudioClip clubSound;
	public AudioClip holeWallSound;
	public AudioClip sandSound;
	public AudioClip waterSound;


	private bool restingInBounds = true;

	private float maxOutOfBoundsSeconds = 3.0f;
	private float outOfBoundsTime = 0.0f;
	private bool isOutOfBounds = false;
	private bool resetShot = false;
	private Vector3 shotPosition;
	private Vector3? warpPosition = null;
	
	// Use this for initialization
	void Start () {

	}
	
	public void SetWarpPosition(Vector3 pos) {
		warpPosition = pos;
	}
	
	void FixedUpdate() {
		if (warpPosition != null) {
			transform.position = (Vector3) warpPosition;
			warpPosition = null;
		}
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
		warpPosition = null;
	}

	public bool ResetShot() {
		if (resetShot) {
			transform.position = shotPosition;
		}
		return resetShot;
	}
	
	// Keep track of what object our 
	void OnCollisionEnter(Collision collisionInfo) {
		Transform surface = collisionInfo.transform;
		
		PlayCollisionClip(surface.tag);
		
		if (surface.CompareTag("Grass") ||
			surface.CompareTag ("Sand")) {
			restingInBounds = true;	
		}

		if (!restingInBounds) {
			// We have hit a surface but we are not in bounds
			outOfBoundsTime = maxOutOfBoundsSeconds;
			isOutOfBounds = true;
		}
	}
	
	public void OnTriggerEnter(Collider collider) {
		PlayCollisionClip(collider.transform.tag);
		
		if (collider.transform.tag == "Water") {
			// Reset the shot immediately
			resetShot = true;
		}
	}
	
	private void PlayCollisionClip(string tag) {
		AudioClip hitClip = null;
		
		switch (tag) {
			case "Walls": hitClip = wallSound; break;
			case "Sand": hitClip = sandSound; break;
			case "Hole": hitClip = holeWallSound; break;
			case "Water": hitClip = waterSound; break;
		}
		
		if (hitClip) {
			audio.PlayOneShot(hitClip);
		}
	}
	
	public void Hit(float force, float max_force) {
		if (clubSound) {
			audio.PlayOneShot(clubSound, force / max_force);
		}
	}
	
	void OnCollisionExit(Collision collisionInfo) {
		Transform surface = collisionInfo.transform;
		
		if (surface.CompareTag("Grass") ||
		    surface.CompareTag ("Sand")) {
		    //We have left the in bounds area
			restingInBounds = false;	
		}
	}
}
