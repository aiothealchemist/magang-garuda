//this is a bloated code with a lot of 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public delegate void voidWithZeroParam();
public delegate void voidWithTwoParams_String_String(string a, string b);

public static class utils {

	//path inside "/Assets/"
	public static IEnumerator loadImageByPath(string internalPath, Vector2 size, List<Texture2D> array){
		WWW www = new WWW (Application.dataPath + internalPath);
		yield return www;
		Texture2D image = new Texture2D ((int)size.x, (int)size.y);
		//LoadImageIntoTexture compresses JPGs by DXT1 and PNGs by DXT5 (assume all images are png)
		www.LoadImageIntoTexture (image);
	}

	public enum loadedContentType { achievement, gem, part, powerup, vehicle, weapon, zombie }
	static TinyXmlReader xmlReader;
	static string fileXML = "*";

	public static LoadableContent[] loadSpecificXML (loadedContentType tag){
		List<string> stringTags = new List<string>(getXMLTag (tag));
		string typenameTag = stringTags [0]; stringTags.RemoveAt (0);
		List<LoadableContent> contents = new List<LoadableContent> ();
		xmlReader = new TinyXmlReader (fileXML, true);
		while (xmlReader.Read ()) {
			if (xmlReader.isOpeningTag && xmlReader.tagName == typenameTag){
				LoadableContent temp = constructContent(tag);
				while(xmlReader.Read(typenameTag)){ // read as long as not encountering the closing tag
					if (xmlReader.isOpeningTag && stringTags.Contains(xmlReader.tagName)) {
						var propertyCalled = temp.GetType().GetProperty(xmlReader.tagName);
						if (propertyCalled == null) {}
						else if (propertyCalled.PropertyType == typeof(string)) {
							propertyCalled.SetValue (temp, xmlReader.content, null);
						} else if (propertyCalled.PropertyType == typeof(int)) {
							propertyCalled.SetValue(temp, int.Parse (xmlReader.content), null);
						} else if (propertyCalled.PropertyType == typeof(bool)) {
							propertyCalled.SetValue(temp, bool.Parse (xmlReader.content), null);
						} else if (propertyCalled.PropertyType is IDictionary) {
							string dictionaryTag = propertyCalled.Name;
							voidWithTwoParams_String_String dictMethod = 
									((dictionaryTag == "pricing") ? 
								 new voidWithTwoParams_String_String(temp.setPrice) : 
								 	(dictionaryTag == "sprites") ? 
								 new voidWithTwoParams_String_String(temp.setSprite) : null );
							while (xmlReader.Read (dictionaryTag) && dictMethod != null) {
								if (xmlReader.isOpeningTag){
									dictMethod(xmlReader.tagName, xmlReader.content);
								}
							}
						}
					}
				}
				contents.Add (temp);
			}
		}
		return contents.ToArray();
	}

	static string[] getXMLTag(loadedContentType tipe){
		List<string> tag = new List<string> ();
		tag.Add (tipe.ToString ());
		LoadableContent temp = constructContent (tipe);
		foreach (var item in temp.GetType ().GetProperties ()) {
			tag.Add (item.Name);
		}
		return tag.ToArray ();
	}

	static LoadableContent constructContent(loadedContentType tipe){
		LoadableContent tag = null;
		switch (tipe) {
		case loadedContentType.achievement:
			tag = new AchievementXML();
			break;
		case loadedContentType.gem:
			tag = new GemXML();
			break;
		case loadedContentType.part:
			tag = new PartXML();
			break;
		case loadedContentType.powerup:
			tag = new PowerupXML();
			break;
		case loadedContentType.vehicle:
			tag = new VehicleXML();
			break;
		case loadedContentType.weapon:
			tag = new WeaponXML();
			break;
		case loadedContentType.zombie:
			tag = new ZombieXML();
			break;
		}
		return tag;
	}

	//player preferences methods
	//gems --> key = "PlayerGems"
	public static void setGems(int gems){
		PlayerPrefs.SetInt ("PlayerGems", gems);
	}
	public static int getGems(){
		return PlayerPrefs.GetInt("PlayerGems");
	}
	public static void addGems(int gems){
		PlayerPrefs.SetInt ("PlayerGems", getGems () + gems);
	}
	public static void substractGems(int gems){
		PlayerPrefs.SetInt ("PlayerGems", getGems () - gems);
	}

	//powerups
	//extracoins key = "ExtraCoins"
	//extrahealth key = "ExtraHealth"
	//extraspeed key = "ExtraSpeed"
	public static void setExtraCoin(int coin){
		PlayerPrefs.SetInt ("ExtraCoins", coin);
	}
	public static void setExtraHealth(int health){
		PlayerPrefs.SetInt ("ExtraHealth", health);
	}
	public static void setExtraSpeed(int speed){
		PlayerPrefs.SetInt ("ExtraSpeed", speed);
	}
	public static int getExtraCoin(){
		return PlayerPrefs.GetInt("ExtraCoins");
	}
	public static int getExtraHealth(){
		return PlayerPrefs.GetInt("ExtraHealth");
	}
	public static int getExtraSpeed(){
		return PlayerPrefs.GetInt("ExtraSpeed");
	}

