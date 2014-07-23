using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour {
	//damage animation variables
	public GameObject explosionPrefab;
	private GameObject explosion;

	void instantiateExplosion(){
		explosion = Instantiate (explosionPrefab, transform.position, transform.rotation) as GameObject;
	}

	//Define the state when this object collide
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "zombieup" || coll.gameObject.tag == "zombieback" || coll.gameObject.tag == "zombieside") {//zombie is hit by bullet
			//add bullet damage
			instantiateExplosion();
		}
	}
}
