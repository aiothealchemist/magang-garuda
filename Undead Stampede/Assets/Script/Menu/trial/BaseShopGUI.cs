using UnityEngine;
using System.Collections;

public class BaseShopGUI : MonoBehaviour {
	
	int height, width;
	public Texture menuBG;
	public DLC[] dlcs;

	Vector2 viewVector;
	Texture objImage;
	string objDesc;
	int objPrice;


	// Use this for initialization
	void Start () {
		viewVector = Vector2.zero;
		dlcs = new DLC[0];

		height = Screen.height;
		width = Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
		Touch touch = (Input.touchCount > 0 ? Input.touches[0] : null);
		if (touch != null && touch.phase == TouchPhase.Moved)
			viewVector.y += touch.deltaPosition.y;
	}

	void OnGUI () {
		GUI.Box (new Rect (width * 2 / 5, height / 9, width * 8 / 15, height * 7 / 9), menuBG);
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Back")) { //back
			Destroy (gameObject);
		}
		// Begin the ScrollView
		viewVector = GUI.BeginScrollView (new Rect (25, 25, 100, 100), viewVector, new Rect (0, 0, 90, dlcs.Length * 100));
		for (int i = 0; i < dlcs.Length; i++) {
			dlcButton(dlcs[i], i);
		}
		// End the ScrollView
		GUI.EndScrollView();

		GUI.DrawTexture (new Rect (width / 2, height / 4, width / 15, width / 15), objImage);
		GUI.Box (new Rect (width / 2, height / 4, width / 15, width / 15), objDesc);
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), "Buy for " + objPrice)) { //buy
			//Destroy (gameObject);
		}
	}

	void dlcButton(DLC obj, int countSoFar){
		objDesc = obj.description;
		objPrice = obj.pricing ["gem"];
		if (GUI.Button (new Rect (width * 13 / 15, height / 9, width / 15, height / 18), obj.name)) { //DLC button
			//Destroy (gameObject);
		}
	}
}
