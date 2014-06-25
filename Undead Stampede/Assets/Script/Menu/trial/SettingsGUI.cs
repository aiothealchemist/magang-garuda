using UnityEngine;
using System.Collections;

public class SettingsGUI : BasemenuGUI {
	
	public Texture[] sfx, music;
	bool isSfx, isMusic;
	int cek;

	string[] menuButtons;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuType = type.showcase;

		isSfx = utils.sfxON;
		isMusic = utils.musicON;

		sfx = new Texture[2];
		music = new Texture[2];
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);

		toggleSFX(GUI.Toggle (new Rect (width / 2, height / 4, width / 15, width / 15), isSfx, sfx [isSfx? 0 : 1]));	//GUIStyle.none
		toggleMusic(GUI.Toggle (new Rect (width * 11 / 15, height / 4, width / 15, width / 15), isMusic, music [isMusic? 0 : 1])); 	//GUIStyle.none

		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 8 / 15, height * 3 / 5, width / 15, height / 18), "Reset")) { //reset
			createPrompt (new delegatedMethod[]{ new delegatedMethod(reset), null },
					new string[] {"Are you sure you want to reset all saved data?","Yes","NO!"});
		}
	}

	void toggleSFX(bool newSFX){
		if (newSFX != isSfx) {
			// TODO set mute/unmute sound effects

			isSfx = newSFX;
		}
	}

	void toggleMusic(bool newMusic){
		if (newMusic != isMusic) {
			// TODO set mute/unmute music

			isMusic = newMusic;
		}
	}
	
	void reset () {
		// TODO do something
		Debug.Log ("nganu dibunuh");
	}
}
