using UnityEngine;
using System.Collections;

public class Settings : Menu {
	
	public Transform resetPrompt;

	public override void chosen (string name){
		switch (name) {
		case "music":
			toggleMusic();
			break;
		case "sfx":
			toggleSFX();
			break;
		case "reset":
			instantPrompt(resetPrompt, 
					new string[] {"Are you sure you want to reset all saved data?","Yes","NO!"}, 
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
	
	//toggle music on/off
	void toggleMusic() {
		utils.musicON = !utils.musicON;
		transform.FindChild ("music").GetComponent<Button> ().toggle (true);
		if (audioSource != null)
			audioSource.mute = utils.musicON;
		if (parentMenu.audioSource != null)
			parentMenu.audioSource.mute = utils.musicON;
	}
	
	//toggle sound effects on/off
	void toggleSFX() {
		utils.sfxON = !utils.sfxON;
		transform.FindChild ("sfx").GetComponent<Button> ().toggle (true);
	}
	
	//reset the game
	void reset() {
		
	}
}
