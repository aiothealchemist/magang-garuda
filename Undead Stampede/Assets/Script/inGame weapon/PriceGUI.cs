using UnityEngine;
using System.Collections;

public class PriceGUI : MonoBehaviour {
	private Vector3 textPos;
	public string priceText;
	private GUIStyle style = new GUIStyle();
	public Font guiFont;

	// Use this for initialization
	void Start () {
		style.font = guiFont;
		style.normal.textColor = Color.green;
		style.hover.textColor = Color.green;
		textPos = Camera.main.WorldToScreenPoint (transform.position);
	}

	void OnGUI(){
		GUI.Label(new Rect(textPos.x - 15,textPos.y + 511,200,20), priceText, style);
	}
}
