using UnityEngine;
using System.Collections;

public class GaragemenuGUI : BaseMenu {
	
	int selectedButton = 0;
	string[] menuButtons;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuButtons = new string[] { "Arsenal", "Parts", "Vehicles"};
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (0, height / 8, width / 3, height * 3 / 4), menuBG);	//GUIStyle.none

		selectedButton = GUI.SelectionGrid (
			new Rect (0, height * 3 / 16, width / 3, height / 2), 
			selectedButton, menuButtons, 1);
		
		// If the user clicked a new Toolbar button this frame, we'll process their input
		if (GUI.changed)
		{
			switch (selectedButton) {
			case 0:	//arsenal
				setShowcase (gameObject.AddComponent<BaseShopGUI>());
				break;
			case 1:	//parts
				setShowcase (gameObject.AddComponent<BaseShopGUI>());
				break;
			case 2:	//vehicle
				setShowcase (gameObject.AddComponent<BaseShopGUI>());
				break;
			default:
				break;
			}
		}
		
		if (GUI.Button(new Rect(0, height/8,width/7,height/16),"Back")){
			gameObject.AddComponent<PlaymenuGUI>();
			Destroy (this);
		}
	}
}
