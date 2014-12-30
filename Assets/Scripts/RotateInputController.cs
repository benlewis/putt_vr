using UnityEngine;
using System.Collections;

public class RotateInputController : MonoBehaviour {

	public float keyboardRotationAmount = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.localEulerAngles += Vector3.up * keyboardRotationAmount;
		}
		
		if (Input.GetKey(KeyCode.RightArrow)) {
			transform.localEulerAngles -= Vector3.up * keyboardRotationAmount;
		}
	}
}
