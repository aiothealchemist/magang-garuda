using UnityEngine;
using System.Collections;

public delegate void delegatedMethod();

public static class utils {

	//harusnya masuk ke Persistent data
	public static bool sfxON = true, musicON = true;

	public static DLC[] getWeapon(){
		return new DLC[]{};
	}
	
	public static DLC[] getParts(){
		return new DLC[]{};
	}
	
	public static DLC[] getVehicles(){
		return new DLC[]{};
	}
	
	public static DLC[] getAchievements(){
		return new DLC[]{};
	}

	public static DLC[] getGems(){
		return new DLC[]{};
	}
}
