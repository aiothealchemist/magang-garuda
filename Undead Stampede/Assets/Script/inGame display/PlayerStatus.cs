using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public int cash;
	public int maxHealth;
	private GUIStyle style = new GUIStyle();
	public Font guiFont;

	int height, width;

	// Use this for initialization
	protected virtual void Start () {
		style.font = guiFont;
		style.normal.textColor = Color.green;
		style.hover.textColor = Color.green;
		style.fontSize = 45;

		height = Screen.height;
		width = Screen.width;
	}

	// Update is called once per frame
	void Update () {
		if (maxHealth <= 0)
			Application.LoadLevel ("Menu Screen");
	}

	void OnGUI () {
		GUI.Label(new Rect(0.3f*width, 0.88f*height,200,20), "$ " + cash.ToString(), style);
		GUI.Label(new Rect(0.023f*width, 0.85f*height,200,20), (maxHealth < 0 ? 0 : maxHealth).ToString(), style);
	}
}
