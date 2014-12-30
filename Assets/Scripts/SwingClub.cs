using UnityEngine;
using System.Collections;

public class SwingClub : MonoBehaviour {

	public Transform ball;
	public Transform hole;
	public Transform club;
	public float maxForce = 300.0f;
				
	/*
	 *	Figure out when the ball has stopped moving
	*/			
	private float sleepVelocity = 0.5f;
	private float sleepAngularVelocity = 3.5f;	
	private float minSleepTime = 0.5f;	
	private float hitTime;
	private bool sleeping = false;
	
	/* 
	 * 	After the ball sleeps we need to move the golfer
	*/
	private Vector3 golferPositionToBall = Vector3.up * 0.8f;
	
	/*
	 * 	How long is the swing in motion
	 * 	And how much force will be applied to the ball when we hit it
	*/
	private float force = 0.0f;
	private float swingTime = 0.0f;

	// Use this for initialization
	void Start () {
		sleeping = false;
		hitTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!sleeping &&
			Time.time - hitTime > minSleepTime &&
			ball.rigidbody.velocity.magnitude < sleepVelocity && 
		    ball.rigidbody.angularVelocity.magnitude < sleepAngularVelocity) {
			PutBallToSleep();
		}
		
		if (sleeping && Input.GetKey (KeyCode.Space)) {
			SwingUp();
		} else if (sleeping && swingTime > 0.0f) {
			SwingDown();
		}
	}
	
	public void PutBallToSleep() {
		ball.rigidbody.Sleep();
		sleeping = true;
		ball.renderer.material.color = Color.green;
		
		transform.position = ball.position + golferPositionToBall;
		club.localEulerAngles = Vector3.zero;
		force = 0.0f;
		swingTime = 0.0f;
	}
	
	public void SwingUp() {
		if (force < maxForce) {
			swingTime += Time.deltaTime;
			club.localEulerAngles += Vector3.right * 1.0f;
			force += 5.0f;
		}
	}
	
	public void SwingDown() {
		swingTime -= Time.deltaTime;
		club.localEulerAngles -= Vector3.right * 1.0f;
		if (swingTime <= 0.0f)
			HitBall();
	}
	
	public void HitBall() {
		// Swing!
		Vector3 forward = transform.forward;
		forward.y = 0;
		forward.Normalize();
		ball.rigidbody.AddForce(forward * force);
		sleeping = false;
		

		ball.transform.renderer.material.color = Color.red;
	}
}
