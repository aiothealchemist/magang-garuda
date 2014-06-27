using UnityEngine;
using System.Collections;

public class MainmenuGUI : BasemenuGUI {

	int selectedButton = 0;
	string[] menuButtons;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuType = type.menu;
		menuButtons = new string[] { "Play", "Settings", "Credits", "Exit" };
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (0, height / 8, width / 3, height * 3 / 4), menuBG);	//GUIStyle.none
		selectedButton = GUI.SelectionGrid (
			new Rect (0, height / 8, width / 3, height * 3 / 4), 
			selectedButton, menuButtons, 1);

		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:	//play
				gameObject.AddComponent<PlaymenuGUI>();
				Destroy (this);
				break;
			case 1:	//settings
				setShowcase(gameObject.AddComponent<SettingsGUI>());
				break;
			case 2:	//credits
				setShowcase(gameObject.AddComponent<CreditsGUI>());
				break;
			case 3:	//exit
				createPrompt(new voidWithZeroParam[] { exit, null },
						new string[] { "Are you sure you want to Quit?", "Yes", "No" });
				break;
			default:
				break;
			}
		}
	}

	//Exit game sequence
	void exit(){
		Debug.Log ("harusnya quit");
		Application.Quit ();
	}
}
