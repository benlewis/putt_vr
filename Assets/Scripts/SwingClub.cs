using UnityEngine;
using System.Collections;

public class SwingClub : MonoBehaviour {

	public Rigidbody ball;
	public Vector3 force;
	public Camera camera;
	
	public float sleepVelocity = 0.5f;
	public float sleepAngularVelocity = 3.5f;
	
	public float minSleepTime = 0.5f;
	
	private float hitTime;
	private bool sleeping = true;
	
	private Vector3 cameraPositionToBall = new Vector3(0.0f, 1.5f, -1.0f);

	// Use this for initialization
	void Start () {
		PutBallToSleep();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!sleeping &&
			Time.time - hitTime > minSleepTime &&
			ball.rigidbody.velocity.magnitude < sleepVelocity && 
		    ball.rigidbody.angularVelocity.magnitude < sleepAngularVelocity) {
			PutBallToSleep();
		}
		
		if (sleeping && Input.GetKeyDown (KeyCode.Space)) {
			HitBall();
		}
	}
	
	public void PutBallToSleep() {
		ball.rigidbody.Sleep();
		sleeping = true;
		ball.transform.renderer.material.color = Color.green;
		camera.transform.position = ball.transform.position + cameraPositionToBall;
	}
	
	public void HitBall() {
		// Swing!
		ball.AddForce(force);
		sleeping = false;
		ball.transform.renderer.material.color = Color.red;
	}
}
