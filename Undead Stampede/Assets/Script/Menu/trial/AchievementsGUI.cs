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
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);

		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (25, 25, 100, 100), viewVector, new Rect (0, 0, 90, 0));

		// End the ScrollView
		GUI.EndScrollView();

		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}
}
