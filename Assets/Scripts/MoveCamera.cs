using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

	public float sensitivity = 0.02f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Fly"), Input.GetAxis("Vertical"));
		directionVector.Normalize();
		directionVector *= sensitivity;
		transform.position += transform.rotation * directionVector;
	}
}
