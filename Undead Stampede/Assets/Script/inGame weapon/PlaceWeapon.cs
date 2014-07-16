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
	private GameObject[] gridup;
	private GameObject[] gridback;
	private GameObject[] gridside;
	private bool isMustDelete = true;
	private string searchTag;
	private float searchFrequency = 1.0f;
	private Transform target;
	GameObject barrel;
	GameObject[] barrelFinder;
	public Sprite sidePad;
	public Sprite sideBarrel;

	public void Start(){
		//invoke searching target for turret
		findBarrel ();
		InvokeRepeating ("ScanForTarget", 0, searchFrequency);
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
		gridup = GameObject.FindGameObjectsWithTag("placementgridup");
		gridback = GameObject.FindGameObjectsWithTag("placementgridback");
		gridside = GameObject.FindGameObjectsWithTag("placementgridside");

		foreach (GameObject quad in gridup) {
			if	(!gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				//object not intersect, delete turret
			}
			else{
				//snap turret position to the selected one

				//check if grid already has content
				if (quad.GetComponent<PlacementGridDisp>().isHaveContent){
					//grid has content, delete turret on Update
				}
				else{
					//grid has no content, snap weapon on Update
					gameObject.transform.position = quad.transform.position;
					searchTag = "zombieup";
					quad.GetComponent<PlacementGridDisp>().isHaveContent = true;
					isMustDelete = false;
				}
			}
		}
		foreach (GameObject quad in gridback) {
			if	(!gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				//object not intersect, delete turret
			}
			else{
				//snap turret position to the selected one
				
				//check if grid already has content
				if (quad.GetComponent<PlacementGridDisp>().isHaveContent){
					//grid has content, delete turret on Update
				}
				else{
					//grid has no content, snap weapon on Update
					gameObject.transform.position = quad.transform.position;
					searchTag = "zombieback";
					//rotate pad
					SpriteRenderer[] renderers =  gameObject.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in renderers){
						if(s.name == "Pad"){
							Debug.Log("rotating pad");
							s.transform.Rotate(new Vector3(0f,0f,90f));
						}
					}
					quad.GetComponent<PlacementGridDisp>().isHaveContent = true;
					isMustDelete = false;
				}
			}
		}
		foreach (GameObject quad in gridside) {
			if	(!gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				//object not intersect, delete turret
			}
			else{
				//snap turret position to the selected one
				
				//check if grid already has content
				if (quad.GetComponent<PlacementGridDisp>().isHaveContent){
					//grid has content, delete turret on Update
				}
				else{
					//grid has no content, snap weapon on Update
					gameObject.transform.position = quad.transform.position;
					searchTag = "zombieside";
					//rotate pad
					SpriteRenderer[] renderers =  gameObject.GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in renderers){
						if(s.name == "Pad"){
							s.sprite = sidePad;
							s.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
						}
						else if(s.name == "Barrel"){
							s.sprite = sideBarrel;
							s.transform.localScale = new Vector3(0.07f, 0.07f, 1f);
							s.transform.position = new Vector3(s.transform.position.x,s.transform.position.y + 0.3f, s.transform.position.z);
							Vector3 newAngle = new Vector3(0,0,60);
							s.transform.rotation = Quaternion.Euler(newAngle);
							s.sortingOrder = 1;
						}
					}
					quad.GetComponent<PlacementGridDisp>().isHaveContent = true;
					isMustDelete = false;
				}
			}
		}
	}

	public void ScanForTarget(){
		target = GetNearestTargetObject ();
	}

	public void findBarrel(){
		barrelFinder = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject g in barrelFinder){
			if (g.transform.IsChildOf(gameObject.transform) && g.name == "Barrel"){
				barrel = g;
			}
		}
	}

	public Transform GetNearestTargetObject(){
		//find the nearest target with certain tag
		float nearestDistanceSqr = Mathf.Infinity;
		GameObject[] taggedGameObject = GameObject.FindGameObjectsWithTag (searchTag);
		Transform nearestObject = null;

		// loop through each tagged object, remembering nearest one found
		foreach (GameObject target in taggedGameObject) {
			Vector3 objectPos = target.transform.position;	
			float distanceSqr = (objectPos - transform.position).sqrMagnitude;

			if(distanceSqr < nearestDistanceSqr){
				//set new target
				nearestObject = target.transform;
				nearestDistanceSqr = distanceSqr;
			}
		}

		return nearestObject;
	}

	void shootTarget(){
		// Point the cannon at the zombie that already liste on the target variable.
		target_pos.z = 0.0f; 
		object_pos = gameObject.transform.position;
		if(target != null){
			try{
				target_pos.x = target.transform.position.x - object_pos.x;
				target_pos.y = target.transform.position.y - object_pos.y;
				angle = Mathf.Atan2(target_pos.y, target_pos.x) * Mathf.Rad2Deg - 180;
				Vector3 rotationVector = new Vector3 (0, 0, angle);
				barrel.transform.rotation = Quaternion.Euler(rotationVector);
				
				// Fire a bullet.	
				if(Time.time > lastFireTime + burstDelay){
					bullet = Instantiate(ammo, barrel.transform.position, barrel.transform.rotation) as GameObject;
					bullet.rigidbody2D.AddForce(bullet.transform.right * bulletSpeed * -1);
					lastFireTime = Time.time;
				}
			}catch{
				//no zombie found
			}
		}
		else{
			//do nothing
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
				else{
					shootTarget();
				}
		}
	}
}
