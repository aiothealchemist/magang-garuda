using UnityEngine;
using System.Collections;

public class Credits : Menu {
	
	public override void chosen (string name){
		switch (name) {
		case "back":
			Destroy(gameObject);
			break;
		default:
			//what the fuck are you doing?
			break;
		}
	}
}
