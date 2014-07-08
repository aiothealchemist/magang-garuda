using UnityEngine;
using System.Collections;

public class BaseShopGUI : BaseMenu {
	
	public LoadableContent[] content { get; set; }
	LoadableContent.currency currency { get; set; }
	LoadableContent.loadedContentType contentType;
	Texture2D[] menuButtons;

	Vector2 viewVector;
	int chosenContent;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		viewVector = Vector2.zero;
		menuType = type.window;
		content = new LoadableContent[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
				viewVector.y += touch.deltaPosition.y;
		}
	}

	protected override void updateGUI () {
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);

		GUI.Box (new Rect (width * 21 / 30, height * 2.5f / 18, width * 4 / 15, height * 7 / 9),"scrollable view");
		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width * 11 / 15, height * 2.5f / 18, width * 3 / 15, height * 7 / 9), 
		                                  viewVector, new Rect (0, 0, width * 3 / 15 - 18, (content.Length) * height * 4 / 15));
		chosenContent = GUI.SelectionGrid(
			new Rect (0, 0, width * 3 / 15 - 18, (content.Length) * height * 4 / 15),
			chosenContent, menuButtons, 1, GUIStyle.none);

		// End the ScrollView
		GUI.EndScrollView();

		// DLC description
		GUI.Box (new Rect (width * 12 / 30, height * 2.5f / 18, width * 4 / 15, width / 5), content[chosenContent].textures[LoadableContent.textureTypes.button]);
		GUI.Box (new Rect (width * 12 / 30, height * 7 / 15, width * 4 / 15, width / 5), content[chosenContent].description);
		GUI.Box (new Rect (width * 12 / 30, height * 12 / 15, width * 2 / 15, height * 0.115f), "Price\n" + content[chosenContent].pricing[currency]);	//objPrice
		if (GUI.Button (new Rect (width * 16 / 30, height * 12 / 15, width * 2 / 15, height * 0.115f), "Buy")) {
			createPrompt(new delegateVoidWithZeroParam[] { buy, null }, new string[] { 
					"Do you want to buy "+content[chosenContent].name +" for "+content[chosenContent].pricing[currency]+" "+currency+"?", 
					"Accept", "Decline" }
			);
		}
		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}

	public void loadShop(LoadableContent.loadedContentType tipe){
		contentType = tipe;
		content = utils.loadSpecificXML (tipe);
		currency = tipe != LoadableContent.loadedContentType.Gem ? 
			LoadableContent.currency.gem : LoadableContent.currency.realMoney;
		menuButtons = new Texture2D[content.Length];
		for (int i = 0; i < content.Length; i++) {
			menuButtons[i] = content[i].textures[LoadableContent.textureTypes.button];
		}
	}
	
	void buy () {
		// TODO shop for gem
		if (content[chosenContent].pricing[currency] > utils.Gem) {
			createPrompt (new delegateVoidWithZeroParam[] { null, null },
					new string[] { "Susdulu, duitnya ga cukup coy", "okay", "okay" });
		} else {
			utils.substractGems (content[chosenContent].pricing[currency]);
			utils.setEnumeratedBooleanPrefs("Unlocked"+contentType.ToString (), chosenContent, true);
		}
	}
}
