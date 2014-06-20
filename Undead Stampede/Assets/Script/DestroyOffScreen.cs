using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Define the state when this object collide
	void OnCollisionEnter2D()
	{
		DestroyObject(gameObject);
	}

	void OnBecameInvisible(){
		Debug.Log ("object out of screen");
		DestroyObject (gameObject);
	}
}
