using UnityEngine;
using System.Collections;

public class DestroyZombieOffscreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnBecameInvisible(){
		Debug.Log ("zombie out of screen");
		DestroyObject (gameObject);
	}
}
