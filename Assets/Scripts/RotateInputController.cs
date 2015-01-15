using UnityEngine;
using System.Collections;
using InControl;

public class RotateInputController : MonoBehaviour {

	public float degreesPerSecond = 90.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		InputDevice device = InputManager.ActiveDevice;
		float rotation = device.RightTrigger - device.LeftTrigger;
		transform.localEulerAngles -= Vector3.up * 
			rotation * 
			degreesPerSecond * 
			Time.deltaTime;
	}
}
