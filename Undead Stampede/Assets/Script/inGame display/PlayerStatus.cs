using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	int height, width;

	// Use this for initialization
	protected virtual void Start () {
		height = Screen.height;
		width = Screen.width;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI () {
		GUI.Box (new Rect (0, 0, width / 2, height / 10), "Bus Health : "+Bus.busYangAda.health);
		GUI.Box (new Rect (width / 2, 0, width / 2, height / 10), "Coins Collected : "+Bus.busYangAda.coinsCollected);
	}
}
