using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class MainmenuGUI : BasemenuGUI {

	int selectedButton = 0;
	Texture2D[] menuButtons;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuButtons = Resources.LoadAll<Texture2D> ("button/mainmenu");
		menuBG = Resources.Load<Texture2D> ("background/menupad");
	}

	protected override void updateGUI () {
		GUI.DrawTexture (new Rect (- width / 6, height / 10, width / 2, height * 4 / 5), menuBG,ScaleMode.StretchToFill);
		selectedButton = GUI.SelectionGrid (
			new Rect (width / 100, height / 6, width / 3, height * 2 / 3), 
			selectedButton, menuButtons, 1, GUIStyle.none);

		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:	//play
				gameObject.AddComponent<PlaymenuGUI>();
				Destroy (this);
				break;
			case 1:	//settings
				setShowcase(gameObject.AddComponent<Settings>());
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
