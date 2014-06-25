using UnityEngine;
using System.Collections;

public class PlaymenuGUI : BasemenuGUI {

	int selectedButton = 0;
	string[] menuButtons;

	Vector2 viewVector;
	System.Collections.Generic.List<Rect> levelButtons;
	level levelConstructor;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuButtons = new string[] { "View Garage", "Achievements", "Gem Shop"};
		levelButtons = new System.Collections.Generic.List<Rect> ();
	}

	void Update () {
		if (showcase == null && levelConstructor != null)
			Destroy(levelConstructor);
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (0, height / 8, width / 3, height * 3 / 4), menuBG);	//GUIStyle.none

		selectedButton = GUI.SelectionGrid (
			new Rect (0, height * 3 / 16, width / 3, height / 2), 
			selectedButton, menuButtons, 1);
		
		// menu pad buttons
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:	//garage
				gameObject.AddComponent<GaragemenuGUI>();
				Destroy (this);
				break;
			case 1:	//achievements
				setShowcase (gameObject.AddComponent<AchievementsGUI>());
				break;
			case 2:	//gem shop
				setShowcase (gameObject.AddComponent<BaseShopGUI>());
				break;
			default:
				break;
			}
		}

		// level buttons
		if (showcase == null) {
			// Begin the ScrollView
			viewVector = GUI.BeginScrollView (new Rect (width * 2 / 5, height / 18, width * 8 / 15, height * 8 / 9), 
					viewVector, new Rect (0, 0, width * 8 / 15, height * 8 / 9));
			
			// TODO level position
			for (int i = 0; i < levelButtons.Count; i++) {
				if (GUI.Button (levelButtons[i], i.ToString())) {
					levelChosen (i);
				}
			}
			
			// End the ScrollView
			GUI.EndScrollView();
		}

		if (GUI.Button(new Rect(0, height/8,width/7,height/16),"Back")){
			gameObject.AddComponent<MainmenuGUI>();
			Destroy(this);
		}
	}

	void levelChosen (int levelNum) {
		setShowcase (gameObject.AddComponent<ChooseWeaponGUI> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
