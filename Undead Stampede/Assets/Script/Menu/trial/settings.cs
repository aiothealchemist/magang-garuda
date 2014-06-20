using UnityEngine;
using System.Collections;

public class settings : MonoBehaviour {
	
	int height, width;
	public Texture menuBG;
	public Texture[] sfx, music;
	
	int selectedButton = 0;
	string[] menuButtons;

	// Use this for initialization
	void Start () {
		height = Screen.height;
		width = Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 4 / 5, height * 7 / 9), menuBG, GUIStyle.none);
		if (GUI.Button (new Rect ())) { //reset

		} else if (true) { //sfx
			
		} else if (true) { //music
			
		} else if (true) { //back
			
		}
	}
}
