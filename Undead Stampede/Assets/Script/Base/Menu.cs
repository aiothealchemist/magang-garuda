using UnityEngine;
using System.Collections;

public abstract class Menu : MonoBehaviour {

	//appearance (background & music)
	public Vector3 position;
	public Sprite background;
	public AudioClip audioClip;
	public AudioSource audioSource { get; private set; }

	//hubungan antarmenu & termasuk menu aktif/tidak
	protected Menu showcaseMenu, parentMenu;
	protected bool isFocused;

	// Use this for initialization
	protected virtual void Start () {
		position = transform.position;
		background = transform.FindChild ("background").GetComponent<SpriteRenderer> ().sprite;
		if (audioClip != null && audioClip.isReadyToPlay && utils.musicON) {
			audioSource = new AudioSource();
			audioSource.clip = audioClip;
			audioSource.loop = true;
			audioSource.Play();
		}
	}

	// Update is called once per frame
	protected virtual void Update () {}
	
	// After button clicked
	public abstract void chosen (string name);

	// Toggle focus, de/activate buttons, should be used only by parentMenu
	public void toggleFocus(bool focused){
		if (parentMenu != null) {
			parentMenu.toggleFocus (focused);
		} else {
			System.Collections.Generic.List<Button> buttonsAffected 
					= new System.Collections.Generic.List<Button>();
			buttonsAffected.AddRange(transform.GetComponentsInChildren<Button> ());
			buttonsAffected.AddRange(showcaseMenu != null 
					? showcaseMenu.transform.GetComponentsInChildren<Button> () 
					: new Button[0]);
			buttonsAffected.ForEach (delegate(Button button) {
				button.isFocused = focused;
			});
		}
	}

	// Instantly instantiate showcase
	protected void instantShowcase(Transform prefab){
		if (showcaseMenu != null) {
			Destroy(showcaseMenu);
		}
		showcaseMenu = ((Transform)Instantiate(prefab, Vector3.zero, Quaternion.identity)).GetComponent<Menu>();
		showcaseMenu.parentMenu = this;
	}

	/// <summary>
	/// Instantly instantiate promptDialog.
	/// </summary>
	/// <param name="prefab">Prefab.</param>
	/// <param name="dialog">Dialog. Array of string[3] {prompt question, yes/confirm, no/cancel}</param>
	/// <param name="method">Method. Array of utils.delegatedMethod[2] {confirmedMethod, cancelledMethod} ()</param>
	protected void instantPrompt(Transform prefab, string[] dialog, utils.delegatedMethod[] method){
		PromptScript promptScript = 
			((Transform) Instantiate(prefab, utils.promptPos, Quaternion.identity)).GetComponent<PromptScript>();
		promptScript.setVar (this, dialog, method);
		toggleFocus (false);
	}
}
