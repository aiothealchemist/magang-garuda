using UnityEngine;
using System.Collections;

public class EquipWeapon : BaseMenu {
	
	WeaponXML[] contents;
	System.Collections.Generic.List<WeaponXML> equipped;
	Texture2D[] contentButtons;

	buttonTexture playButton;
	int chosenWeapon, equippedWeapon, maxAvailableEquip = 3, maxEquip = 6;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		viewVector = Vector2.zero;
		equipped = new System.Collections.Generic.List<WeaponXML> ();
//		contents = System.Array.ConvertAll(
//			Utils.XMLLoader.loadSpecificXML (LoadableContent.loadedContentType.Weapon), 
//			item => (WeaponXML) item
//		);
//		contentButtons = new Texture2D[contents.Length];
//		for (int i = 0; i < contents.Length; i++) {
//			contentButtons[i] = contents[i].textures[LoadableContent.textureTypes.button];
//		}
	}

	protected override void loadResources () {
		menuType = type.window;
		backButton = loadButtonTexture("menu/button/back");
		menuBG = Resources.Load<Texture2D>("menu/background/window");
		contentButtons = Resources.LoadAll<Texture2D>("weapons/button");
		playButton = loadButtonTexture("menu/button/mainmenu/mainmenu_0");
		bgRect = new Rect (width / 30, height / 18, width * 14 / 15, height * 8 / 9);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
				viewVector.x += touch.deltaPosition.x;
		}
	}

	protected override void updateGUI () {
		GUI.Box (new Rect(width / 3.1f, height / 7, width / 3, height / 16), "Select your weapon");
		GUI.Box (new Rect (width / 13, height / 4.8f, width * 12.3f / 15, height * 2.5f / 9), "Available Weapons");
		// ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width / 12, height / 4f, width * 12.1f / 15, height * 2.5f / 10.8f), 
			viewVector, new Rect (0, 0, (contentButtons.Length + 1) * width / 6, height * 2.4f / 12f));
		chosenWeapon = GUI.Toolbar(
			new Rect (0, 0,(contentButtons.Length + 1) *  width / 6, height * 2.4f / 12f), -1, contentButtons, GUIStyle.none);
		GUI.EndScrollView();

		for (int i = 0; i < maxEquip; i++){
			if (GUI.Button(new Rect(width * (i%3 / 5.8f + 1 /9f), height * (1 / 2f + i / 3 / 6f), width / 6,height * 5.6f / 36f),
					"No data."))
				if (equipped[i] != null) equipped.RemoveAt (i);
		}

		if (GUI.changed){
			if (chosenWeapon != -1 && !equipped.Contains (contents[chosenWeapon])){
				if (equippedWeapon < maxAvailableEquip ) {
					equipped[equippedWeapon++] = contents[chosenWeapon];
				} else {
					createPrompt (new Utils.delegateVoidWithZeroParam[2], 
						new string[] {"Please buy more slot to equip weapon", "okay", "okay"});
				}
			}
		}

		if (ButtonGUI (new Rect (width * 26 / 30, height / 20, width * 2 / 26, height / 10), backButton)) { 
			//back
			Destroy (this);
		} else if (ButtonGUI (new Rect (width * 3.35f / 5, height / 1.65f, width / 6, height / 8), playButton)) {

		}
	}
}
