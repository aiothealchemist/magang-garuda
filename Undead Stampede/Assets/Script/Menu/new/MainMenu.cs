using UnityEngine;
using System.Collections;

public class MainMenu : BaseMenu {

	Texture2D[] menuButtons;
	Texture2D gameTitle;

	bool BGM {
		get{return Utils.PrefsAccess.persistenceBGM; }
		set{Utils.PrefsAccess.persistenceBGM = value;}
	}
	bool SFX {
		get{return Utils.PrefsAccess.persistenceSFX; }
		set{Utils.PrefsAccess.persistenceSFX = value;}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		resetCredits ();
		menuType = type.menu;
		menuBG = Resources.Load<Texture2D> ("background/mainmenu");
		menuButtons = Resources.LoadAll<Texture2D> ("button/mainmenu");
		gameTitle = Resources.Load<Texture2D> ("background/mainmenu_title");
	}

	void Update () {
		if (credits) {
			if (Input.GetMouseButtonDown(0)) {
				resetCredits ();
			} else {
				startCredits ();
			}
		}
	}

	protected override void updateGUI () {
		GUI.Box (new Rect(0, 0, width, height), menuBG, GUIStyle.none);
		GUI.Box (new Rect(width * 1.1f / 5, height / 4, width / 2, height), gameTitle, GUIStyle.none);
		BGM = (GUI.Toggle (new Rect (width * 12 / 15, audiButtY, width / 10.9f, height / 8), BGM, menuButtons [BGM ? 3 : 4], GUIStyle.none));
		SFX = (GUI.Toggle (new Rect (width * 13.5f / 15, audiButtY, width / 8, height / 8), SFX, menuButtons [SFX ? 5 : 6], GUIStyle.none));

		if (GUI.Button (new Rect (width * 2.38f / 7, playButtY, width / 3, height  / 5), menuButtons [0], GUIStyle.none)) {//play
			gameObject.AddComponent<PlayMenu>();	Destroy (this);
		} else if (GUI.Button (new Rect (achiButtX, height * 3.1f / 4, width * 3.2f / 10, height), menuButtons [1], GUIStyle.none)) {//achievement
			setWindow (gameObject.AddComponent<Achievements>());
		} else if (GUI.Button (new Rect (credButtX, height * 3.1f / 4, width * 3.3f / 10, height), menuButtons [2], GUIStyle.none)) {//credits
			Destroy (window);
			credits = true;
		}

		if (creditText){
			// TODO credit text
		}
	}

	bool credits, creditText;
	float credButtX, achiButtX, playButtY, audiButtY;

	void startCredits(){
		credButtX = Mathf.Lerp (credButtX, width, 0.1f);
		achiButtX = Mathf.Lerp (achiButtX, -width * 3.2f / 10, 0.1f);
		playButtY = Mathf.Lerp (playButtY, height, 0.1f);
		audiButtY = Mathf.Lerp (audiButtY, -height / 8, 0.1f);
		if (playButtY == height) creditText = true;
	}
	void resetCredits(){
		creditText = false;
		credits = false;
		achiButtX = 0;
		credButtX = width * 3.35f / 5;
		playButtY = height * 3 / 4;
		audiButtY = height / 100;
	}
}
