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
			return;
		}
		
		Hole hole = holes[currentHole];
		ball.transform.position = hole.startingSpot.position;
		golfer.SetHole(hole.target, ball);
		ball.gameObject.SetActive(true);
		//golfer.SetForSwing();
		
		currentHole += 1;
		
		if (currentHole >= holes.Count) {
			// Quit
			Debug.Log ("Game over!");
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
