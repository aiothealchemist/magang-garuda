using UnityEngine;
using System.Collections;
using System.Linq;

public class Achievements : BaseMenu {

	AchievementXML[] achievementList;
	BitArray unlockedAchievement;
	int achievementCount;

	protected override void Start () {
		base.Start ();
//		achievementList = Utils.XMLLoader.loadSpecificXML(LoadableContent.loadedContentType.Achievement).Cast<AchievementXML> ().ToArray ();
//		unlockedAchievement = Utils.PrefsAccess.getEnumeratedBooleanPrefs (Utils.PrefsAccess.enumeratedBooleanKey.UnlockedAchievement);
		achievementCount = /*achievementList.Length ?? */ 20;
	}

	protected override void loadResources () {
		menuType = type.window;
		bgRect = new Rect (width / 30, height / 18, width * 14 / 15, height * 13 / 18);
		menuBG = Resources.Load<Texture2D>("menu/background/window");
		backButton = loadButtonTexture("menu/button/back");
	}

	protected override void updateGUI () {
		// ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width * 8 / 90, height * 11 / 90, width * 12.3f / 15, height * 13 / 24),
			viewVector, new Rect (0, 0, width * 12 / 15, achievementCount * height / 7));
		for (int i = 0; i < achievementCount; ++i){
//			GUIContent desc = new GUIContent(
//				achievementList[i].name+" Prize : "+achievementList[i].pricing[LoadableContent.currency.gem]+" Gem(s)",
//				achievementList[i].description);
			GUI.Box (new Rect(0, i * height / 7, width * 12 / 15, height / 7.5f), string.Empty);
			GUI.Toggle (new Rect(0, i * height / 7, width * 12 / 15, height / 7.5f), 
			            /*unlockedAchievement[i] ?? */ true, /*desc ?? */ i.ToString ());
		}
		GUI.EndScrollView();

		if (ButtonGUI (new Rect (width * 26 / 30, height / 20, width * 2 / 26, height / 10), backButton)) { 
			//back
			Destroy (this);
		}
	}
}
