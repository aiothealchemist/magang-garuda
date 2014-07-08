using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public class ZombieController : MonoBehaviour {
	public float moveSpeed = 5.0f;
	private Vector3 moveDirection;
	public float turnSpeed;
	private Vector3 target_pos;
	private Vector3 object_pos;
	private float attackFreq = 3.0f;

	//bikinan cahyo
	public ZombieXML attributes { get; set; }
	public int health { get; set; }
	public int strength { get; set; }
	//END OF bikinan cahyo

	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
	}

	void setAttribute(ZombieXML xml){
		attributes = xml;
	}
	
	// Update is called once per frame
	void Update () {
		//check zombie's health point
		if (health < 0) {
			//destroy zombie
			DestroyObject(gameObject);
		}
		MoveTowardVehicle ();
	}

	void MoveTowardVehicle(){
		target_pos.z = 0.0f; 
		object_pos = transform.position;
		target_pos.x = GameObject.Find("vehicle").transform.position.x - object_pos.x;
		target_pos.y = GameObject.Find("vehicle").transform.position.y - object_pos.y;
		Vector3 moveToward = new Vector3(target_pos.x, target_pos.y, target_pos.z);
		moveDirection = moveToward - object_pos;
		moveDirection.z = 0; 
		moveDirection.Normalize();
		Vector3 target = moveDirection * moveSpeed + object_pos;
		transform.position = Vector3.Lerp( object_pos, target, Time.deltaTime );
		
		//rotate zombie
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp( transform.rotation, 
			                 Quaternion.Euler( 0, 0, targetAngle ), 
			                 turnSpeed * Time.deltaTime );
	}

	void Attack(){
		gameObject.GetComponent<ZombieAnimator>().state = "attacking";
	}

	//Define the state when this object collide
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "vehicle") {
						moveSpeed = 0;
						InvokeRepeating ("Attack", 0, attackFreq);
				} 
		else if (coll.gameObject.tag == "bullet") {//zombie is hit by bullet
			//add bullet damage
			health -= 3;
		}
	}
}