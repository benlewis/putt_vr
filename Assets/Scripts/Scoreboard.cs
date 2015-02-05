using UnityEngine;
using System.Collections;
using InControl;

public class Scoreboard : MonoBehaviour {

	public CourseManager courseManager;
	public int starting_hole = 0;
	public TextMesh controlText;

	private TextMesh textMesh;
	
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();		
	}
	
	// Update is called once per frame
	void Update () {
	
		InputDevice device = InputManager.ActiveDevice;
		if (device.GetControl(InputControlType.Start).IsPressed) {
			textMesh.text = courseManager.GetControlText();
			controlText.text = "Release Start for Scores";
		}
		else {
			textMesh.text = courseManager.GetScoreText(starting_hole, 9);
			controlText.text = "Hold Start for Controls";
		}
	}
}
