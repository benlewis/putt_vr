using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	public Transform warpPartner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider collider) {
		Debug.Log ("Enter warp " + collider.tag);
		if (collider.CompareTag("Ball")) {
			Debug.Log ("Moving ball from " + collider.transform.position + " to " + warpPartner.position);
			Ball ball = collider.GetComponent<Ball>();
			ball.SetWarpPosition(warpPartner.position);
			//collider.transform.position = warpPartner.position;
		}
	 } 
}
