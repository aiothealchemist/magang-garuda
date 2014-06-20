using UnityEngine;
using System.Collections;

public class GarageMenu : Menu {

	public Transform arsenal;
	public Transform part;
	public Transform vehicle;

	//??? harus bikin apa buat ngisi garage
	
	public override void chosen (string name){
		switch (name) {
		case "arsenal":
			instantShowcase(arsenal);
			break;
		case "part":
			instantShowcase(part);
			break;
		case "vehicle":
			instantShowcase(vehicle);
			break;
		case "back":
			Application.LoadLevel(2);
			break;
		default:
			//what the fuck are you doing?
			break;
		}
	}
}
