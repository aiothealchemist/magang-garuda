using UnityEngine;
using System.Collections;

public class PlaceWeapon : MonoBehaviour {
	private Vector3 screenPoint;
	private Vector3 offset;
	public bool isPlaced = false;
	public bool isNotChosen = true;
	public GameObject ammo;
	private GameObject bullet;
	private Vector3 target_pos;
	private Vector3 object_pos;
	private float angle;
	private float bulletSpeed = 10;
	private float lastFireTime = 0;	
	private float burstDelay = 0.5f;
	private GameObject[] grid;
	private GameObject[] quads;
	private bool isMustDelete = true;

	public void Start(){

	}

	public void OnMouseDown() {
		if (isPlaced){
			//do nothing
		}
		else{
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}
	
	public void OnMouseDrag()
	{
		if (isPlaced){
			//do nothing
		}
		else{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
		}
	}

	//set the turret position to snap to selected position
	public void SetPosition(){
		grid = GameObject.FindGameObjectsWithTag("placementgrid");

		foreach (GameObject quad in grid) {
			if	(!gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				//object not intersect, delete turret
			}
			else{
				//snap turret position to the selected one

				//check if grid already has content
				if (quad.GetComponent<PlacementGridDisp>().isHaveContent){
					//grid has content, delete turret on Update
					Debug.Log("this grid is full");
				}
				else{
					//grid has no content, snap weapon on Update
					gameObject.transform.position = quad.transform.position;
					quad.GetComponent<PlacementGridDisp>().isHaveContent = true;
					isMustDelete = false;
				}
			}
		}
	}

	public void Update(){
		//shoot a bullet while placed
		if (!isPlaced && isNotChosen) {
			//do nothing		
			} else if (isPlaced && !isNotChosen) {
				if(isMustDelete){
					DestroyObject(gameObject);
				}
				else {
					// Point the cannon at the zombie.
					target_pos.z = 0.0f; 
					object_pos = gameObject.transform.position;
					try{
					//for apa
						//cari paling deket
					//endfor
						target_pos.x = GameObject.FindGameObjectWithTag("zombie").transform.position.x - object_pos.x;
						target_pos.y = GameObject.FindGameObjectWithTag("zombie").transform.position.y - object_pos.y;
						angle = Mathf.Atan2(target_pos.y, target_pos.x) * Mathf.Rad2Deg - 180;
						Vector3 rotationVector = new Vector3 (0, 0, angle);
						transform.rotation = Quaternion.Euler(rotationVector);
						
						// Fire a bullet.	
						if(Time.time > lastFireTime + burstDelay){
							bullet = Instantiate(ammo, transform.position, transform.rotation) as GameObject;
							bullet.rigidbody2D.AddForce(bullet.transform.right * bulletSpeed * -1);
							lastFireTime = Time.time;
						}
						}catch{
							Debug.Log("zombie not found");
						}
				}	
		}
	}
}
