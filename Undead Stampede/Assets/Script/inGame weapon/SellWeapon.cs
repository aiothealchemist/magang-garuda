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
		sellWeapon ();
	}

	void sellWeapon(){
		Transform selectedWeapon = gameObject.transform.parent;
		setGridContent (false);
		DestroyObject (gameObject);
		DestroyObject (selectedWeapon.gameObject);
		
	}

	void setGridContent(bool toggle){
		Transform selectedWeapon = gameObject.transform.parent;
		gridup = GameObject.FindGameObjectsWithTag("placementgridup");
		gridback = GameObject.FindGameObjectsWithTag("placementgridback");
		gridside = GameObject.FindGameObjectsWithTag("placementgridside");
		foreach (GameObject quad in gridup) {
			if	(selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;

			}
		}
		foreach (GameObject quad in gridback) {
			if	(selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
		foreach (GameObject quad in gridside) {
			if	(selectedWeapon.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
	}
}
