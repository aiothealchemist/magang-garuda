using UnityEngine;
using System.Collections;

public class PriceGUI : MonoBehaviour {
	private Vector3 textPos;
	public string priceText;

	// Use this for initialization
	void Start () {
		textPos = Camera.main.WorldToScreenPoint (transform.position);
	}

	void OnGUI(){
		GUI.Label(new Rect(textPos.x - 15,textPos.y + 505,200,20), priceText);
	}
}
