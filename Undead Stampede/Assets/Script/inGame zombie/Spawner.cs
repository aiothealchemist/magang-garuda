using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private System.Collections.Generic.List<Transform> zombiePrefabs;
	public float xMax = -6;
	public int minDelayTime = 300;
	public int maxDelayTime = 1500;
	public Stack waves;

	private int countdown;
	level roosterHolder;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Ticks;
		zombiePrefabs = new System.Collections.Generic.List<Transform> ();
		roosterHolder = GameObject.FindObjectOfType<level>();
		startTick ();
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown > 0) {
			--countdown;
		} else {
			spawnZombie ();
			startTick ();
		}

		//check the remaining zombie in the field
		if (zombiePrefabs.Count == 0) {
			GameObject[] enemyCheckerUp = GameObject.FindGameObjectsWithTag("zombieup");
			GameObject[] enemyCheckerBack = GameObject.FindGameObjectsWithTag("zombieback");
			GameObject[] enemyCheckerSide = GameObject.FindGameObjectsWithTag("zombieside");
			if (enemyCheckerUp.Length == 0 && enemyCheckerBack.Length == 0 && enemyCheckerSide.Length == 0) {
				//win signal
				//Debug.Log("no zombie");
				zombiePrefabs = roosterHolder.pop();
			}
		}
	}

	void startTick () {
		countdown = Random.Range(minDelayTime,maxDelayTime);
	}

	void spawnZombie(){
		Vector3 worldZero = Camera.main.ScreenToWorldPoint (new Vector3 (0, Screen.height, 10));
		if (zombiePrefabs.Count > 0) {
			int i = Random.Range (0,zombiePrefabs.Count);
			Transform zombieInstance = (Transform) Instantiate (zombiePrefabs [i], worldZero, Quaternion.identity);
			zombieInstance.Translate (Random.Range (0, 5), 0.5f, 0);
			zombieInstance.Translate(xMax, 0, 0);

			//ini berisi posisi zombie yg benar, depend on sisi tertentu
			if (zombieInstance.tag == "zombieback") {
				zombieInstance.Translate(-worldZero.y, -worldZero.y * 1.3f, 0);
			} else if (zombieInstance.tag  == "zombieside") {
				zombieInstance.Translate(0, -worldZero.y * 2.2f, 0);
			}

			zombiePrefabs.RemoveAt (i);
		} else {
		}
	}
}
