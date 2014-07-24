using UnityEngine;
using System.Collections;

public class ProjectileEffect : MonoBehaviour {
	//damage animation variables
	public GameObject explosionPrefab;
	public int healthDmg;
	private GameObject explosion;

	void Start(){
		healthDmg = gameObject.transform.parent.GetComponent<PlaceWeapon> ().damage;
	}

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
