//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections.Generic;

public class level : MonoBehaviour
{
	Stack<List<Transform>> arrofarrofzombie;
	public Transform[] zombieprefabslevel;
		
	void Start ()
	{
		//set zombie rooster for each level
		arrofarrofzombie = new Stack<List<Transform>> ();
		fillWave (1);
	}

	public List<Transform> pop(){
		if (arrofarrofzombie.Count != 0) {
			return arrofarrofzombie.Pop ();
		} else {
			return new List<Transform>();
		}
	}

	public void fillWave(int levelNum){
		//LEVEL ROOSTER EDITOR
		switch (levelNum) {
		case 1:
			//insert all wave for level 1
			CreateWave(new int[8]{2,2,2,2,2,2,2,2});
			break;
		}
	}

	public void CreateWave(int[] rooster){
		List<Transform> tempWave = new List<Transform>();

		foreach (int r in rooster) {
			tempWave.Add(zombieprefabslevel[r]);		
		}
		arrofarrofzombie.Push (tempWave);
	}
}


