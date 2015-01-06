using UnityEngine;
using System.Collections;

public class Golfer : MonoBehaviour {

	/* 
	 *	All the transforms we need to handle swings
	 *
	*/
	public Ball ball;
	public Transform hole;
	public Transform club;
	public Transform body;
	
	/*
	 * 	Determines how far back we swing
	 *
	*/
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
	 * 	After the ball sleeps we need to move the camera back
	*/
	private Vector3 cameraPositionToGolfer;
	
	/*
	 * 	How long is the swing in motion
	 * 	And how much force will be applied to the ball when we hit it
	*/
	private float force = 0.0f;
	private float swingTime = 0.0f;
	
	/*
	 *	Interface with the Course Manager
	 *
	*/
	private CourseManager manager;
	
	// Use this for initialization
	void Start () {
		sleeping = false;
		hitTime = Time.time;
		cameraPositionToGolfer = Camera.main.transform.localPosition;
		manager = FindObjectOfType<CourseManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!sleeping &&
		    Time.time - hitTime > minSleepTime &&
		    ball.rigidbody.velocity.magnitude < sleepVelocity && 
		    ball.rigidbody.angularVelocity.magnitude < sleepAngularVelocity) {
			PutBallToSleep();
		}
		
		if (sleeping && Input.GetButton ("Fire1")) {
			SwingUp();
		} else if (sleeping && swingTime > 0.0f) {
			SwingDown();
		}
	}
	
	public void PutBallToSleep() {
		ball.rigidbody.Sleep();
		sleeping = true;
		ball.renderer.material.color = Color.green;
		
		if (ball.GetTouchingObject())
			Debug.Log ("Ball is resting on " + ball.GetTouchingObject().name);
		
		if (hole.collider.bounds.Contains(ball.transform.position)) {
			Debug.Log ("Ball is inside hole. On to next hole");
			manager.PlayNextHole();
		}
			
		SetForSwing();
	}
	
	public void SetForSwing() {
		
		// Move the golfer back to the ball
		transform.position = ball.transform.position;
		club.localEulerAngles = Vector3.zero;
		Camera.main.transform.localPosition = cameraPositionToGolfer;
		
		//find the vector pointing from the ball position to the hole
		Vector3 ballToHoleDirection = (hole.position - ball.transform.position).normalized;
		
		//create the rotation we need to be in to look at the hole
		Quaternion bodyLookRotation = Quaternion.LookRotation(ballToHoleDirection);
		
		// Rotate the body to face the hole
		body.rotation = bodyLookRotation;
		body.eulerAngles = Vector3.Scale (body.eulerAngles, Vector3.up);
		
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
		swingTime -= Time.deltaTime * 2.0f;
		club.localEulerAngles -= Vector3.right * 2.0f;
		if (swingTime <= 0.0f)
			HitBall();
	}
	
	public void HitBall() {
		// Swing!
		Vector3 forward = body.transform.forward;
		forward.y = 0;
		forward.Normalize();
		ball.rigidbody.AddForce(forward * force);
		sleeping = false;
		
		
		ball.renderer.material.color = Color.red;
	}
}
