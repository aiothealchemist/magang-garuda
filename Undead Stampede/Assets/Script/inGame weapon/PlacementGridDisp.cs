﻿using UnityEngine;
using System.Collections;

public class PlacementGridDisp : MonoBehaviour {
	public bool isHaveContent = false;

	// Use this for initialization
	void Start () {
		renderer.enabled = false;		
	}

	public void OnMouseDrag () {
		renderer.enabled = true;	
	}
}