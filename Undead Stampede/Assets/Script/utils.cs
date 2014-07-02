//this is a bloated code with a lot of 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerPrefs = PreviewLabs.PlayerPrefs;
using System;

public delegate void voidWithZeroParam();
public delegate void voidWithTwoParams_String_String(string a, string b);

public static class utils {
	//player preferences indices
	enum playerprefsKey {
		UnlockedWeaponInt, UnlockedVehicleInt, UnlockedPartInt,
		EquippedWeaponInt, EquippedVehicleInt, EquippedPartInt, 
		UnlockedLevel, UnlockedAchievement,
		PlayerGems, ExtraCoins, ExtraHealth, ExtraSpeed,
		SFXSetting, BGMSetting
	};
	public enum enumeratedBooleanKey {
		UnlockedWeaponInt, UnlockedVehicleInt, UnlockedPartInt,
		EquippedWeaponInt, EquippedVehicleInt, EquippedPartInt,
		UnlockedLevel, UnlockedAchievement
	};
	public enum integerKey {
		PlayerGems, ExtraCoins, ExtraHealth, ExtraSpeed
	};
	public enum booleanKey {
		SFXSetting, BGMSetting
	};

	//unlockables
	public enum Parts{Engine, Armor};
	public enum Weapons{MachineGun,GrenadeLauncher,CryoGun};
	public enum Vehicle{Bus,Truck,RV};

	//enumerated boolean
	public static void setEnumeratedBoolean(enumeratedBooleanKey key, int index, bool toggle){
		set_BitArray_PlayerPrefs (key.ToString (), index, toggle);
	}
	public static void setEnumeratedBoolean<TEnum>(enumeratedBooleanKey key, TEnum enumValue, bool toggle) {
		set_BitArray_PlayerPrefs (key.ToString (), (int) (object) enumValue, toggle);
	}
	public static BitArray getEnumeratedBoolean(enumeratedBooleanKey key){
		return ToBinary(PlayerPrefs.GetInt(key.ToString ()));
	}
	static void set_BitArray_PlayerPrefs(string key, int index, bool status){
		int indexInt = PlayerPrefs.GetInt(key);
		BitArray tempSet = ToBinary (indexInt);
		tempSet [index] = status;
		PlayerPrefs.SetInt (key, ToNumeral (tempSet));
	}
	public static BitArray ToBinary(int numeral)
	{
		return new BitArray(new[] { numeral });
	}
	public static int ToNumeral(BitArray binary)
	{
		if (binary == null)
			throw new ArgumentNullException("binary");
		if (binary.Length > 32)
			throw new ArgumentException("must be at most 32 bits long");
		
		var result = new int[1];
		binary.CopyTo(result, 0);
		return result[0];
	}

	//xml loading
	static TinyXmlReader xmlReader;
	static string fileXML = "*";

	public static LoadableContent[] loadSpecificXML (LoadableContent.loadedContentType tag){
		List<string> stringTags = new List<string>(getXMLTag (tag));
		string typenameTag = stringTags [0]; stringTags.RemoveAt (0);
		List<LoadableContent> contents = new List<LoadableContent> ();
		xmlReader = new TinyXmlReader (fileXML, true);
		while (xmlReader.Read ()) {
			if (xmlReader.isOpeningTag && xmlReader.tagName == typenameTag){
				LoadableContent temp = LoadableContent.constructContent(tag);
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

	static string[] getXMLTag(LoadableContent.loadedContentType tipe){
		List<string> tag = new List<string> ();
		tag.Add (tipe.ToString ());
		LoadableContent temp = LoadableContent.constructContent (tipe);
		foreach (var item in temp.GetType ().GetProperties ()) {
			tag.Add (item.Name);
		}
		return tag.ToArray ();
	}

	//gems
	public static int Gem{
		get{
			return PlayerPrefs.GetInt(integerKey.PlayerGems.ToString ());
		}
		set{
			PlayerPrefs.SetInt(integerKey.PlayerGems.ToString (), value);
		}
	}
	public static void addGems(int gems){
		PlayerPrefs.SetInt (integerKey.PlayerGems.ToString (), Gem + gems);
	}
	public static void substractGems(int gems){
		PlayerPrefs.SetInt (integerKey.PlayerGems.ToString (), Gem - gems);
	}

	//powerups
	public static int PowerupsExtraCoin{
		get{
			return PlayerPrefs.GetInt(integerKey.ExtraCoins.ToString ());
		}
		set{
			PlayerPrefs.SetInt(integerKey.ExtraCoins.ToString (), value);
		}
	}
	public static int PowerupsExtraHealth{
		get{
			return PlayerPrefs.GetInt(integerKey.ExtraHealth.ToString ());
		}
		set{
			PlayerPrefs.SetInt(integerKey.ExtraHealth.ToString (), value);
		}
	}
	public static int PowerupsExtraSpeed{
		get{
			return PlayerPrefs.GetInt(integerKey.ExtraSpeed.ToString ());
		}
		set{
			PlayerPrefs.SetInt(integerKey.ExtraSpeed.ToString (), value);
		}
	}
	
	//BGM and SFX toggle preference
	public static bool persistenceSFX {
		get {
			return PlayerPrefs.GetBool(booleanKey.SFXSetting.ToString ());
		}
		set {
			PlayerPrefs.SetBool(booleanKey.SFXSetting.ToString (), value);
		}
	}
	public static bool persistenceBGM {
		get {
			return PlayerPrefs.GetBool(booleanKey.BGMSetting.ToString ());
		}
		set {
			PlayerPrefs.SetBool(booleanKey.BGMSetting.ToString (), value);
		}
	}

	//INSERT OTHER PREFERENCES HERE

	public static void setInitialPersistent(){
		PlayerPrefs.DeleteAll ();
		Gem = 100;
//		buyWeapon ("UMachineGun");
//		buyWeapon("UGrenadeLauncher");
//		buyWeapon ("UCryoGun");
//		equipWeapon ("EMachineGun");
//		equipWeapon("EGrenadeLauncher");
//		equipWeapon ("ECryoGun");
//		buyVehicle ("UBus");
//		equipVehicle ("EBus");
//		unlockLevel ("level01");
//		unlockLevel ("level02");
//		unlockLevel ("level03");
		persistenceBGM = true;
		persistenceSFX = true;
	}
}
