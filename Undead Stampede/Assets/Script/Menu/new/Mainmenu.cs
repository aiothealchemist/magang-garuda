using UnityEngine;
using System.Collections;

public class Mainmenu : BasemenuGUI {

	Texture2D[] menuButtons;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuButtons = Resources.LoadAll<Texture2D> ("button/mainmenu");
	}
	
	protected override void updateGUI () {
		if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [0], GUIStyle.none)) {	//play
			gameObject.AddComponent<PlaymenuGUI>();
			Destroy (this);
		} else if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [0], GUIStyle.none)) {	//achievement
			setShowcase (gameObject.AddComponent<AchievementsGUI>());
		} else if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [0], GUIStyle.none)) {	//settings
			setShowcase(gameObject.AddComponent<Settings>());
		} 
	}

}
