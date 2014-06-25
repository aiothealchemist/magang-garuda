using UnityEngine;
using System.Collections;

public class CreditsGUI : BasemenuGUI {
	
	string[] menuButtons;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	protected override void updateGUI () {
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}
}