	//unlocked weapons
	//contains booleans referring whether or not the weapon has been unlocked, setter, getter
	// index 0, key "UMachineGun" =  Machine Gun
	// index 1, key "UGrenadeLauncher" =  Grenade Launcher
	// index 2, key "UCryoGun" =  Cryo Gun
	public static List<bool> getAllUnlockedWeapon(){
		List<bool> res = new List<bool>();

		res.Add (PlayerPrefs.GetBool("UMachineGun"));
		res.Add (PlayerPrefs.GetBool("UGrenadeLauncher"));
		res.Add (PlayerPrefs.GetBool("UCryoGun"));

		return res;
	}
	public static void buyWeapon(string weaponname){
		if (PlayerPrefs.HasKey (weaponname)) {
						PlayerPrefs.SetBool (weaponname, true);
				} else {
					//key not found
					Debug.Log("key: " + weaponname + "not found");
				}
	}

	//equipped weapon, index arrangement equals to the unlocked weapon index arrangement
	// index 0, key "EMachineGun" =  Machine Gun
	// index 1, key "EGrenadeLauncher" =  Grenade Launcher
	// index 2, key "ECryoGun" =  Cryo Gun
	public static List<bool> getAllEquippedWeapon(){
		List<bool> res = new List<bool>();
		
		res.Add (PlayerPrefs.GetBool("EMachineGun"));
		res.Add (PlayerPrefs.GetBool("EGrenadeLauncher"));
		res.Add (PlayerPrefs.GetBool("ECryoGun"));

		return res;
	}
	public static void equipWeapon(string weaponname){
		if (PlayerPrefs.HasKey (weaponname)) {
			PlayerPrefs.SetBool (weaponname, true);
		} else {
			//key not found
			Debug.Log("key: " + weaponname + "not found");
		}
	}
	public static void unequipWeapon(string weaponname){
		if (PlayerPrefs.HasKey (weaponname)) {
			PlayerPrefs.SetBool (weaponname, false);
		} else {
			//key not found
			Debug.Log("key: " + weaponname + "not found");
		}
	}

	//vehicle preference
	// index 0, key "Bus" = Default Bus, bought by default
	// index 1, key "Truck" = The Truck
	// index 2, key "RV" = The RV
	public static List<bool> getAllUnlockedVehicle(){
		List<bool> res = new List<bool>();

		res.Add(PlayerPrefs.GetBool("Bus"));
		res.Add(PlayerPrefs.GetBool("Truck"));
		res.Add(PlayerPrefs.GetBool("RV"));

		return res;
	}
	public static void buyVehicle(string vehiclename){
		if (PlayerPrefs.HasKey (vehiclename)) {
			PlayerPrefs.SetBool (vehiclename, true);
		} else {
			//key not found
			Debug.Log("key: " + vehiclename + "not found");
		}
	}
	public static void equipVehicle(string vehiclename){
		if (PlayerPrefs.HasKey (vehiclename)) {
			PlayerPrefs.SetBool (vehiclename, true);
		} else {
			//key not found
			Debug.Log("key: " + vehiclename + "not found");
		}
	}

	//level preference
	// index 0, key "level01" = level 1
	// index 1, key "level02" = level 2
	// index 3, key "level03" = level 3
	public static List<bool> getAllUnlockedLevel(){
		List<bool> res = new List<bool> ();

		res.Add(PlayerPrefs.GetBool("level01"));
		res.Add(PlayerPrefs.GetBool("level02"));
		res.Add(PlayerPrefs.GetBool("level03"));
		
		return res;
	}
	public static void unlockLevel(string levelname){
		if (PlayerPrefs.HasKey (levelname)) {
			PlayerPrefs.SetBool (levelname, true);
		} else {
			//key not found
			Debug.Log("key: " + levelname + "not found");
		}
	}

	//achievement preference
	// index 0, key "achv01" = Finished Tutorial
	public static List<bool> getAllUnlockedAchievement(){
		List<bool> res = new List<bool> ();

		res.Add(PlayerPrefs.GetBool("achv01"));

		return res;
	}
	public static void unlockAchievement(string achievementname){
		if (PlayerPrefs.HasKey (achievementname)) {
			PlayerPrefs.SetBool (achievementname, true);
		} else {
			//key not found
			Debug.Log("key: " + achievementname + "not found");
		}
	}

	//BGM and SFX toggle preference
	// key "BGMSetting" = toggle settings for BGM
	// key "SFXSetting" = toggle settings for SFX
	public static bool persistenceSFX {
		get {
			return PlayerPrefs.GetBool("SFXSetting");
		}
		set {
			PlayerPrefs.SetBool("SFXSetting", value);
		}
	}
	public static bool persistenceBGM {
		get {
			return PlayerPrefs.GetBool("BGMSetting");
		}
		set {
			PlayerPrefs.SetBool("BGMSetting", value);
		}
	}

	//INSERT OTHER PREFERENCES HERE

	public static void setInitialPersistent(){
		PlayerPrefs.DeleteAll ();
		setGems (100);
		buyWeapon ("UMachineGun");
		buyWeapon("UGrenadeLauncher");
		buyWeapon ("UCryoGun");
		equipWeapon ("EMachineGun");
		equipWeapon("EGrenadeLauncher");
		equipWeapon ("ECryoGun");
		buyVehicle ("Bus");
		equipVehicle ("Bus");
		unlockLevel ("level01");
		unlockLevel ("level02");
		unlockLevel ("level03");
		persistenceBGM = true;
		persistenceSFX = true;
	}
}
