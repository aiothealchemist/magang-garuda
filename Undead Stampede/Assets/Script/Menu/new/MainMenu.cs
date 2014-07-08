using UnityEngine;
using System.Collections;

public class MainMenu : BaseMenu {

	Texture2D[] menuButtons;

	bool BGM {
		get{return utils.persistenceBGM;}
		set{
			utils.persistenceBGM = value;

		}
	}
	bool SFX {
		get{return utils.persistenceSFX;}
		set{
			utils.persistenceSFX = value;

		}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		menuButtons = Resources.LoadAll<Texture2D> ("button/mainmenu");
		menuBG = Resources.Load<Texture2D> ("background/mainmenu");
	}
	
	protected override void updateGUI () {
		BGM = (GUI.Toggle (new Rect (0, 0, 0, 0), BGM, menuButtons [BGM ? 3 : 4], GUIStyle.none));
		SFX = (GUI.Toggle (new Rect (0, 0, 0, 0), SFX, menuButtons [SFX ? 5 : 6], GUIStyle.none));

		if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [0], GUIStyle.none)) {		//play
			gameObject.AddComponent<PlaymenuGUI>();	Destroy (this);
		} else if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [1], GUIStyle.none)) {//achievement
			setShowcase (gameObject.AddComponent<AchievementsGUI>());
		} else if (GUI.Button (new Rect (0, 0, 0, 0), menuButtons [2], GUIStyle.none)) {//credits
			setShowcase (gameObject.AddComponent<CreditsGUI>());
		} 
	}
}
