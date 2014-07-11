//this is a bloated code with a lot of 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayerPrefs = PreviewLabs.PlayerPrefs;

namespace Utils {
	public delegate void delegateVoidWithZeroParam();
	public delegate void delegateVoidWithTwoParams_String_String(string a, string b);

	class PrefsAccess {
		//player preferences indices
		public enum integerKey {PlayerGems, ExtraCoins, ExtraHealth, ExtraSpeed, UnlockedLevel};
		public enum booleanKey {SFXSetting, BGMSetting};
		public enum enumeratedBooleanKey {
			UnlockedWeapon, UnlockedVehicle, UnlockedPart,
			EquippedWeapon, EquippedVehicle, EquippedPart,
			UnlockedAchievement
		};
		//unlockables
		public enum Parts{Engine, Armor};
		public enum Weapons{MachineGun,GrenadeLauncher,CryoGun};
		public enum Vehicle{Bus,Truck,RV};
		
		//prefs setter
		public static void setIntegerPrefs(integerKey key, int value){
			PlayerPrefs.SetInt (key.ToString (), value);
		}
		public static void setBooleanPrefs(booleanKey key, bool value){
			PlayerPrefs.SetBool (key.ToString (), value);
		}
		public static void setEnumeratedBooleanPrefs(string key, int index, bool value){
			set_BitArray_PlayerPrefs (key, index, value);
		}
		public static void setEnumeratedBooleanPrefs(enumeratedBooleanKey key, int index, bool value){
			set_BitArray_PlayerPrefs (key.ToString (), index, value);
		}
		public static void setEnumeratedBooleanPrefs<TEnum>(enumeratedBooleanKey key, TEnum enumIdx, bool value) {
			set_BitArray_PlayerPrefs (key.ToString (), (int) System.Enum.ToObject(typeof(TEnum), enumIdx), value);
		}
		static void set_BitArray_PlayerPrefs(string key, int index, bool status){
			int indexInt = PlayerPrefs.GetInt(key);
			BitArray tempSet = ToBinary (indexInt);
			tempSet [index] = status;
			PlayerPrefs.SetInt (key, ToNumeral (tempSet));
		}
		
		//prefs getter
		public static int getIntegerPrefs(integerKey key){
			return PlayerPrefs.GetInt (key.ToString ());
		}
		public static bool getBooleanPrefs(booleanKey key){
			return PlayerPrefs.GetBool (key.ToString ());
		}
		public static BitArray getEnumeratedBooleanPrefs(enumeratedBooleanKey key){
			return ToBinary(PlayerPrefs.GetInt(key.ToString ()));
		}
		
		//prefs properties
		//gems
		public static void substractGems(int gems){ Gem -= gems; }
		public static void addGems(int gems){ Gem += gems; }
		public static int Gem{
			get{ return PlayerPrefs.GetInt(integerKey.PlayerGems.ToString ()); }
			set{ PlayerPrefs.SetInt(integerKey.PlayerGems.ToString (), value); }
		}
		//powerups
		public static int PowerupsExtraCoin{
			get{ return PlayerPrefs.GetInt(integerKey.ExtraCoins.ToString ()); }
			set{ PlayerPrefs.SetInt(integerKey.ExtraCoins.ToString (), value); }
		}
		public static int PowerupsExtraSpeed{
			get{ return PlayerPrefs.GetInt(integerKey.ExtraSpeed.ToString ()); }
			set{ PlayerPrefs.SetInt(integerKey.ExtraSpeed.ToString (), value); }
		}
		public static int PowerupsExtraHealth{
			get{ return PlayerPrefs.GetInt(integerKey.ExtraHealth.ToString ()); }
			set{ PlayerPrefs.SetInt(integerKey.ExtraHealth.ToString (), value); }
		}
		//BGM and SFX toggle
		public static bool persistenceSFX {
			get { return PlayerPrefs.GetBool(booleanKey.SFXSetting.ToString ()); }
			set { PlayerPrefs.SetBool(booleanKey.SFXSetting.ToString (), value); }
		}
		public static bool persistenceBGM {
			get { return PlayerPrefs.GetBool(booleanKey.BGMSetting.ToString ()); }
			set { PlayerPrefs.SetBool(booleanKey.BGMSetting.ToString (), value); }
		}
		//unlocked level
		public static int UnlockedLevel{
			get{ return PlayerPrefs.GetInt(integerKey.UnlockedLevel.ToString ()); }
			set{ PlayerPrefs.SetInt(integerKey.UnlockedLevel.ToString (), value); }
		}
		
