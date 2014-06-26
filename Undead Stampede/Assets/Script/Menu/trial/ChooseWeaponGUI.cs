﻿using UnityEngine;
using System.Collections;

public class ChooseWeaponGUI : BasemenuGUI {
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuType = type.showcase;
	}

	protected override void updateGUI ()
	{
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);

		// Leaderboards
		GUI.Box (new Rect (width * 5 / 12, height / 12, width / 2, height / 5), menuBG);
		
		// Available weapons
		GUI.Box (new Rect (width * 5 / 12, height * 3 / 10, width / 4, height * 10 / 27), menuBG);
		
		// Chosen weapons
		GUI.Box (new Rect (width * 5 / 12, height * 0.69f, width / 2, height * 0.22f), menuBG);

		// Proceed Button
		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 3 / 4, height * 13 / 30, width / 6, height / 5), "Proceed")) {
			
		}
	}
}