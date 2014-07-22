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
	//variables for rotating barrel
	GameObject barrel;
	GameObject[] barrelFinder;
	//variables for sprite change to side weapon sprite
	public Sprite sidePad;
	public Sprite sideBarrel;
	//variables for sell/upgrade button
	public GameObject sellButtonPrefab;
	private GameObject sellButton;
	public bool isSellUpgradeOn = false;

	void Start(){
		//invoke searching target for turret
		findBarrel ();
		InvokeRepeating ("ScanForTarget", 0, searchFrequency);
	}

	void Update(){
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

	public void OnMouseDown() {
		if (isPlaced){
			//do nothing
		}
		else{
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		}
	}

	public void OnMouseUp(){
		//show sell/upgrade button if button is off
		if (isSellUpgradeOn) {	
					deleteAllSellUpgradeButton ();
		} else {
			showSellButton ();
			//show upgrade button
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

	void showSellButton(){
		//show sell button when weapon is tapped
		if (!isPlaced && isNotChosen) {
			//do nothing		
		} else if (isPlaced && !isNotChosen) {
			//remove all sell and upgrade button of other weapons
			deleteAllSellUpgradeButton();
			//show sell and upgrade button for this weapon
			Vector3 sellButtonPos = new Vector3(gameObject.transform.position.x + 0.5f,gameObject.transform.position.y,gameObject.transform.position.z);
			sellButton = Instantiate(sellButtonPrefab, sellButtonPos, gameObject.transform.rotation) as GameObject;
			sellButton.transform.parent = gameObject.transform;
			isSellUpgradeOn = true;
		}
	}

	public void deleteAllSellUpgradeButton(){
		GameObject[] delete = GameObject.FindGameObjectsWithTag("sellupgradebutton");
		GameObject[] weapon = GameObject.FindGameObjectsWithTag("weapon");
		foreach (GameObject d in delete){
			d.GetComponent<SellWeapon>().isDestroying = true;
		}
		foreach (GameObject w in weapon) {
			w.GetComponent<PlaceWeapon>	().isSellUpgradeOn = false;	
		}
	}

	public void SetPosition(){
		//set the turret position to snap to selected position
		gridup = GameObject.FindGameObjectsWithTag("placementgridup");
		gridback = GameObject.FindGameObjectsWithTag("placementgridback");
		gridside = GameObject.FindGameObjectsWithTag("placementgridside");

		enableAllQuadCollider ();
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
					addColliderToWeapon();
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
							s.transform.Rotate(new Vector3(0f,0f,90f));
						}
					}
					addColliderToWeapon();
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
					addColliderToWeapon();
					quad.GetComponent<PlacementGridDisp>().isHaveContent = true;
					isMustDelete = false;
				}
			}
		}
		disableAllQuadCollider ();
	}

	public void setGridContent(bool toggle){
		gridup = GameObject.FindGameObjectsWithTag("placementgridup");
		gridback = GameObject.FindGameObjectsWithTag("placementgridback");
		gridside = GameObject.FindGameObjectsWithTag("placementgridside");
		foreach (GameObject quad in gridup) {
			if	(gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
				
			}
		}
		foreach (GameObject quad in gridback) {
			if	(gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
		foreach (GameObject quad in gridside) {
			if	(gameObject.renderer.bounds.Intersects(quad.renderer.bounds)){
				quad.GetComponent<PlacementGridDisp>().isHaveContent = toggle;
			}
		}
	}

	void addColliderToWeapon(){
		gameObject.AddComponent<BoxCollider2D>();
		gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1,1);
		gameObject.GetComponent<BoxCollider2D>().center = new Vector2(0,0);
	}

	void disableAllQuadCollider(){
		foreach (GameObject quad in gridup) {
			quad.GetComponent<MeshCollider>().enabled = false;	
		}
		foreach (GameObject quad in gridback) {
			quad.GetComponent<MeshCollider>().enabled = false;	
		}
		foreach (GameObject quad in gridside) {
			if(quad.name == "Sidequad Indicator"){
				
			}else
			{
				quad.GetComponent<MeshCollider>().enabled = false;	
			}
		}
	}

	void enableAllQuadCollider(){
		foreach (GameObject quad in gridup) {
			quad.GetComponent<MeshCollider>().enabled = true;	
		}
		foreach (GameObject quad in gridback) {
			quad.GetComponent<MeshCollider>().enabled = true;	
		}
		foreach (GameObject quad in gridside) {
			if(quad.name == "Sidequad Indicator"){

			}else
			{
			   quad.GetComponent<MeshCollider>().enabled = true;	
			}
		}
	}

	void ScanForTarget(){
		target = GetNearestTargetObject ();
	}

	Transform GetNearestTargetObject(){
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

	void findBarrel(){
		barrelFinder = GameObject.FindObjectsOfType<GameObject>();
		foreach(GameObject g in barrelFinder){
			if (g.transform.IsChildOf(gameObject.transform) && g.name == "Barrel"){
				barrel = g;
			}
		}
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
}
