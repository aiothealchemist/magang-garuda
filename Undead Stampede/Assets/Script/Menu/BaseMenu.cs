using UnityEngine;
using System.Collections;

public abstract class BaseMenu : MonoBehaviour {

	//menu type
	protected enum type {popup, window, menu};
	protected type menuType;
	protected BaseMenu window, parent;
	protected PopUp popup;

	//punyaan anak
	protected AudioSource bgm, sfx;
	protected Texture2D backButton;
	protected Vector2 viewVector;
	protected int height, width;
	protected Texture menuBG;
	protected Rect bgRect;

	//abstracts and virtuals
	protected abstract void loadResources ();	//resources and menuType
	protected abstract void updateGUI ();
	protected virtual void updateBlockableGUI () {}
	protected virtual void onPopupWindow () {}
	
	//built-in
	protected virtual void Start () {
		width = Screen.width; height = Screen.height;
		bgRect = new Rect (0, 0, width, height);
		loadResources ();
	}
	void OnGUI () {
		GUI.depth = (int) menuType;
		GUI.DrawTexture (bgRect, menuBG, ScaleMode.StretchToFill);
		if ((parent ?? this).popup == null){
			if (window == null) updateBlockableGUI();
			updateGUI ();
		}
	}
	void OnDestroy () {
		Destroy (window);
		Destroy (popup);
	}

	//menu flow
	protected void createPrompt (Utils.delegateVoidWithZeroParam[] method, string[] dialog) {
		if (menuType == type.window) {
			parent.createPrompt (method, dialog);
		} else {
			popup = gameObject.AddComponent<PopUp> ();
			popup.setVar (dialog, method);
		}
	}
	protected void setWindow(BaseMenu newSC){
		Destroy (this.window);
		this.window = newSC;
		newSC.parent = this;
	}

	//buttonGUI
	protected bool ButtonGUI(Rect rect, Texture2D[] textures){
		GUI.skin.button.normal.background = textures[0];
		GUI.skin.button.hover.background = textures[1];
		GUI.skin.button.active.background = textures[2];

		return GUI.Button (rect,"");
	}
}
