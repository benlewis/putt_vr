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
		ball.StartHole(hole);
		golfer.SetHole(hole, ball);
		ball.gameObject.SetActive(true);
		ball.rigidbody.WakeUp();
		//golfer.SetForSwing();
		
		currentHole += 1;
	}
	
	public readonly string[] holeNames = {"1","2","3","4","5","6","7","8","9"};
	
	public string GetScoreText() {
		
		string[] lines = {
			"        ",
			"Par     ",
			"Strokes "
		};
//		lines[0] 	= "        ";
//		lines[1] 	= "Par     ";
//		lines[2] 	= "Strokes ";
		for (int i = 0; i < holes.Count; i++) {
			lines[0] += string.Format("{0} ", i + 1);
			lines[1] += string.Format("{0} ", holes[i].par);
			if (currentHole > i)
				lines[2] += string.Format("{0} ", holes[i].GetStrokes ());
		}
		//string top_line = 		"   " + string.Join(".. ", holeNames);
		//string pars  = 		"Par holes.GetRange(0,9).ConvertAll(hole => hole.name).ToArray()
		
		return string.Join("\n", lines);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
