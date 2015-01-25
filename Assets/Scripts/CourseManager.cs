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
	
	public string GetScoreText(int starting_hole, int num_holes) {
		
		string[] lines = {
			"       ",
			"",
			"Par    ",
			"",
			"Strokes"
		};
//		lines[0] 	= "        ";
//		lines[1] 	= "Par     ";
//		lines[2] 	= "Strokes ";
		for (int i = starting_hole; i < starting_hole + num_holes; i++) {
			int h = i + 1;
			lines[0] += AddInt(h, false);
			if (holes[i]) {
				lines[2] += AddInt(holes[i].par);
				if (currentHole > i)
					lines[4] += AddInt(holes[i].GetStrokes ());
			}
		}
		for (int i = 0; i < lines[0].Length; i++) {
			lines[1] += "-";
			lines[3] += "-";
		}
		
		//string top_line = 		"   " + string.Join(".. ", holeNames);
		//string pars  = 		"Par holes.GetRange(0,9).ConvertAll(hole => hole.name).ToArray()
		
		return string.Join("\n", lines);
	}
	
	private string AddInt(int h, bool pipe = true) {
		return string.Format(" {0}{1}{2}", h, h >= 10 ? "" : " ", pipe ? "|" : " ");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
