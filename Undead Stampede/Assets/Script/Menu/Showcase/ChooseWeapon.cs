using UnityEngine;
using System.Collections;

public class ChooseWeapon : Menu {

	int area, level;

	public override void chosen (string name)
	{
		int a = area; a = level;
		level = a;
	}

	public void setLevel(int[] input){
		area = input[0];
		level = input[1];
	}
}
