using UnityEngine;
using System.Collections;

public abstract class BaseMenu : MonoBehaviour {

	//menu type
	protected enum type {window, menu};
	protected type menuType;

	protected int height, width;
	public Texture menuBG;

	public AudioSource bgm, sfx;

	protected BaseMenu window, parent;
	protected PromptGUI popup;

	// Use this for initialization
	protected virtual void Start () {
		height = Screen.height;
		width = Screen.width;
	}

	void OnGUI () {
		GUI.depth = (int) menuType;
		if ((parent ?? this).popup == null)
			updateGUI ();
	}

	void OnDestroy () {
		Destroy (window);
		Destroy (popup);
	}

	protected abstract void updateGUI ();

	protected void createPrompt (Utils.delegateVoidWithZeroParam[] method, string[] dialog) {
		if (menuType == type.window) {
			parent.createPrompt (method, dialog);
		} else {
			popup = gameObject.AddComponent<PromptGUI> ();
			popup.setVar (dialog, method);
		}
	}

	protected void setWindow(BaseMenu newSC){
		Destroy (this.window);
		this.window = newSC;
		newSC.parent = this;
	}
}
