//this is a bloated code with a lot of 

using UnityEngine;
using System.Collections;

public delegate void voidWithZeroParam();
public delegate void voidWithTwoParams_String_String(string a, string b);

public static class utils {

	//harusnya ada Persistent data
	public static bool sfxON = true, musicON = true;

	public enum xmlType { achievement, gem, part, powerup, vehicle, weapon, zombie }
	static TinyXmlReader xmlReader;
	static string fileXML = "*";

	public static bool isSfx { get; set; }
	public static bool isMusic { get; set; }

	public static LoadableContent[] loadSpecificXML (xmlType tag){
		System.Collections.Generic.List<string> stringTags = new System.Collections.Generic.List<string>(getXMLTag (tag));
		string typenameTag = stringTags [0]; stringTags.RemoveAt (0);
		System.Collections.Generic.List<LoadableContent> contents = new System.Collections.Generic.List<LoadableContent> ();
		xmlReader = new TinyXmlReader (fileXML, true);
		while (xmlReader.Read ()) {
			if (xmlReader.isOpeningTag && xmlReader.tagName == typenameTag){
				LoadableContent temp = constructContent(tag);
				while(xmlReader.Read(typenameTag)){ // read as long as not encountering the closing tag for Skills
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
								 new voidWithTwoParams_String_String(temp.setSprite));
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

	static string[] getXMLTag(xmlType tipe){
		string[] tag = new string[0];
		switch (tipe) {
		case xmlType.achievement:
			break;
		case xmlType.gem:
			break;
		case xmlType.part:
			break;
		case xmlType.powerup:
			break;
		case xmlType.vehicle:
			break;
		case xmlType.weapon:
			break;
		case xmlType.zombie:
			break;
		}
		return tag;
	}

	static LoadableContent constructContent(xmlType tipe){
		LoadableContent tag = null;
		switch (tipe) {
		case xmlType.achievement:
			tag = new AchievementXML();
			break;
		case xmlType.gem:
			tag = new GemXML();
			break;
		case xmlType.part:
			tag = new PartXML();
			break;
		case xmlType.powerup:
			tag = new PowerupXML();
			break;
		case xmlType.vehicle:
			tag = new VehicleXML();
			break;
		case xmlType.weapon:
			tag = new WeaponXML();
			break;
		case xmlType.zombie:
			tag = new ZombieXML();
			break;
		}
		return tag;
	}
}
