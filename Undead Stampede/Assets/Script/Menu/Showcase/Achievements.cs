using UnityEngine;
using System.Collections;

public class Achievements : Menu {
	
	public Transform resetPrefab;

	public override void chosen (string name){
		switch (name) {
		case "reset":
			instantPrompt(resetPrefab, 
					new string[] {"Are you sure you want to reset all achievement you have got?","I'm not a pussy!","No, please."}, 
					new utils.delegatedMethod[] { reset });
			break;
		case "back":
			Destroy(gameObject);
			break;
		default:
			//what the fuck are you doing?
			break;
		}
	}

	//reset the achievement
	void reset() {
		
	}
}
