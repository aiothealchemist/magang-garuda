using UnityEngine;
using System.Collections;

public class BaseShopGUI : BasemenuGUI {
	
	public DLC[] dlcs;

	Vector2 viewVector;
	string currency;
	Texture objImage;
	string objDesc;
	int objPrice, num;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuType = type.showcase;
		dlcs = new DLC[3];

		viewVector = Vector2.zero;
		currency = "";
	}
	
	// Update is called once per frame
//	void Update () {
//		if (Input.touchCount > 0) {
//			Touch touch = Input.touches[0];
//			if (touch.phase == TouchPhase.Moved)
//				viewVector.y += touch.deltaPosition.y;
//		}
//	}

	protected override void updateGUI () {
		GUI.Box (new Rect (width * 11 / 30, height / 18, width * 9 / 15, height * 8 / 9), menuBG);

		GUI.Box (new Rect (width * 21 / 30, height * 2.5f / 18, width * 4 / 15, height * 7 / 9),"scrollable view");
		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width * 11 / 15, height * 2.5f / 18, width * 3 / 15, height * 7 / 9), 
		                                  viewVector, new Rect (0, 0, width * 3 / 15 - 18, (9) * height * 4 / 15));
		for (int i = 0; i < 9; i++) {
			if (GUI.Button (new Rect (0 , (1 * i) * height * 4 / 15, width * 3 / 15 - 18, height * 4 / 15 - 10),
					"Button Image")) { //DLC button , GUIStyle.none
//				objImage = dlcs[i].buttonImage;
//				objDesc = dlcs[i].description;
//				objPrice = (int) dlcs[i].pricing [currency];
				num = i;
			}
		}

		// End the ScrollView
		GUI.EndScrollView();

		// DLC description
		GUI.Box (new Rect (width * 12 / 30, height * 2.5f / 18, width * 4 / 15, width / 5), "Image here");
		GUI.Box (new Rect (width * 12 / 30, height * 7 / 15, width * 4 / 15, width / 5), "Description here");
		GUI.Box (new Rect (width * 12 / 30, height * 12 / 15, width * 2 / 15, height * 0.115f), "Price\n" + num);
		if (GUI.Button (new Rect (width * 16 / 30, height * 12 / 15, width * 2 / 15, height * 0.115f), "Buy for " + objPrice)) { //buy
			// TODO Prompt
			createPrompt(new delegatedMethod[] { new delegatedMethod(buy), null }, new string[3]);
		}

		if (GUI.Button (new Rect (width * 27 / 30, height / 18, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}

	void buy () {

	}
}
