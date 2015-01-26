using UnityEngine;
using System.Collections;
using InControl;

public class MoveCamera : MonoBehaviour {

	public float speed = 1.0f;

	public float sensitivityX = 75.0f;
	public float sensitivityY = 75.0f;

	public float minimumY = -60.0f;
	public float maximumY = 60.0f;

	private float rotationY = 0.0f;

	/* 
	 * 	After the ball sleeps we need to move the camera back
	*/
	private Vector3 cameraPositionToGolfer;
	private Vector3 cameraLocalEulerAngles;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cameraPositionToGolfer = transform.localPosition;
		cameraLocalEulerAngles = transform.localEulerAngles;
	}
	
	public void ResetPosition() {
		transform.localPosition = cameraPositionToGolfer;
		ResetOrientation();
	}	

	// Update is called once per frame
	void Update () {
		InputDevice device = InputManager.ActiveDevice;

		// Mouse & Gamepad look
		float rotationX = transform.localEulerAngles.y + device.RightStickX * sensitivityX * Time.deltaTime;
		
		rotationY += device.RightStickY * sensitivityY * Time.deltaTime;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		// Movement
		Vector3 directionVector = new Vector3(device.LeftStickX, device.DPadY, device.LeftStickY);

		// Keep our overall magnitude at 1.0f
		directionVector.Normalize();

		// Now normalize to our speed and fps
		directionVector *= Time.deltaTime * speed;

		// Move the camera
		transform.position += transform.rotation * directionVector;
	}

	public void ResetOrientation() {
		rotationY = -180.0f;
	}
}
