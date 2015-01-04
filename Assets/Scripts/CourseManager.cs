using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseManager : MonoBehaviour {

	public Transform golfer;
	public List<Hole> holes;

	// Use this for initialization
	void Start () {
		holes = new List<Hole>(FindObjectsOfType<Hole>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
