using UnityEngine;
using System.Collections;

public class SettingsGUI : MonoBehaviour {
	
	int height, width;
	public Texture menuBG;
	public Texture[] sfx, music;
	bool isSfx, isMusic;
	int cek;

	string[] menuButtons;

	// Use this for initialization
	void Start () {
		isSfx = false;
		isMusic = false;
		height = Screen.height;
		width = Screen.width;
		sfx = new Texture[2];
		music = new Texture[2];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		toggleSFX(GUI.Toggle (new Rect (width / 2, height / 4, width / 15, width / 15), isSfx, sfx [isSfx? 0 : 1], GUIStyle.none));
		toggleMusic(GUI.Toggle (new Rect (width * 11 / 15, height / 4, width / 15, width / 15), isMusic, music [isMusic? 0 : 1], GUIStyle.none));
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 8 / 15, height * 3 / 5, width / 15, height / 18), "Reset")) { //reset
			
		}
	}

	void toggleSFX(bool newSFX){
		if (newSFX != isSfx) {
			// TODO something

			isSfx = newSFX;
		}
	}

	void toggleMusic(bool newMusic){
		if (newMusic != isMusic) {
			// TODO something

			isMusic = newMusic;
		}
	}

	void reset(){
		// TODO do something
	}
}
