using UnityEngine;
using System.Collections;

public class PlayMenu : BaseMenu {

	Texture2D shopButton;

	System.Collections.Generic.List<Rect> levelButtons;
	level levelConstructor;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		initLevelButtons ();
	}

	protected override void loadResources () {
		menuType = type.menu;
		menuBG = Resources.Load<Texture2D>("menu/background/mainmenu");
		backButton = Resources.Load<Texture2D>("menu/button/back 1");
		shopButton = Resources.Load<Texture2D>("menu/button/shop");
	}

	protected override void updateGUI () { }
	protected override void updateBlockableGUI () {
		if (GUI.Button(new Rect (width * 12 / 15, height / 100, width / 10.9f, height / 8),backButton, GUIStyle.none)){
			//back
			gameObject.AddComponent<MainMenu>();
			Destroy(this);
		} else if (GUI.Button(new Rect (width * 13.5f / 15, height / 100, width / 10.9f, height / 8),shopButton, GUIStyle.none)) {
			//shop
			setWindow (gameObject.AddComponent<Shop>());
		} else if (GUI.Button(new Rect (width * 10.5f / 15, height / 100, width / 10.9f, height / 8),"Play")) {
			setWindow (gameObject.AddComponent<EquipWeapon>());
		}
		// ScrollView
//			viewVector = GUI.BeginScrollView (new Rect (width / 30, height / 18, width * 14 / 15, height * 8 / 9), 
//					viewVector, new Rect (0, 0, width * 14 / 15, height * 8 / 9));
//			for (int i = 0; i < levelButtons.Count; i++) {
//				if (GUI.Button (levelButtons[i], i.ToString())) {
//					levelChosen (i);
//				}
//			}
//			GUI.EndScrollView();
	}

	void initLevelButtons(){	//level buttons position
		levelButtons = new System.Collections.Generic.List<Rect> ();
		levelButtons.Add(new Rect(0,0,0,0));
	}

	void levelChosen (int levelNum) {
		setWindow (gameObject.AddComponent<EquipWeapon> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
