using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Transform objectTouchingBall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Transform GetTouchingObject() {
		return objectTouchingBall;
	}
	
	// Keep track of what object our 
	void OnCollisionEnter(Collision collisionInfo) {
		objectTouchingBall = collisionInfo.transform;
		
		Debug.Log ("Ball collides with " + objectTouchingBall.name);
		// TODO: Play a sound if the collisionInfo object has a sound
	}
	
	void OnCollisionExit(Collision collisionInfo) {
		if (objectTouchingBall) {
			Debug.Log ("Ball is no longer touching " + objectTouchingBall.name);
		}
		
		//objectTouchingBall = null;
	}
}
