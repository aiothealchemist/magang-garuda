using UnityEngine;
using System.Collections;

public class SettingsGUI : BaseMenu {
	
	public Texture[] sfx, music;
	bool isSfx, isMusic;
	int cek;

	string[] menuButtons;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuType = type.window;

		sfx = new Texture[] {
			(Texture2D)Resources.LoadAssetAtPath("Assets/Sprites/speaker1.png", typeof(Texture)), 
			(Texture2D)Resources.LoadAssetAtPath("Assets/Sprites/speaker2.png", typeof(Texture))
		};
		music = new Texture[] {
			(Texture2D)Resources.LoadAssetAtPath("Assets/Sprites/not1.png", typeof(Texture)), 
			(Texture2D)Resources.LoadAssetAtPath("Assets/Sprites/not2.png", typeof(Texture))
		};
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);

		toggleSFX(GUI.Toggle (new Rect (width / 2, height / 4, width / 15, width / 15), isSfx, sfx [isSfx? 0 : 1], GUIStyle.none));
		toggleMusic(GUI.Toggle (new Rect (width * 11 / 15, height / 4, width / 15, width / 15), isMusic, music [isMusic? 0 : 1], GUIStyle.none));

		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 8 / 15, height * 3 / 5, width / 15, height / 18), "Reset")) { //reset
			createPrompt (new delegateVoidWithZeroParam[]{ reset, null },
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