		//bitarray 
		static BitArray ToBinary(int numeral){
			return new BitArray(new[] { numeral });
		}
		static int ToNumeral(BitArray binary){
			if (binary == null)
				throw new System.ArgumentNullException("binary");
			if (binary.Length > 32)
				throw new System.ArgumentException("must be at most 32 bits long");
			
			var result = new int[1];
			binary.CopyTo(result, 0);
			return result[0];
		}

		//INSERT OTHER PREFERENCES HERE
		public static void setInitialPersistent(){
			PlayerPrefs.DeleteAll ();
			PlayerPrefs.EnableEncryption (true);
			Gem = 100;
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.UnlockedWeapon, Weapons.MachineGun, true);
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.UnlockedWeapon, Weapons.GrenadeLauncher, true);
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.UnlockedWeapon, Weapons.CryoGun, true);
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.EquippedWeapon, Weapons.MachineGun, true);
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.EquippedWeapon, Weapons.GrenadeLauncher, true);
			setEnumeratedBooleanPrefs<Weapons> (enumeratedBooleanKey.EquippedWeapon, Weapons.CryoGun, true);
			setEnumeratedBooleanPrefs<Vehicle> (enumeratedBooleanKey.UnlockedVehicle, Vehicle.Bus, true);
			setEnumeratedBooleanPrefs<Vehicle> (enumeratedBooleanKey.EquippedVehicle, Vehicle.Bus, true);
			UnlockedLevel = 1;		
			persistenceBGM = true;
			persistenceSFX = true;
		}
	}

	class XMLLoader {
		static TinyXmlReader xmlReader;
		static string fileXML = "*";
		
		static bool findXMLTagName(ref TinyXmlReader xmlReader, string tagName, string closure = null){
			while (closure == null ? xmlReader.Read () : xmlReader.Read(closure))
				if (xmlReader.isOpeningTag && xmlReader.tagName == tagName)
					return true;
			return false;
		}
		
		static bool findXMLTagName(ref TinyXmlReader xmlReader, List<string> tagNames, string closure = null){
			foreach (string item in tagNames)
				if (findXMLTagName(ref xmlReader,item,closure))
					return true;
			return false;
		}
		
		static void loadXMLToContent(ref TinyXmlReader xmlReader, ref LoadableContent temp, string closure = null) {
			var propertyCalled = temp.GetType().GetProperty(xmlReader.tagName);
			if (propertyCalled == null) {
			} else if (propertyCalled.PropertyType.IsEnum) {
				if (xmlReader.tagName == "sisi")	//punyaan anak zombie
					temp.setSisi (xmlReader.content);
			} else if (propertyCalled.PropertyType is IDictionary) {
				string dictionaryTag = xmlReader.tagName;
				delegateVoidWithTwoParams_String_String dictMethod = 
					(dictionaryTag == "pricing") ? 
						new delegateVoidWithTwoParams_String_String(temp.setPrice) : 
						(dictionaryTag == "sprites") ? 
						new delegateVoidWithTwoParams_String_String(temp.setSprite) : null;
				while (dictMethod != null && xmlReader.Read (dictionaryTag))
					if (xmlReader.isOpeningTag)
						dictMethod(xmlReader.tagName, xmlReader.content);
			} else {	//singular variable (string, int, bool)
				propertyCalled.SetValue (
					temp, System.Convert.ChangeType (xmlReader.content, propertyCalled.PropertyType), null);
			}
		}
		
		public static LoadableContent[] loadSpecificXML (LoadableContent.loadedContentType tag){
			List<string> stringTags = new List<string>(getXMLTag (tag));
			string typenameTag = stringTags [0]; stringTags.RemoveAt (0);
			List<LoadableContent> contents = new List<LoadableContent> ();
			xmlReader = new TinyXmlReader (fileXML, true);
			
			while (findXMLTagName (ref xmlReader, typenameTag)) {
				LoadableContent temp = LoadableContent.constructContent(tag);
				while (findXMLTagName (ref xmlReader, stringTags, typenameTag))
					loadXMLToContent(ref xmlReader, ref temp);
				contents.Add (temp);
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
	}
}