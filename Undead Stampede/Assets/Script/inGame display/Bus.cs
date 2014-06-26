using UnityEngine;
using System.Collections;

public class Bus : MonoBehaviour {

	public static Bus busYangAda;
	ScrollingBackground fore, parFore, parBack;
	public float speed, health;
	float speedFactor;
	public int coinsCollected;

	// Use this for initialization
	void Start () {
		Transform bgCamera = GameObject.Find ("BG Camera").transform;
		fore = bgCamera.FindChild ("Foreground Quad").GetComponent<ScrollingBackground>();
		parFore = bgCamera.FindChild ("Paralaks foreground Quad").GetComponent<ScrollingBackground>();
		parBack = bgCamera.FindChild ("Paralaks background Quad").GetComponent<ScrollingBackground>();

		busYangAda = this;
	}
	
	// Update is called once per frame
	void Update () {
		// TODO update busspeed
		speedFactor = speed * 0.1f;
		updateParallax ();
	}

	void updateParallax (){
		fore.speed = speedFactor * .5f; // TODO diganti
		parFore.speed = fore.speed * 0.5f;
		parBack.speed = fore.speed * 0.25f;
	}

	public void digaet(float speedBerkurang){
		speed -= speedBerkurang;
	}

	public void dihantam(float healthBerkurang){
		health -= healthBerkurang;
	}
}
