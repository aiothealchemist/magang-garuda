using UnityEngine;
using System.Collections;

public class PromptGUI : MonoBehaviour {

	int height, width;
	string dialog, yes, no;
	delegatedMethod rmiYes, rmiNo;

	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		GUI.Box (new Rect (width / 5, height * 4 / 15, width * 3 / 5, height * 7 / 15), dialog);
		if (GUI.Button (new Rect (width * 35 / 160, height * 8 / 15, width /4, height / 6), yes)) { // confirm
			if (rmiYes != null)
				rmiYes();
			Destroy (this);
		} else if (GUI.Button (new Rect (width * 17 / 32, height * 8 / 15, width /4, height / 6), no)) { // cancel
			if (rmiNo != null)
				rmiNo();
			Destroy (this);
		}
	}

	public void setVar(string[] dialogStrings, delegatedMethod[] method){
		if (dialogStrings.Length != 3) {
			Debug.Log ("Wrong string array size (3: dialog box, yes button, no button).");
		} else if (method == null ||  method.Length != 2) {
			Debug.Log("Wrong delegatedMethod size (2: yesMethod, noMethod).");
		} else {
			dialog = dialogStrings[0];
			yes = dialogStrings[1];
			no = dialogStrings[2];

			rmiYes = method[0];
			rmiNo = method[1];
		}
	}
}
