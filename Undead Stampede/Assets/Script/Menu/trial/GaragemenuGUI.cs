using UnityEngine;
using System.Collections;

public class GaragemenuGUI : MonoBehaviour {
	
	int height, width;
	public Texture menuBG;
	
	int selectedButton = 0;
	string[] menuButtons;
	
	// Use this for initialization
	void Start () {
		height = Screen.height;
		width = Screen.width;
		menuButtons = new string[] { "Arsenal", "Parts", "Vehicles"};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		GUI.Box (new Rect (0, height / 8, width / 3, height * 3 / 4), menuBG, GUIStyle.none);

		selectedButton = GUI.SelectionGrid (
			new Rect (0, height * 3 / 16, width / 3, height * 3 / 4), 
			selectedButton, menuButtons, 1);
		
		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:	//arsenal
				gameObject.AddComponent<BaseShopGUI>();
				break;
			case 1:	//parts
				gameObject.AddComponent<BaseShopGUI>();
				break;
			case 2:	//vehicle
				gameObject.AddComponent<BaseShopGUI>();
				break;
			default:
				break;
			}
		}
		
		if (GUI.Button(new Rect(0, height/8,width/7,height*3/16), "Back")){
			gameObject.AddComponent<PlaymenuGUI>();
			Destroy (this);
		}
	}
}
