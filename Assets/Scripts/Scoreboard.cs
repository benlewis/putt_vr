using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	public CourseManager courseManager;

	private TextMesh textMesh;
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		textMesh.text = courseManager.GetScoreText();
	}
}
