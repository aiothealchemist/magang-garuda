using UnityEngine;
using System.Collections;

public class MainMenu : Menu {

	public Transform settingsSC;
	public Transform creditsSC;
	public Transform exitPrompt;

	public override void chosen (string name){
		switch (name) {
			case "play":
				Application.LoadLevel(2);
				break;
			case "settings":
				instantShowcase(settingsSC);
				break;
			case "credits":
				instantShowcase(creditsSC);
				break;
			case "exit":
				instantPrompt(exitPrompt, 
						new string[] { "Are you sure you want to Quit?", "Yes", "No"}, 
						new delegatedMethod[] {exit, null});
				break;
			default:
				//what the fuck are you doing?
				break;
		}
	}

	//Exit game sequence
	public static void exit(){
		Debug.Log ("harusnya quit");
		Application.Quit ();
	}
}
