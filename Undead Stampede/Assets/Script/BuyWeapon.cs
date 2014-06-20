using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class BuyWeapon : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	public float sensitivity = 0.02f;
	public GameObject selectedWeapon;
	private GameObject draggedWeapon;

	void OnMouseDown() {
		draggedWeapon = Instantiate (selectedWeapon, transform.position, Quaternion.identity) as GameObject;
		draggedWeapon.GetComponent<PlaceWeapon> ().OnMouseDown ();
	}
	
	void OnMouseDrag()
	{
		draggedWeapon.GetComponent<PlaceWeapon> ().OnMouseDrag ();

		//show placement grid, to be multiplied
		GameObject[] PG = GameObject.FindGameObjectsWithTag("placementgrid");
		foreach (GameObject quad in PG) {
						quad.GetComponent<PlacementGridDisp> ().OnMouseDrag ();
				}
	}

	void OnMouseUp(){
		draggedWeapon.GetComponent<PlaceWeapon> ().isPlaced = true;
		draggedWeapon.GetComponent<PlaceWeapon> ().isNotChosen = false;
		draggedWeapon.GetComponent<PlaceWeapon> ().SetPosition ();

		//hide placement grid, to be multiplied
		GameObject[] PG = GameObject.FindGameObjectsWithTag("placementgrid");
		foreach (GameObject quad in PG) {
						quad.GetComponent<PlacementGridDisp> ().renderer.enabled = false;
				}
	}
}
