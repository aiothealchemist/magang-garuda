using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour {
	//damage animation variables
	public GameObject explosionPrefab;

	void instantiateExplosion(){
		Instantiate (explosionPrefab, transform.position, transform.rotation);
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
