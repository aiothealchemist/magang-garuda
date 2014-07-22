using UnityEngine;
using System.Collections;

public class SellWeapon : MonoBehaviour {
	private GameObject[] gridup;
	private GameObject[] gridback;
	private GameObject[] gridside;
	private Transform selectedWeapon;

	//size animation variables
	private float sizeTimer = 0;
	private float frameRate = 1.5f;
	private bool isSet = false;
	public bool isDestroying = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!isSet)
			animateInstantiation ();

		if(isDestroying)
			animateDestruction ();
	}

	void OnMouseUp(){
		//sell weapon
		sellWeapon (selectedWeapon);
	}

	void sellWeapon(Transform _selectedWeapon){
		_selectedWeapon = gameObject.transform.parent;
		_selectedWeapon.GetComponent<PlaceWeapon>().setGridContent (false);
		DestroyObject(_selectedWeapon.gameObject);
	}



	void animateInstantiation(){
		sizeTimer += (Time.deltaTime * frameRate);
		Vector3 newScale = new Vector3 (sizeTimer, sizeTimer, 0);
		if (sizeTimer <= 0.08) {
				gameObject.transform.localScale = newScale;
		} else {
				isSet = true;
		}
	}

	void animateDestruction(){
		sizeTimer -= (Time.deltaTime * frameRate);
		Vector3 newScale = new Vector3 (sizeTimer, sizeTimer, 0);
		if (sizeTimer >= 0) {
			gameObject.transform.localScale = newScale;
		} else {
			DestroyObject(gameObject);
		}
	}

}

