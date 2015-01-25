using UnityEngine;
using System.Collections;
using InControl;

public class RotateInputController : MonoBehaviour {

	public float degreesPerSecond = 90.0f;
	public Transform hmd;
	public Transform monitor;
	
	private float lastY;

	// Use this for initialization
	void Start () {
		if (hmd)
			lastY = hmd.localEulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
//		if (hmd) {
//			Debug.Log ("Setting to hmd y of " + (hmd.eulerAngles.y - 90.0f));
//			//transform.localEulerAngles.Set(transform.localEulerAngles.x, hmd.eulerAngles.y, transform.localEulerAngles.z);
//			float delta_y = lastY - hmd.localEulerAngles.y;
//			lastY = hmd.localEulerAngles.y;
//			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, hmd.localEulerAngles.y - 90.0f, transform.localEulerAngles.z);
//												
//			return;
//		}

		InputDevice device = InputManager.ActiveDevice;
		
		if (device.Action4) {
			// Rotate club to face the direction of the camera
			Transform t = hmd;
			if (t == null || !t.gameObject.activeInHierarchy)
				t = monitor;
			if (t) {
				Debug.Log ("Rotating club to face the direction of " + t.gameObject.name);
				float y = t.localEulerAngles.y;
				transform.localEulerAngles = new Vector3(
					transform.localEulerAngles.x, 
					y, 
					transform.localEulerAngles.z
				);			
			}			
		}
		
		float rotation = device.RightTrigger - device.LeftTrigger;
		transform.localEulerAngles -= Vector3.up * 
			rotation * 
			degreesPerSecond * 
			Time.deltaTime;
	}
}
