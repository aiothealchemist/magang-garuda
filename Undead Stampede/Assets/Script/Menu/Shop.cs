using UnityEngine;
using System.Collections;
using System.Linq;

public class Shop : BaseMenu {
	
	LoadableContent[] content;
	LoadableContent.currency currency;
	LoadableContent.loadedContentType contentType;
	Texture2D[] contentButtons, shopButtons;

	buttonTexture buyButton;
	int chosenContent, chosenShop;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		chosenShop = 0;
		viewVector = Vector2.zero;
//		loadShop (LoadableContent.loadedContentType.Weapon);
	}

	protected override void loadResources ()
	{
		menuType = type.window;
		buyButton = loadButtonTexture("menu/button/buy");
		backButton = loadButtonTexture("menu/button/back");
		menuBG = Resources.Load<Texture2D>("menu/background/window");
		contentButtons = Resources.LoadAll<Texture2D>("weapons/button");
		shopButtons = Resources.LoadAll<Texture2D>("menu/button/shopmenu");
		bgRect = new Rect (width / 30, height / 18, width * 14 / 15, height * 8 / 9);
	}

	void Update () {
		touchScrollView(true, viewVector);
	}

	protected override void updateGUI () {
		int changeShop = GUI.Toolbar(new Rect(width / 13, height / 6.5f, width * 12.3f / 15, height / 10), 
		                             chosenShop, shopButtons, GUIStyle.none);
		GUI.Box (new Rect (width / 13, height / 3.77f, width * 12.3f / 15, height / 2.3f), string.Empty);

		// ScrollView
		viewVector = GUI.BeginScrollView (new Rect (width / 12, height / 3.3f, width * 12.1f / 15, height / 2.6f), 
			viewVector, new Rect (0, 0, (contentButtons.Length + 1) *  width / 4, height / 3f));
		chosenContent = GUI.Toolbar(
			new Rect (0, 0,(contentButtons.Length + 1) *  width / 4, height / 3f), 
			chosenContent, contentButtons, GUIStyle.none);
		GUI.EndScrollView();

		GUI.Box (new Rect (width / 13, height / 1.4f, width / 3, height / 8),"Content name");
		GUI.Box (new Rect (width * 16.5f / 39, height / 1.4f, width / 3.2f, height / 8),"content price");

		if (changeShop != chosenShop){
//			chosenShop = changeShop;
//			loadShop (
//				chosenShop == 0 ? LoadableContent.loadedContentType.Weapon :
//				chosenShop == 1 ? LoadableContent.loadedContentType.Part :
//				chosenShop == 2 ? LoadableContent.loadedContentType.Gem :
//				LoadableContent.loadedContentType.Vehicle
//			);
		}

		ButtonGUI (new Rect (width * 26 / 30, height / 20, width * 2 / 26, height / 10), backButton, string.Empty,
			() => Destroy (this));
		ButtonGUI(new Rect (width * 3.75f / 5, height / 1.4f, width / 6, height / 8), buyButton, string.Empty,
			() => createPrompt(new Utils.voidNoParams[] { buy, null }, new string[] {
				"Do you want to buy " /*+content[chosenContent].name+" for "+
				 content[chosenContent].pricing[currency]+" "+currency*/+"?", 
				"Accept", "Decline" }));
	}

	public void loadShop(LoadableContent.loadedContentType tipe){
		contentType = tipe;
		content = Utils.XMLLoader.loadSpecificXML (tipe);
		currency = tipe == LoadableContent.loadedContentType.Gem ? 
			LoadableContent.currency.realMoney : LoadableContent.currency.gem;
		contentButtons = (Texture2D[]) content.Select (item => item.sprites[LoadableContent.textureTypes.button]);
	}
	
	void buy () {
		// TODO shop for gem
		if (content[chosenContent].pricing[currency] > Utils.PrefsAccess.Gem) {
			createPrompt (new Utils.voidNoParams[] { null, null },
					new string[] { "Susdulu, duitnya ga cukup coy", "okay", "okay" });
		} else {
			Utils.PrefsAccess.substractGems (content[chosenContent].pricing[currency]);
			Utils.PrefsAccess.setEnumeratedBooleanPrefs("Unlocked"+contentType.ToString (), chosenContent, true);
		}
	}
}
