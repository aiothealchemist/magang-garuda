using UnityEngine;
using System.Collections;

public class SellWeapon : MonoBehaviour {
	private GameObject[] gridup;
	private GameObject[] gridback;
	private GameObject[] gridside;
	private Transform selectedWeapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){
		//sell weapon
		sellWeapon (selectedWeapon);
	}

	void sellWeapon(Transform _selectedWeapon){
		_selectedWeapon = gameObject.transform.parent;
		setGridContent (_selectedWeapon, false);
		DestroyObject (gameObject);
		DestroyObject (_selectedWeapon.gameObject);
		
	}

	void setGridContent(Transform _selectedWeapon, bool toggle){
		_selectedWeapon = gameObject.transform.parent;
		gridup = GameObject.FindGameObjectsWithTag("placementgridup");
		gridback = GameObject.FindGameObjectsWithTag("placementgridback");
		gridside = GameObject.FindGameObjectsWithTag("placementgridside");
		foreach (GameObject quad in gridup) {
			if	(_selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;

			}
		}
		foreach (GameObject quad in gridback) {
			if	(_selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
		foreach (GameObject quad in gridside) {
			if	(_selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
	}
}
