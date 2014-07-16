using UnityEngine;
using System.Collections;
using System.Linq;

public class Achievements : BaseMenu {

	AchievementXML[] achievementList;
	BitArray unlockedAchievement;
	int achievementCount;

	protected override void Start () {
		base.Start ();
//		achievementList = System.Array.ConvertAll<LoadableContent, AchievementXML> 
//			( utils.loadSpecificXML(LoadableContent.loadedContentType.Achievement), item => (AchievementXML)item );
//		unlockedAchievement = Utils.PrefsAccess.getEnumeratedBooleanPrefs (Utils.PrefsAccess.enumeratedBooleanKey.UnlockedAchievement);
		achievementCount = /*achievementList.Length ?? */ 20;
	}

	protected override void loadResources () {
		menuType = type.window;
		backButton = Resources.Load<Texture2D>("button/back");
		menuBG = Resources.Load<Texture2D>("background/window");
		bgRect = new Rect (width / 30, height / 18, width * 14 / 15, height * 13 / 18);
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

		if (GUI.Button (new Rect (width * 26 / 30, height / 20, width * 2 / 26, height / 10), backButton, GUIStyle.none)) { 
			//back
			Destroy (this);
		}
	}
}
