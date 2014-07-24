using UnityEngine;
using System.Collections;

public class UpgradeWeapon : MonoBehaviour {
	private Transform selectedWeapon;
	public int price;
	
	//size animation variables
	private float sizeTimer = 0;
	private float frameRate = 1.5f;
	private bool isSet = false;
	public bool isDestroying = false;
	
	// Use this for initialization
	void Start () {
		price = transform.parent.GetComponent<PlaceWeapon> ().price / 2;
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
		upgradeWeapon (selectedWeapon);
	}
	
	void upgradeWeapon(Transform _selectedWeapon){
		if (isAffordable ()) {
			_selectedWeapon = transform.parent;
			_selectedWeapon.GetComponent<PlaceWeapon> ().damage += 3;
			substractCash (price);
			Debug.Log ("current weapon damage: " + _selectedWeapon.GetComponent<PlaceWeapon> ().damage.ToString ());
		}
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
	
	void substractCash(int amount){
		GameObject.Find ("PlayerStatus").GetComponent<PlayerStatus> ().cash -= amount;
	}

	bool isAffordable(){
		return !(GameObject.Find ("PlayerStatus").GetComponent<PlayerStatus> ().cash - price < 0);
	}
}
