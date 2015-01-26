using UnityEngine;
using System.Collections;

public class RotateForAngularVelocity : MonoBehaviour {

	public Vector3 velocityToAchieve = Vector3.zero;



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 localangularvelocity = transform.InverseTransformDirection(rigidbody.angularVelocity);
		if (localangularvelocity != velocityToAchieve) {
			rigidbody.AddRelativeTorque((velocityToAchieve - localangularvelocity) * 10.0f);
		}
	}
	

}
