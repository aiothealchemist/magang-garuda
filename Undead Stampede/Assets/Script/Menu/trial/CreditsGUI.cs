using UnityEngine;
using System.Collections;

public class CreditsGUI : BasemenuGUI {
	
	string[] menuButtons;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	protected override void updateGUI () {
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);
		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}
}
