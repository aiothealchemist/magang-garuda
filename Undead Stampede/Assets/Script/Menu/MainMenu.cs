﻿using UnityEngine;
using System.Collections;

public class MainMenu : BaseMenu {

	System.Timers.Timer wiggleTimer;
	float maxRotation = 6, rotationAngle;
	double timerInterval = 3000;
	int wigglePhase;

	float credButtX, achiButtX, playButtY, audiButtY;
	bool credits, creditText;

	buttonTexture[] menuButtons;
	Texture2D gameTitle;

	//built-in
	protected override void Start () {
		wiggleTimer = new System.Timers.Timer(timerInterval);
		wiggleTimer.Elapsed += (sender, e) => wiggleTimer.Stop ();
		base.Start ();
		resetCredits ();
	}
	void Update () {
		if (credits) {
			if (Input.GetMouseButtonDown(0)) {
				resetCredits ();
			} else if (!creditText) {
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
		gameTitle = Resources.Load<Texture2D> ("menu/background/mainmenu_title");
		menuButtons = new buttonTexture[]{
			loadButtonTexture ("menu/button/mainmenu/mainmenu_0"),
			loadButtonTexture ("menu/button/mainmenu/mainmenu_1"),
			loadButtonTexture ("menu/button/mainmenu/mainmenu_2"),
			loadToggleTexture ("menu/button/mainmenu/mainmenu_3"),
			loadToggleTexture ("menu/button/mainmenu/mainmenu_4"),
		};
	}
	protected override void updateGUI () {
		ButtonGUI (new Rect (width * 2.38f / 7, playButtY, width / 3.1f, height  / 5), menuButtons[0], string.Empty,
			() => {gameObject.AddComponent<PlayMenu>(); Destroy (this);});
		ButtonGUI (new Rect (achiButtX, height * 3.1f / 4, width * 3.3f / 10, height/6), menuButtons[1], string.Empty,
			() => setWindow (gameObject.AddComponent<Achievements>()));
		ButtonGUI (new Rect (credButtX, height * 3.1f / 4, width * 3.3f / 10, height/6), menuButtons[2], string.Empty,
			() => {Destroy (window); credits = true;});

		if (creditText){
			creditsGUI ();
		}

		//wiggle title
		GUIUtility.RotateAroundPivot (rotationAngle, new Vector2(width/2,height/2));
		GUI.DrawTexture (new Rect(width * 1.1f / 5, height / 4, width / 2, height / 3f), gameTitle, ScaleMode.ScaleToFit);
	}
	protected override void updateBlockableGUI () {
		Utils.PrefsAccess.persistenceBGM = ToggleGUI( new Rect (width * 12 / 15, audiButtY, width / 11f, height / 8), 
				menuButtons [3], Utils.PrefsAccess.persistenceBGM );
		Utils.PrefsAccess.persistenceSFX = ToggleGUI (new Rect (width * 13.5f / 15, audiButtY, width / 11, height / 8), 
				menuButtons [4], Utils.PrefsAccess.persistenceSFX );
	}

	//credits
	void startCredits(){
		wiggleTimer.Stop ();
		credButtX = Mathf.Lerp (credButtX, width, 0.1f);
		achiButtX = Mathf.Lerp (achiButtX, -width * 3.3f / 10, 0.1f);
		playButtY = Mathf.Lerp (playButtY, height, 0.1f);
		audiButtY = Mathf.Lerp (audiButtY, -height / 8, 0.1f);
		if (Mathf.RoundToInt (playButtY) == height) creditText = true;
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
		// TODO credit text using gui label, and move upward as time goes by
		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.Label (new Rect(width / 3, height / 1.8f, width / 3, height / 2), 
				"Game Designers & Programmers:\n" +
					"Purwoko C. Nugroho\n(rprwk.nugroho@hotmail.com),\n" +
					"Tino E. K. Sambora\n(krisnasambora@gmail.com)\n" +
				"\nArtists:\n" + 
					"Aditiyo Dwi Putro\n(dwibeowulfz@gmail.com) @dwibeowulfz\n" + 
					"Adimas F.\n(d.dimazzz@yahoo.com) @dimas_ef\n" +
				"\nSpecial Thanks to:\n" +
					"Garuda Studio as Incubator");
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
			rotationAngle = Mathf.LerpAngle (rotationAngle, maxRotation, 0.4f);
			if (Mathf.RoundToInt(rotationAngle)== Mathf.RoundToInt(maxRotation)) {
				++wigglePhase;
			}
		} else if (wigglePhase%2 == 1) {
			rotationAngle = Mathf.LerpAngle (rotationAngle, -maxRotation, 0.5f);
			if (Mathf.RoundToInt(rotationAngle)== Mathf.RoundToInt(-maxRotation)) {
				++wigglePhase;
			}
		}
	}
}
