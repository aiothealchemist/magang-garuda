using UnityEngine;
using System.Collections;

public abstract class BasemenuGUI : MonoBehaviour {

	//menu type
	protected enum type {menu, showcase};
	protected type menuType;

	protected int height, width;
	public Texture menuBG;
	protected BasemenuGUI showcase, parent;
	protected PromptGUI prompt;

	// Use this for initialization
	protected virtual void Start () {
		height = Screen.height;
		width = Screen.width;
	}

	void OnGUI () {
		if (parent == null ? prompt == null : parent.prompt == null)
			updateGUI ();
	}

	void OnDestroy () {
		Destroy (showcase);
		Destroy (prompt);
	}

	protected abstract void updateGUI ();

	protected void createPrompt (delegatedMethod[] method, string[] dialog) {
		if (menuType == type.showcase) {
			parent.createPrompt (method, dialog);
		} else {
			prompt = gameObject.AddComponent<PromptGUI> ();
			prompt.setVar (dialog, method);
		}
	}

	protected void setShowcase(BasemenuGUI newSC){
		Destroy (this.showcase);
		this.showcase = newSC;
		newSC.parent = this;
	}
}
