using UnityEngine;
using System.Collections;

public class PromptScript : Menu {

	delegatedMethod yesMethod, noMethod;

	Menu parentScript;

	// Use this for initialization
	protected override void Start (){ }

	public void setVar(Menu parentMenu, string[] dialog, delegatedMethod[] method){
		parentScript = parentMenu;
		if (dialog.Length != 3) {
			Debug.Log ("Wrong string array size (exact 3: dialog box, yes button, no button).");
		} else if (method == null || (method.Length != 1 && method.Length != 2)) {
			Debug.Log("Wrong delegatedMethod size (max 2: yesMethod, noMethod).");
		} else {
			//transform.FindChild ("dialog").GetComponent<Button> ().setText(dialog[0]);	//COY, ini bukan button
			transform.FindChild ("yes").GetComponent<Button> ().setText(dialog[1]);
			transform.FindChild ("no").GetComponent<Button> ().setText(dialog[2]);

			Debug.Log("masuk");
			yesMethod = (method[0] != null ? method[0] : killPrompt);
			noMethod = (method.Length == 2 && method[1] != null ? method[1] : killPrompt);
		}
	}

	public override void chosen(string name){
		switch (name) {
		case "yes":
			yesMethod();
			break;
		case "no":
			noMethod();
			break;
		default:
			break;
		}
	}

	void killPrompt(){
		Debug.Log ("gajadi,gajadi");
		parentScript.toggleFocus (true);
		Destroy (gameObject);
	}
}
