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
		menuBG = Resources.Load<Texture2D>("menu/background/mainmenu");
		backButton = loadButtonTexture("menu/button/back 1");
		shopButton = loadButtonTexture("menu/button/shop");
	}

	protected override void updateGUI () { }
	protected override void updateBlockableGUI () {
		if (ButtonGUI (new Rect (width * 12 / 15, height / 100, width / 10.9f, height / 8), backButton)){
			//back
			gameObject.AddComponent<MainMenu>();
			Destroy(this);
		} else if (ButtonGUI (new Rect (width * 13.5f / 15, height / 100, width / 10.9f, height / 8), shopButton)) {
			//shop
			setWindow (gameObject.AddComponent<Shop>());
		} else if (GUI.Button(new Rect (width * 10.5f / 15, height / 100, width / 10.9f, height / 8),"Play")) {
			setWindow (gameObject.AddComponent<EquipWeapon>());
		}

//		int unlockedLvl = Utils.PrefsAccess.getIntegerPrefs (Utils.PrefsAccess.integerKey.UnlockedLevel);
//		BitArray masteredLvl = Utils.PrefsAccess.getEnumeratedBooleanPrefs (Utils.PrefsAccess.enumeratedBooleanKey.MasteredLevel);
//		for (int i = 0; i < /*unlockedLvl*/ levelButtons.Count; i++)
//			if (ButtonGUI (levelButtons[i], masteredLvl[i] ? masteredLevelButton : unlockedLevelButton))
//				levelChosen (i);
//		levelButtons.Skip (unlockedLvl).ToList ().ForEach (item => {if (ButtonGUI (item, lockedLevelButton));});
	}

	void initLevelButtons(){	//level buttons position
		levelButtons = new System.Collections.Generic.List<Rect> ();
		levelButtons.Add (new Rect(0,0,0,0));
	}

	void levelChosen (int levelNum) {
		setWindow (gameObject.AddComponent<EquipWeapon> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
