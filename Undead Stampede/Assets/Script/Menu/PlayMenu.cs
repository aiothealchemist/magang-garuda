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

		ButtonGUI (new Rect (0, height * 7 / 8, width / 10.9f, height / 8), backButton, string.Empty, 
			() => {gameObject.AddComponent<MainMenu>(); Destroy(this);});
		ButtonGUI (new Rect (width * 1.5f / 15, height * 7 / 8, width / 10.9f, height / 8), shopButton, string.Empty, 
			() => setWindow (gameObject.AddComponent<Shop>()));
	}
	protected override void updateBlockableGUI () {
		int unlockedLvl = 3; //Utils.PrefsAccess.getIntegerPrefs (Utils.PrefsAccess.integerKey.UnlockedLevel);
		BitArray masteredLvl = Utils.PrefsAccess.ToBinary (3); //Utils.PrefsAccess.getEnumeratedBooleanPrefs (Utils.PrefsAccess.enumeratedBooleanKey.MasteredLevel);
		for (int i = 0; i < unlockedLvl; i++){
			GUI.Box (levelButtons[i], string.Empty);
			ButtonGUI (levelButtons[i], masteredLvl[i] ? masteredLevelButton : unlockedLevelButton, string.Empty,
				() => levelChosen (i));
		}
	}

	void initLevelButtons(){	//level buttons position
		levelButtons = new System.Collections.Generic.List<Rect> ();
		levelButtons.Add (new Rect (width * 1.5f / 15, height / 8.1f, width / 18, height / 13));
		levelButtons.Add (new Rect (width * (1.5f / 15 + 1 / 40f), height * (1 / 9f + 1 / 8f), width / 18, height / 13));
		levelButtons.Add (new Rect (width * (1.35f / 15 + 3 / 21f), height * (1 / 9f + 1 / 8f - 1 / 32f), width / 18, height / 13));
	}

	void levelChosen (int levelNum) {
		setWindow (gameObject.AddComponent<EquipWeapon> ());
		levelConstructor = new level ();
		levelConstructor.fillWave (levelNum);
		DontDestroyOnLoad (levelConstructor);
	}
}
