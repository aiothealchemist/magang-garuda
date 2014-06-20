using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	//button clickability
	public enum buttonType {push, draggable, toggle};
	public buttonType tipe;
	public bool isFocused { get; set; } //is it available to click?

	//click position
	private Vector3 mouseNow;
	public Bounds kotakTujuan { get; set; }
	
	//button appearance
	public Sprite sprite;
	public Sprite[] sprites;
	private int spriteOnDisplay = 0;
	private GameObject theText;

	// Use this for initialization
	void Start () {
		isFocused = true;
		kotakTujuan = new Bounds( 
		                         new Vector3(transform.position.x, transform.position.y, 0), 
		                         transform.GetComponent<SpriteRenderer> ().bounds.size);
//		theText = new GameObject ();
//		theText.transform.parent = transform;
//		theText.AddComponent<GUIText> ();
//		theText.transform.position = transform.position;
//		theText.guiText.text = transform.name;

//		transform.GetComponent<SpriteRenderer> ().sprite
//				= (tipe == buttonType.toggle ? sprites [spriteOnDisplay] : sprite);
	}
	
	// Update is called once per frame
	void Update () {
		if (isFocused) {
			if (Input.GetButtonDown ("Fire1")) {
				mouseNow = Camera.main.ScreenToWorldPoint( Input.mousePosition );
				mouseNow.z = 0;
				if (!kotakTujuan.Contains (mouseNow)) {
					mouseNow = Vector3.zero;
				} else {
				}
			} else if (Input.GetButton("Fire1") && mouseNow != Vector3.zero && tipe == buttonType.draggable){
				mouseNow = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mouseNow.z = 0;
				transform.position = mouseNow;
			} else if (Input.GetButtonUp ("Fire1")) {
				if (kotakTujuan.Contains(mouseNow)){
					transform.parent.GetComponent<Menu>().chosen(gameObject.name);
				}
			}
		}
	}

	public void setText(string text){
//		theText.guiText.text = text;
	}

	public void toggle(bool forward){
		if (tipe == buttonType.toggle)
			transform.GetComponent<SpriteRenderer> ().sprite
					= sprites[ (forward ? ++spriteOnDisplay : --spriteOnDisplay) % sprites.Length ];
	}
}
