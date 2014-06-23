using UnityEngine;
using System.Collections;

public class BaseShopGUI : MonoBehaviour {
	
	int height, width;
	public Texture menuBG;
	public DLC[] dlcs;

	int dlcButtonHeight, viewHeight, viewWidth;
	Vector2 viewVector;
	string currency;
	Texture objImage;
	string objDesc;
	int objPrice;

	// Use this for initialization
	void Start () {
		height = Screen.height;
		width = Screen.width;

		viewVector = Vector2.zero;
		dlcButtonHeight = height;
		viewHeight = height;
		viewWidth = width;
		currency = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
				viewVector.y += touch.deltaPosition.y;
		}
	}

	void OnGUI () {
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);

		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (25, 25, viewWidth, viewHeight), 
		                                  viewVector, new Rect (0, 0, viewWidth, dlcs.Length * dlcButtonHeight));
		for (int i = 0; i < dlcs.Length; i++) {
			if (GUI.Button (new Rect (0 , dlcButtonHeight * i, viewWidth, viewHeight),
			                dlcs[i].buttonImage, GUIStyle.none)) { //DLC button
				objImage = dlcs[i].buttonImage;
				objDesc = dlcs[i].description;
				objPrice = (int) dlcs[i].pricing [currency];
			}
		}
		// End the ScrollView
		GUI.EndScrollView();

		// DLC description
		GUI.DrawTexture (new Rect (width / 2, height / 4, width / 15, width / 15), objImage);
		GUI.Box (new Rect (width / 2, height / 4, width / 15, width / 15), objDesc);
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Buy for " + objPrice)) { //buy
			// TODO Prompt
		}

		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (this);
		}
	}
}
