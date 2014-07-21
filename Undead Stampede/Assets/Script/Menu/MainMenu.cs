using UnityEngine;
using System.Collections;
using System.Linq;

public class MainMenu : BaseMenu {

	System.Timers.Timer wiggleTimer;
	float maxRotation = 6, rotationAngle;
	double timerInterval = 3000;
	int wigglePhase;

	float credButtX, achiButtX, playButtY, audiButtY;
	bool credits, creditText;

	Texture2D[] menuButtons;
	Texture2D gameTitle;

	//built-in
	protected override void Start () {
		base.Start ();
		resetCredits ();
		wiggleTimer = new System.Timers.Timer(timerInterval);
		wiggleTimer.Elapsed += (sender, e) => wiggleTimer.Stop ();
	}
	void Update () {
		if (credits) {
			if (Input.GetMouseButtonDown(0)) {
				resetCredits ();
			} else {
				startCredits ();
			}
		} else if (!wiggleTimer.Enabled) {
			doWiggle ();
		}
	}

	//overrides
	protected override void loadResources () {
		menuType = type.menu;
		menuBG = Resources.Load<Texture2D> ("menu/background/mainmenu");
		menuButtons = Resources.LoadAll<Texture2D> ("menu/button/mainmenu");
		gameTitle = Resources.Load<Texture2D> ("menu/background/mainmenu_title");
	}
	protected override void updateGUI () {
		if (ButtonGUI (new Rect (width * 2.38f / 7, playButtY, width / 3, height  / 5), 
				new Texture2D[] {menuButtons[0], menuButtons[1], menuButtons[2]})) {
			//play
			gameObject.AddComponent<PlayMenu>();	Destroy (this);
		} else if (ButtonGUI (new Rect (achiButtX, height * 3.1f / 4, width * 3.2f / 10, height), 
				new Texture2D[] {menuButtons [1], menuButtons [1], menuButtons [1]})) {
			//achievement
			setWindow (gameObject.AddComponent<Achievements>());
		} else if (ButtonGUI (new Rect (credButtX, height * 3.1f / 4, width * 3.3f / 10, height),
				new Texture2D[] {menuButtons [2], menuButtons [2], menuButtons [2]})) {
			//credits
			Destroy (window);
			credits = true;
		}

		GUI.BeginGroup (new Rect(0,0, width, height));
		GUIUtility.RotateAroundPivot (rotationAngle, new Vector2(width/2,height/2));
		GUI.DrawTexture (new Rect(width * 1.1f / 5, height / 4, width / 2, height / 3f), gameTitle, ScaleMode.ScaleToFit);
		GUI.EndGroup ();

		if (creditText){
			creditsGUI ();
		}
	}
	protected override void updateBlockableGUI () {
		Utils.PrefsAccess.persistenceBGM = (GUI.Toggle (new Rect (width * 12 / 15, audiButtY, width / 10.9f, height / 8), 
				Utils.PrefsAccess.persistenceBGM, menuButtons [Utils.PrefsAccess.persistenceBGM ? 3 : 4], GUIStyle.none));
		Utils.PrefsAccess.persistenceSFX = (GUI.Toggle (new Rect (width * 13.5f / 15, audiButtY, width / 8, height / 8), 
				Utils.PrefsAccess.persistenceSFX, menuButtons [Utils.PrefsAccess.persistenceSFX ? 5 : 6], GUIStyle.none));
	}

	//credits
	void startCredits(){
		wiggleTimer.Stop ();
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
		rotationAngle = 0;
	}
	void creditsGUI(){
		// TODO credit text
	}

	void titleGUI(){
		GUI.BeginGroup (new Rect(0,0, width, height));
		GUIUtility.RotateAroundPivot (rotationAngle, new Vector2(width/2,height/2));
		GUI.DrawTexture (new Rect(width * 1.1f / 5, height / 4, width / 2, height / 3f), gameTitle, ScaleMode.ScaleToFit);
		GUI.EndGroup ();
	}
	void doWiggle(){
		if (wigglePhase == 4) {
			wiggleTimer.Start ();
			wigglePhase = 0;
		} else if (wigglePhase%2 == 0){
			rotationAngle = Mathf.LerpAngle (rotationAngle, maxRotation, 0.3f);
			if (Mathf.RoundToInt(rotationAngle)== Mathf.RoundToInt(maxRotation)) {
				++wigglePhase;
			}
		} else if (wigglePhase%2 == 1) {
			rotationAngle = Mathf.LerpAngle (rotationAngle, -maxRotation, 0.4f);
			if (Mathf.RoundToInt(rotationAngle)== Mathf.RoundToInt(-maxRotation)) {
				++wigglePhase;
			}
		}
	}
}
