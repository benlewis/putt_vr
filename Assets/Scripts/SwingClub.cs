using UnityEngine;
using System.Collections;

public class SwingClub : MonoBehaviour {

	public Rigidbody ball;
	public float force = 100.0f;
	public Camera mainCamera;
	public Transform hitDirection;
	
	public float sleepVelocity = 0.5f;
	public float sleepAngularVelocity = 3.5f;
	
	public float minSleepTime = 0.5f;
	
	private float hitTime;
	private bool sleeping = false;
	
	private Vector3 cameraPositionToBall = new Vector3(0.0f, 1.0f, -0.75f);

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
		
		if (sleeping && Input.GetKeyDown (KeyCode.Space)) {
			HitBall();
		}
	}
	
	public void PutBallToSleep() {
		ball.rigidbody.Sleep();
		sleeping = true;
		ball.transform.renderer.material.color = Color.green;
		mainCamera.transform.position = ball.transform.position + cameraPositionToBall;
		
		hitDirection.transform.position = ball.transform.position;
		hitDirection.gameObject.SetActive(true);
	}
	
	public void HitBall() {
		// Swing!
		Vector3 forward = hitDirection.forward;
		forward.y = 0;
		forward.Normalize();
		hitDirection.gameObject.SetActive(false);
		ball.AddForce(forward * force);
		sleeping = false;
		

		ball.transform.renderer.material.color = Color.red;
	}
}
