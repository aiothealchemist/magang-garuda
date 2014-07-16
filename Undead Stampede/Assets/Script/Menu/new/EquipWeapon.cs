using UnityEngine;
using System.Collections;

public class EquipWeapon : BaseMenu {
	
	WeaponXML[] contents, equipped;
	string[] tempButtonStrings;

	Texture2D playButton;
	int chosenWeapon;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		viewVector = Vector2.zero;
		equipped = new WeaponXML[3];	//3 yang lain in-app purchase
//		contents = System.Array.ConvertAll(
//			Utils.XMLLoader.loadSpecificXML (LoadableContent.loadedContentType.Weapon), 
//			item => (WeaponXML) item
//		);
//		equipButtons = new System.Collections.Generic.List<Texture2D>(contents.Length);
//		System.Array.ForEach(
//			contents, 
//			item => equipButtons.Add (
//				item.textures[LoadableContent.textureTypes.button]
//			)
//		);
//		equippedContents = new bool[contents.Length];
		tempButtonStrings = new string[]{ "Weapons", "Parts", "Vehicles", "Gems" };
	}

	protected override void loadResources () {
		menuType = type.window;
		backButton = Resources.Load<Texture2D>("button/back");
		menuBG = Resources.Load<Texture2D>("background/window");
		playButton = Resources.Load<Texture2D>("button/mainmenu/mainmenu_1");
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
		GUI.Box (new Rect(width / 3.1f, height / 7, width / 3, height / 16),"Select your weapon");
		GUI.Box (new Rect (width / 13, height / 4.8f, width * 12.3f / 15, height * 2.5f / 9), "Available Weapons");
		// ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width / 12, height / 4f, width * 12.1f / 15, height * 2.5f / 10.8f), 
		                                  viewVector, new Rect (0, 0, tempButtonStrings.Length * width * 12.3f / 15, height * 2.4f / 12f));
		chosenWeapon = GUI.Toolbar(
			new Rect (0, 0,(tempButtonStrings.Length) *  width / 6, height * 2.4f / 12f), 
			-1, /*equipButtons ?? */tempButtonStrings);
		GUI.EndScrollView();

		if (chosenWeapon != -1){

		}

		if (GUI.Button (new Rect (width * 26 / 30, height / 20, width * 2 / 26, height / 10), backButton, GUIStyle.none)) { 
			//back
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 3.35f / 5, height / 1.7f, width / 6, height / 8), playButton, GUIStyle.none)) {

		}
	}
}
