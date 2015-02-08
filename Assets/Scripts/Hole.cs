using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hole : MonoBehaviour {

	public Transform startingSpot;
	public Transform target;
	public int par = 2;
	
	private int strokes = 0;
	
	public List<Collider> unoccupiedZones;

	// Use this for initialization
	void Start () {
		strokes = 0;
		TextMesh[] holeTexts = GetComponentsInChildren<TextMesh>();
		foreach (TextMesh text in holeTexts) {
			switch(text.name) {
				case "Name Text": text.text = name; break;
				case "Par Text": text.text = string.Format("Par {0}", par); break;
			}
		}
	}
	
	public void AddStroke() {
		strokes += 1;
	}

	public void StartHole() {
		strokes = 0;
	}
	
	public int GetStrokes() {
		return strokes;
	}
}
