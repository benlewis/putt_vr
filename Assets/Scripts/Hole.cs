using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {

	public Transform startingSpot;
	public Transform target;
	public int par = 2;
	public TextMesh scoreText;
	
	private int strokes = 0;

	// Use this for initialization
	void Start () {
		strokes = 0;
	}
	
	public void AddStroke() {
		strokes += 1;
	}

	public void StartHole() {
		strokes = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//scoreText.text = "Par: " + par + "\nStrokes: " + strokes;
	}
}
