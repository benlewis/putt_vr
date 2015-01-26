using UnityEngine;
using System.Collections;
using InControl;

public class Golfer : MonoBehaviour {

	/* 
	 *	All the transforms we need to handle swings
	 *
	*/
	public Transform club;
	public Transform body;
	public MoveCamera moveableCamera;
	
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
	private float minSleepTime = 1.5f;
	private float minStationaryTime = 0.2f;
	private float? stationaryTime;	
	private float hitTime;
	private bool sleeping = false;
	


	/*
	 * 	How long is the swing in motion
	 * 	And how much force will be applied to the ball when we hit it
	*/
	private float force = 0.0f;
	private float swingTime = 0.0f;
	private bool inDownSwing = false;
	private float maxSwingTime = 0.0f;
	private bool waitForHitRelease = false;
	
	/*
	 *	Interface with the Course Manager
	 *
	*/
	private CourseManager manager;
	private Ball ball;
	private Transform holeTarget;
	private Hole hole;

	// Use this for initialization
	void Start () {
		sleeping = false;
		hitTime = Time.time;
		stationaryTime = null;
		manager = FindObjectOfType<CourseManager>();
		Screen.lockCursor = true;
	}
	
	public void SetHole(Hole h, Ball b) {
		hole = h;
		holeTarget = h.target;
		ball = b;
		sleeping = false;
		hitTime = Time.time;
		stationaryTime = null;
	}

	void FixedUpdate () {

		if (!sleeping &&
			ball.StillInBounds() && 
		    Time.time - hitTime > minSleepTime &&
		    ball.rigidbody.velocity.magnitude < sleepVelocity && 
		    ball.rigidbody.angularVelocity.magnitude < sleepAngularVelocity) {
		    
		    // Debug.Log ("Checking stationaryTime: " + stationaryTime);
		    if (stationaryTime == null)	   
		    	stationaryTime = minStationaryTime;
		    else
		    	stationaryTime -= Time.deltaTime;
		    
		    if (stationaryTime <= 0.0f)	
				PutBallToSleep();
		} else {
			stationaryTime = null;
		}

		// This is true when the ball is out of bounds or in a hazard
		if (ball.ResetShot ()) {
			hole.AddStroke(); // penalty stroke

			PutBallToSleep();
		}		
	}
	
	void Update() {
		InputDevice device = InputManager.ActiveDevice;
		
		if (waitForHitRelease && !device.Action1.IsPressed)
			waitForHitRelease = false;
		
		if (sleeping && !inDownSwing && !waitForHitRelease && device.Action1.IsPressed) {
			SwingUp();
		} else if (swingTime > 0.0f && !waitForHitRelease) {
			SwingDown();
		}
		
		if (device.RightBumper.WasPressed) {
			transform.Rotate(0.0f,45.0f,0.0f, Space.World);
		} else if (device.LeftBumper.WasPressed) {
			transform.Rotate(0.0f,-45.0f,0.0f, Space.World);
		}
	}
	
	public void PutBallToSleep() {
		ball.rigidbody.Sleep();
		sleeping = true;
		
		if (holeTarget.collider.bounds.Contains(ball.transform.position)) {
			Debug.Log ("Ball is inside hole. On to next hole");
			manager.PlayNextHole();
			return;
		}
		
		foreach (Collider c in hole.unoccupiedZones) {
			Debug.Log ("Checking bounds of " + c.name);
			if (c.bounds.Contains (ball.transform.position)) {
				Debug.Log ("Moving ball back");
				Collider landingSpot = c.GetComponentsInChildren<Collider>()[1];
				Debug.Log ("Moving to landing spot " + landingSpot.name);
				ball.transform.position = new Vector3(
					ball.transform.position.x,
					ball.transform.position.y,
					landingSpot.transform.position.z
				);
			}
		}
			
		SetForSwing();
	}
	
	public void SetForSwing() {
		
		// Move the golfer back to the ball
		transform.position = ball.transform.position;

		// If this is the first stroke, face the starting direction
		if (hole.GetStrokes() == 0) {
			transform.eulerAngles = Vector3.up * hole.startingSpot.eulerAngles.y;
			body.localEulerAngles = Vector3.zero;
		}
		if (false) { //always point towards the hole
			//find the vector pointing from the ball position to the hole
			Vector3 ballToHoleDirection = (holeTarget.position - ball.transform.position).normalized;
			
			//create the rotation we need to be in to look at the hole
			Quaternion bodyLookRotation = Quaternion.LookRotation(ballToHoleDirection);
			
			// Rotate the golfer to face the hole
			transform.rotation = bodyLookRotation;
			transform.eulerAngles = Vector3.Scale (transform.eulerAngles, Vector3.up);
			
			// Reset the body to be straight forward
			body.localEulerAngles = Vector3.zero;
		}
		
		// If this is a screen camera, put it back in the Head position looking forward
		if (moveableCamera) {
			moveableCamera.ResetPosition();
		}

		// Reset the ball
		ball.StartShot ();
		SwingReady();
	}
	
	public void SwingReady() {
		// We are no longer swinging
		
		// Reset the club to a standing up angle
		club.localEulerAngles = Vector3.zero;
		force = 0.0f;
		swingTime = 0.0f;
		inDownSwing = false;
		maxSwingTime = 0.0f;
		waitForHitRelease = false;
	}
	
	public void SwingUp() {
		if (force < maxForce) {
			swingTime += Time.deltaTime;
			club.localEulerAngles += Vector3.right * 40.0f * Time.deltaTime;
			force += 200.0f * Time.deltaTime;
		} else {
			maxSwingTime += Time.deltaTime;
			if (maxSwingTime > 1.0f) {
				SwingReady ();
				waitForHitRelease = true;
			}
		}
	}
	
	private float swingDownSeconds = 0.35f;
	private float swingDownDegreesPerSecond;
	public void SwingDown() {
		if (inDownSwing == false) {
			inDownSwing = true;
			swingTime = swingDownSeconds;
			// How fast do we swing down to get to 0 at the end?
			swingDownDegreesPerSecond = club.localEulerAngles.x / swingDownSeconds;
		}
		swingTime -= Time.deltaTime;
		club.localEulerAngles -= Vector3.right * swingDownDegreesPerSecond * Time.deltaTime;
		if (swingTime <= 0.0f) {
			if (force > 15.0f) 
				HitBall();
			else
				SwingReady(); // Skip the stroke, it wasn't enough of a swing
		}
	}
	
	public void HitBall() {
		// Swing!
		Vector3 forward = body.transform.forward;
		forward.y = 0;
		forward.Normalize();
		ball.rigidbody.AddForce(forward * force);
		sleeping = false;

		hole.AddStroke ();
		
		//ball.renderer.material.color = Color.red;
		ball.Hit(force, maxForce);		
	}
}
