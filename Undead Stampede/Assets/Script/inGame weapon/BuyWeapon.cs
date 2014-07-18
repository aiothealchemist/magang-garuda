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
		draggedWeapon.GetComponent<PlaceWeapon> ().deleteAllSellUpgradeButton ();
	}
	
	void OnMouseDrag()
	{
		draggedWeapon.GetComponent<PlaceWeapon> ().OnMouseDrag ();

		//show placement grid, to be multiplied
		GameObject[] PGUp = GameObject.FindGameObjectsWithTag("placementgridup");
		foreach (GameObject quad in PGUp) {
			quad.GetComponent<PlacementGridDisp> ().showGrid();
				}
		GameObject[] PGBack = GameObject.FindGameObjectsWithTag("placementgridback");
		foreach (GameObject quad in PGBack) {
			quad.GetComponent<PlacementGridDisp> ().showGrid();
		}
		GameObject[] PGSide = GameObject.FindGameObjectsWithTag("placementgridside");
		foreach (GameObject quad in PGSide) {
			quad.GetComponent<PlacementGridDisp> ().showGrid();
		}
	}

	void OnMouseUp(){
		draggedWeapon.GetComponent<PlaceWeapon> ().isPlaced = true;
		draggedWeapon.GetComponent<PlaceWeapon> ().isNotChosen = false;
		draggedWeapon.GetComponent<PlaceWeapon> ().SetPosition ();

		//hide placement grid, to be multiplied
		GameObject[] PGUp = GameObject.FindGameObjectsWithTag("placementgridup");
		foreach (GameObject quad in PGUp) {
						quad.GetComponent<PlacementGridDisp> ().renderer.enabled = false;
				}
		GameObject[] PGBack = GameObject.FindGameObjectsWithTag("placementgridback");
		foreach (GameObject quad in PGBack) {
			quad.GetComponent<PlacementGridDisp> ().renderer.enabled = false;
		}
		GameObject[] PGSide = GameObject.FindGameObjectsWithTag("placementgridside");
		foreach (GameObject quad in PGSide) {
			quad.GetComponent<PlacementGridDisp> ().renderer.enabled = false;
		}
	}
}
