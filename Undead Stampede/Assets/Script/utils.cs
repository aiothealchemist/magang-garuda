//this is a bloated code with a lot of 

using UnityEngine;
using System.Collections;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

public delegate void voidWithZeroParam();
public delegate void voidWithTwoParams_String_String(string a, string b);

public static class utils {

	//harusnya ada Persistent data
	public static bool sfxON = true, musicON = true;

	public enum loadedContentType { achievement, gem, part, powerup, vehicle, weapon, zombie }
	static TinyXmlReader xmlReader;
	static string fileXML = "*";

	public static bool isSfx { get; set; }
	public static bool isMusic { get; set; }

	public static LoadableContent[] loadSpecificXML (loadedContentType tag){
		System.Collections.Generic.List<string> stringTags = new System.Collections.Generic.List<string>(getXMLTag (tag));
		string typenameTag = stringTags [0]; stringTags.RemoveAt (0);
		System.Collections.Generic.List<LoadableContent> contents = new System.Collections.Generic.List<LoadableContent> ();
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
							while (xmlReader.Read (dictionaryTag)) {
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
		System.Collections.Generic.List<string> tag = new System.Collections.Generic.List<string> ();
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
	public static System.Collections.Generic.List<bool> getAllUnlockedWeapon(){
		System.Collections.Generic.List<bool> res = new System.Collections.Generic.List<bool>();

		res.Add (PlayerPrefs.GetBool("UMachineGun"));
		res.Add (PlayerPrefs.GetBool("UGrenadeLauncher"));
		res.Add (PlayerPrefs.GetBool("UCryoGun"));

		return res;
	}
	public static void buyWeapon(string weaponname){
		PlayerPrefs.SetBool (weaponname, true);
	}

	//equipped weapon, index arrangement equals to the unlocked weapon index arrangement
	// index 0, key "EMachineGun" =  Machine Gun
	// index 1, key "EGrenadeLauncher" =  Grenade Launcher
	// index 2, key "ECryoGun" =  Cryo Gun
	public static System.Collections.Generic.List<bool> getAllEquippedWeapon(){
		System.Collections.Generic.List<bool> res = new System.Collections.Generic.List<bool>();
		
		res.Add (PlayerPrefs.GetBool("EMachineGun"));
		res.Add (PlayerPrefs.GetBool("EGrenadeLauncher"));
		res.Add (PlayerPrefs.GetBool("ECryoGun"));

		return res;
	}
	public static void equipWeapon(string weaponname){
		PlayerPrefs.SetBool (weaponname, true);
	}
	public static void unequipWeapon(string weaponname){
		PlayerPrefs.SetBool (weaponname, false);
	}

	//INSERT OTHER PREFERENCES HERE

	public static void SetInitialPersistent(){

	}
}
