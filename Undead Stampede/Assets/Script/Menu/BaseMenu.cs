using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class BaseMenu : MonoBehaviour {

	//menu type
	protected enum type {popup, window, menu};
	protected type menuType;
	protected BaseMenu window, parent;
	protected PopUp popup;

	//punyaan anak
	protected AudioSource bgm, sfx;
	protected buttonTexture backButton;
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
		bgRect = new Rect (-2, -1, width+2, height+1);
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
	protected void createPrompt (Utils.voidNoParams[] method, string[] dialog) {
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
	//author: rPrwk
	protected struct buttonTexture {
		public Texture2D inactive, active;
	}
	buttonTexture baseButton;
	protected buttonTexture loadButtonTexture(string texturePath){
		buttonTexture temp;
		temp.inactive = Resources.Load<Texture2D>(texturePath);
		temp.active = new Texture2D(temp.inactive.width, temp.inactive.height);
		Color[] color = new System.Collections.Generic.List<Color>(temp.inactive.GetPixels ())
			.Select (item => (item.a != 0 || (item.r != 0 && item.g != 0 && item.b != 0) ? 
					new Color(item.r += 0.06f, item.g += 0.06f, item.b += 0.06f) : item)).ToArray ();
//		Color[] color = temp.inactive.GetPixels ();
//		for (int i = 0; i < color.Length; ++i) {
//			if (color[i].a != 0 || (color[i].r != 0 && color[i].g != 0 && color[i].b != 0)) {
//				color[i].r += 0.06f; color[i].g += 0.06f; color[i].b += 0.06f;
//			}
//			color[i] -= 0.6f;
//		}
		temp.active.SetPixels (color);
		temp.active.Apply ();
		return temp;
	}
	protected buttonTexture loadToggleTexture(string texturePath){
		buttonTexture temp;
		temp.inactive = Resources.Load<Texture2D>(texturePath+"_off");
		temp.active = Resources.Load<Texture2D>(texturePath+"_on");
		return temp;
	}
	protected bool ButtonGUI(Rect rect, buttonTexture textures, string text = null){
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.normal.background = textures.inactive;
		buttonStyle.hover.background = textures.inactive;
		buttonStyle.active.background = textures.active;
		
		return GUI.Button (rect, text ?? string.Empty, buttonStyle);
	}
	protected void ButtonGUI(Rect rect, buttonTexture textures, string text, Utils.voidNoParams method){
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.normal.background = textures.inactive;
		buttonStyle.hover.background = textures.inactive;
		buttonStyle.active.background = textures.active;
		
		if (GUI.Button (rect, text ?? string.Empty, buttonStyle)){
			method();
		}
	}
	protected bool ButtonGUI(Rect rect, string text){
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.normal.background = baseButton.inactive;
		buttonStyle.hover.background = baseButton.inactive;
		buttonStyle.active.background = baseButton.active;
		
		return GUI.Button (rect, text, buttonStyle);
	}
	protected bool ToggleGUI(Rect rect, buttonTexture textures, bool active, string text = null){
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.toggle);

		buttonStyle.active.background = textures.active;
		buttonStyle.hover.background = textures.inactive;
		buttonStyle.normal.background = textures.inactive;

		buttonStyle.onActive.background = textures.inactive;
		buttonStyle.onHover.background = textures.active;
		buttonStyle.onNormal.background = textures.active;

		return GUI.Toggle (rect, active, text ?? string.Empty, buttonStyle);
	}

	//scrollView by touch
	//author: rPrwk
	protected void touchScrollView(bool isHorizontal, Vector2 viewVector){
		if (Input.touchCount > 0) {
			Touch touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
				if (isHorizontal) {
					viewVector.x -= touch.deltaPosition.x;
				} else {
					viewVector.y -= touch.deltaPosition.y;
				}
		}
	}
}
