using UnityEngine;
using System.Collections;

public class PlayMenu : Menu {
	
	public Transform chooseWeapon;
	public Transform garage;
	public Transform achievements;
	public Transform gemshop;
	
	public override void chosen (string name){
		switch (name) {
		case "garage":
			Application.LoadLevel(3);
			break;
		case "achievements":
			instantShowcase(achievements);
			break;
		case "gemshop":
			instantShowcase(gemshop);
			break;
		case "back":
			Application.LoadLevel (1);
			break;
		default:
			if (name.Substring(0,4).Equals("play")){
				instantShowcase(chooseWeapon);
				int[] level = new int[] {
						int.Parse(name[4].ToString(),System.Globalization.NumberStyles.HexNumber), 
						int.Parse(name[10].ToString(),System.Globalization.NumberStyles.HexNumber)};
				showcaseMenu.GetComponent<ChooseWeapon>().setLevel(level);
			}
			break;
		}
	}

	public void levelButton(bool on){
		(new System.Collections.Generic.List<GameObject> (GameObject.FindGameObjectsWithTag ("levelbutton")))
			.ForEach(delegate(GameObject gObject) {
				gObject.GetComponent<Button>().isFocused = on;
		});
	}
}