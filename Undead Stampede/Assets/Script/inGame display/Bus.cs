using UnityEngine;
using System.Collections;

public class Bus : MonoBehaviour {

	public static Bus busYangAda;
	ScrollingBackground fore, parFore, parBack;
	public float speed, health;
	public float speedFactor;

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
		fore.speed = speed * 1; // TODO diganti
		parFore.speed = fore.speed * 0.2f;
		parBack.speed = fore.speed * 0.001f;
	}

	public void digaet(float speedBerkurang){
		speed -= speedBerkurang;
	}

	public void dihantam(float healthBerkurang){
		health -= healthBerkurang;
	}
}
