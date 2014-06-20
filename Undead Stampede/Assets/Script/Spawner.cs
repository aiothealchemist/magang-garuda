using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public System.Collections.Generic.List<Transform> zombiePrefabs;
	public float koreksiPosisiAwal;
	public float xMax = -6;
	public int maxDelayTime = 60;

	private int countdown;

	// Use this for initialization
	void Start () {
		koreksiPosisiAwal = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y;
		Random.seed = (int)System.DateTime.Now.Ticks;
		startTick ();
	}
	
	// Update is called once per frame
	void Update () {
		if (zombiePrefabs.Count == 0) {
			//win signal
		}

		if (countdown > 0) {
			--countdown;
		} else {
			spawnZombie ();
			startTick ();
		}
	}

	void startTick () {
		countdown = Random.Range(0,maxDelayTime);
	}

	void spawnZombie(){
		if (zombiePrefabs.Count > 0) {
			int i = Random.Range (0,zombiePrefabs.Count);
			Transform zombieInstance = (Transform)Instantiate (zombiePrefabs [i], new Vector3( Random.value * -5 , 0, 0 ), Quaternion.identity);

			//ini berisi posisi zombie yg benar, depend on sisi tertentu
			//		if (zombieInstance.GetComponent<ZombieController>().sisi == ZombieController.zombieSide.belakang) {
			//			zombieInstance.Translate(-koreksiPosisiAwal, koreksiPosisiAwal/2, 0);
			//		} else if (zombieInstance.GetComponent<ZombieController>().sisi == ZombieController.zombieSide.samping) {
			//			zombieInstance.Translate(0, koreksiPosisiAwal, 0);
			//		}

			//zombieInstance.Translate(  xMax , 0, 0 );
			zombiePrefabs.RemoveAt (i);
		} else {
				
		}
	}
}
