using UnityEngine;
using System.Collections;

public class AchievementsGUI : BasemenuGUI {
	
	Vector2 viewVector;

	string[] menuButtons;

	protected override void Start () {
		base.Start ();
		menuType = type.showcase;
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);

		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (25, 25, 100, 100), viewVector, new Rect (0, 0, 90, 0));

		// End the ScrollView
		GUI.EndScrollView();

		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}
}
