﻿using UnityEngine;
using System.Collections;

public class mainmenu : MonoBehaviour {

	int height, width;
	public Texture menuBG;

	int selectedButton = 0;
	string[] menuButtons;

	// Use this for initialization
	void Start () {
		height = Screen.height;
		width = Screen.width;
		menuButtons = new string[] { "Play", "Settings", "Credits", "Exit" };
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Box (new Rect (0, height / 8, width / 3, height * 3 / 4), menuBG, GUIStyle.none);
		selectedButton = GUI.SelectionGrid (
			new Rect (0, height / 8, width / 3, height * 3 / 4), 
			selectedButton, menuButtons, 1);

		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:
				break;
			case 1:
				break;
			case 2:
				break;
			case 3:
				break;
			default:
				break;
			}
		}
	}
}
