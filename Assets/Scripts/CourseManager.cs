using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CourseManager : MonoBehaviour {

	public Golfer golfer;
	public Ball ball;
	public List<Hole> holes;
	public int currentHoleIndex = 0;
	public AudioClip soundHoleInOne;
	public AudioClip soundBirdie;
	public AudioClip soundPar;
	public AudioClip soundBogie;
	public AudioClip soundEagle;
	public AudioClip soundDoubleBogie;
	public AudioClip soundTripleBogie;
	public AudioClip soundQuadrupleBogie;
	public AudioClip soundAlbatross;
	
	private Hole currentHole = null;

	// Use this for initialization
	void Start () {		
		PlayNextHole();
	}
	
	public void FinishHole() {
		AudioClip clip = null;

		if (currentHole.GetStrokes() == 1)
			clip = soundHoleInOne;
		else  {
			int difference = currentHole.GetStrokes() - currentHole.par;
			if (difference >= 4)
				clip = soundQuadrupleBogie;
			else if (difference <= -3)
				clip = soundAlbatross;
			else switch(difference) {
				case 3: clip = soundTripleBogie; break;
				case 2: clip = soundDoubleBogie; break;
				case 1: clip = soundBogie; break;
				case 0: clip = soundPar; break;
				case -1: clip = soundBirdie; break;
				case -2: clip = soundEagle; break;
			}
		}

		
		if (clip)	
			golfer.audio.PlayOneShot(clip);
		
		PlayNextHole();
		
	}
	
	public void PlayNextHole() {
		
		if (currentHole != null)
			currentHoleIndex += 1;
		
		if (currentHoleIndex >= holes.Count) {
			// Quit
			Debug.Log ("Game over!");
			
			// TODO: Show an end game dialog of sorts
			currentHoleIndex = 0; // restart the course
			
		}
		
		currentHole = holes[currentHoleIndex];
		
		currentHole.StartHole ();
		ball.StartHole(currentHole);
		golfer.SetHole(currentHole, ball);
		ball.gameObject.SetActive(true);
		ball.rigidbody.WakeUp();
		golfer.PutBallToSleep();
	}
	
	public readonly string[] holeNames = {"1","2","3","4","5","6","7","8","9"};
	
	public string GetControlText() {
		string[] lines = {
			"Swing: (A)",
			"Reset Shot: Hold (A)",
			"Follow Ball (Mid Shot): (A)",
			"Rotate Club: (LT), (RT)",
			"Turn Around (180): (LB), (RB)",
			"Reset View: (Y)",
			"Aim Look: Hold (X)"
		};
	
		return string.Join("\n", lines);
	}
	
	public string GetScoreText(int starting_hole, int num_holes) {
		
		string[] lines = {
			"       ",
			"",
			"Par    ",
			"",
			"Strokes",
			"",
			""
		};
		
		int total_par = 0;
		int total_strokes = 0;
//		lines[0] 	= "        ";
//		lines[1] 	= "Par     ";
//		lines[2] 	= "Strokes ";
		for (int i = starting_hole; i < starting_hole + num_holes; i++) {
			int h = i + 1;
			lines[0] += AddInt(h, false);
			if (holes[i]) {
				lines[2] += AddInt(holes[i].par);
				if (currentHoleIndex >= i) {
					lines[4] += AddInt(holes[i].GetStrokes ());
					if (currentHoleIndex >= i + 1) {
						total_strokes += holes[i].GetStrokes();
						total_par += holes[i].par;
					}
				}
				
			}
		}
		
		int overall_par = 0;
		int overall_strokes = 0;
		for (int i = 0; i < currentHoleIndex; i++) {
			overall_par += holes[i].par;
			overall_strokes += holes[i].GetStrokes();
		}
		
		for (int i = 0; i < lines[0].Length; i++) {
			lines[1] += "-";
			lines[3] += "-";
		}
	
		lines[6] = string.Format ("{0} Nine: {1} / {2}", (starting_hole == 0) ? "Front" : "Back", total_strokes, total_par);
		lines[6] += string.Format ("   Overall: {0} / {1}", overall_strokes, overall_par);
		
		return string.Join("\n", lines);
	}
	
	private string AddInt(int h, bool pipe = true) {
		return string.Format(" {0}{1}{2}", h, h >= 10 ? "" : " ", pipe ? "|" : " ");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
