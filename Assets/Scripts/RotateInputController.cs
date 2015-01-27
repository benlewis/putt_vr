using UnityEngine;
using System.Collections;
using InControl;

public class RotateInputController : MonoBehaviour {

	public float degreesPerSecond = 90.0f;
	public Transform hmd;
	public MoveCamera monitor;
	public Transform rayObject;
	
	private float lastY;
	private Golfer golfer;

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
			if (hmd && hmd.gameObject.activeInHierarchy)
				OVRManager.capiHmd.RecenterPose();
			
			if (monitor && monitor.gameObject.activeInHierarchy)
				monitor.ResetPosition();
		}
			
		if (device.Action3) {
			// Rotate club to face the direction of the camera
			Transform t = hmd;
			if (t == null || !t.gameObject.activeInHierarchy)
				t = monitor.transform;
				
			RaycastHit hitInfo;
			if (t && Physics.Raycast(t.position, t.forward, out hitInfo)) {
				rayObject.transform.position = hitInfo.point;
				rayObject.renderer.enabled = true;
				
				//find the vector pointing from the ball position to the hole
				Vector3 directionToFace = (hitInfo.point - transform.position).normalized;
				
				//create the rotation we need to be in to look at the hole
				Quaternion bodyLookRotation = Quaternion.LookRotation(directionToFace);
				
				// Rotate the golfer to face the hole
				transform.rotation = bodyLookRotation;
				
				// Don't aim up
				transform.eulerAngles = Vector3.Scale (transform.eulerAngles, Vector3.up);
				
				
//				float y = t.localEulerAngles.y;
//				transform.localEulerAngles = new Vector3(
//					transform.localEulerAngles.x, 
//					y, 
//					transform.localEulerAngles.z
//				);		
			} else {
				rayObject.renderer.enabled = false;				
			}			
		} else {
			rayObject.renderer.enabled = false;			
		}
		
		float rotation = device.RightTrigger - device.LeftTrigger;
		transform.localEulerAngles -= Vector3.up * 
			rotation * 
			degreesPerSecond * 
			Time.deltaTime;
	}
}
