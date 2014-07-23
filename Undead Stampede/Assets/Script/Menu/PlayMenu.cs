using UnityEngine;
using System.Collections;
using System.Linq;

public class PlayMenu : BaseMenu {

	buttonTexture shopButton, lockedLevelButton, unlockedLevelButton, masteredLevelButton;

	System.Collections.Generic.List<Rect> levelButtons;
	level levelConstructor;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		initLevelButtons ();
	}

	protected override void loadResources () {
		menuType = type.menu;
		menuBG = Resources.Load<Texture2D>("menu/background/playmenu");
		masteredLevelButton = loadButtonTexture ("menu/button/playmenu/mastered");
		unlockedLevelButton = loadButtonTexture ("menu/button/playmenu/unlocked");
		backButton = loadButtonTexture("menu/button/playmenu/back");
		shopButton = loadButtonTexture("menu/button/playmenu/shop");
	}

	protected override void updateGUI () {
		GUI.Box (new Rect(0, height * 7 / 8, width / 5, height / 8), "");
		if (ButtonGUI (new Rect (0, height * 7 / 8, width / 10.9f, height / 8), backButton)){
			//back
			gameObject.AddComponent<MainMenu>();
			Destroy(this);
		} else if (ButtonGUI (new Rect (width * 1.5f / 15, height * 7 / 8, width / 10.9f, height / 8), shopButton)) {
			//shop
			setWindow (gameObject.AddComponent<Shop>());
		}
	}
	protected override void updateBlockableGUI () {
		if (GUI.Button(new Rect (width * 1.5f / 15, height / 8, width / 22, height / 16),"Play")) {
			setWindow (gameObject.AddComponent<EquipWeapon>());
		} else if (GUI.Button(new Rect (width * (1.5f / 15 + 3 / 21f), height * (1 / 8f + 1 / 8f - 1 / 32f), width / 22, height / 16),"Play")) {
			setWindow (gameObject.AddComponent<EquipWeapon>());
		} else if (GUI.Button(new Rect (width * (1.5f / 15 + 1 / 40f), height * (1 / 8f + 1 / 8f), width / 22, height / 16),"Play")) {
			setWindow (gameObject.AddComponent<EquipWeapon>());
		}

		int unlockedLvl = 3; //Utils.PrefsAccess.getIntegerPrefs (Utils.PrefsAccess.integerKey.UnlockedLevel);
		BitArray masteredLvl = Utils.PrefsAccess.ToBinary (1); //Utils.PrefsAccess.getEnumeratedBooleanPrefs (Utils.PrefsAccess.enumeratedBooleanKey.MasteredLevel);
		for (int i = 0; i < unlockedLvl; i++)
			if (ButtonGUI (levelButtons[i], masteredLvl[i] ? masteredLevelButton : unlockedLevelButton))
				levelChosen (i);
//		System.Array.ForEach(levelButtons.Skip (unlockedLvl),item => {if (ButtonGUI (item, lockedLevelButton));});
	}

	void initLevelButtons(){	//level buttons position
		levelButtons = new System.Collections.Generic.List<Rect> ();
		levelButtons.Add (new Rect (width * 1.5f / 15, height / 8, width / 22, height / 16));
		levelButtons.Add (new Rect (width * (1.5f / 15 + 3 / 21f), height * (1 / 8f + 1 / 8f - 1 / 32f), width / 22, height / 16));
		levelButtons.Add (new Rect (width * (1.5f / 15 + 1 / 40f), height * (1 / 8f + 1 / 8f), width / 22, height / 16));
	}

	void levelChosen (int levelNum) {
		setWindow (gameObject.AddComponent<EquipWeapon> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
