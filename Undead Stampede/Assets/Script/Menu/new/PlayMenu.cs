using UnityEngine;
using System.Collections;

public class PlayMenu : BaseMenu {

	Vector2 viewVector;
	System.Collections.Generic.List<Rect> levelButtons;
	level levelConstructor;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		levelButtons = new System.Collections.Generic.List<Rect> ();
	}

	void Update () {
		if (window == null && levelConstructor != null)
			Destroy(levelConstructor);
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (0, 0, width, height), menuBG);	//GUIStyle.none

		if (GUI.Button(new Rect (width * 12 / 15, height / 100, width / 10.9f, height / 8),"Back")){
			gameObject.AddComponent<MainMenu>();
			Destroy(this);
		} else if (GUI.Button(new Rect (width * 13.5f / 15, height / 100, width / 10.9f, height / 8),"Shop")) {
			
		}

		// level buttons
		if (window == null) {
			// Begin the ScrollView
//			viewVector = GUI.BeginScrollView (new Rect (width / 30, height / 18, width * 14 / 15, height * 8 / 9), 
//					viewVector, new Rect (0, 0, width * 14 / 15, height * 8 / 9));
			// TODO level position
//			for (int i = 0; i < levelButtons.Count; i++) {
//				if (GUI.Button (levelButtons[i], i.ToString())) {
//					levelChosen (i);
//				}
//			}
			
			// End the ScrollView
//			GUI.EndScrollView();
		}
	}

	void levelChosen (int levelNum) {
		setWindow (gameObject.AddComponent<ChooseWeaponGUI> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
