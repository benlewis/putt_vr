using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseManager : MonoBehaviour {

	public Golfer golfer;
	public Ball ball;
	public List<Hole> holes;

	public int currentHole = 0;

	// Use this for initialization
	void Start () {
		holes = new List<Hole>(FindObjectsOfType<Hole>());
		holes.Sort(			
			delegate(Hole a, Hole b) 
			{ 
				return a.name.CompareTo(b.name); 
			}
		);
		
		PlayNextHole();
	}
	
	public void PlayNextHole() {
		if (currentHole >= holes.Count) {
			// Quit
			Debug.Log ("Game over!");
			// return;
			currentHole = 0; // restart the course
		}
		
		Hole hole = holes[currentHole];
		hole.StartHole ();
		ball.transform.position = hole.startingSpot.position;
		golfer.SetHole(hole, ball);
		ball.gameObject.SetActive(true);
		ball.rigidbody.WakeUp();
		//golfer.SetForSwing();
		
		currentHole += 1;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
