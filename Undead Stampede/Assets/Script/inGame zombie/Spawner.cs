﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private System.Collections.Generic.List<Transform> zombiePrefabs;
	public float koreksiPosisiAwal;
	public float xMax = -6;
	public int minDelayTime = 0;
	public int maxDelayTime = 60;
	public Stack waves;

	private int countdown;

	// Use this for initialization
	void Start () {
		koreksiPosisiAwal = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)).y;
		Random.seed = (int)System.DateTime.Now.Ticks;
		zombiePrefabs = new System.Collections.Generic.List<Transform> ();
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
				zombiePrefabs = GameObject.FindObjectOfType<level>().pop();
			}
		}
	}

	void startTick () {
		countdown = Random.Range(minDelayTime,maxDelayTime);
	}

	void spawnZombie(){
		if (zombiePrefabs.Count > 0) {
			int i = Random.Range (0,zombiePrefabs.Count);
			//Transform zombieInstance = (Transform)
				Instantiate (zombiePrefabs [i], new Vector3( Random.value * -9 , 5, 0 ), Quaternion.identity);

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
