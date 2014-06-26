using UnityEngine;
using System.Collections;

public delegate void delegatedMethod();

public static class utils {

	//harusnya ada Persistent data
	public static bool sfxON = true, musicON = true;
	static TinyXmlReader xmlReader;
	static string fileXML = "*";

	public static bool isSfx { get; set; }
	public static bool isMusic { get; set; }

	static DLC[] loadSpecificXML (string tag){
		System.Collections.Generic.List<DLC> arrayOfDLC = new System.Collections.Generic.List<DLC> ();
		xmlReader = new TinyXmlReader (fileXML, true);
		while (xmlReader.Read ()) {
			if (xmlReader.tagName == tag && xmlReader.isOpeningTag){
				DLC temp = new DLC();
				while(xmlReader.Read(tag)) // read as long as not encountering the closing tag for Skills
				{
					if (xmlReader.isOpeningTag)
					{
						//text += ("Skill: " + xmlReader.tagName + " \"" + xmlReader.content + "\"\n");
					}
				}
				arrayOfDLC.Add (temp);
			}
		}
		return arrayOfDLC.ToArray();
	}

	public static DLC[] getWeapon(){
		return loadSpecificXML ("Weapon");
	}
	
	public static DLC[] getParts(){
		return loadSpecificXML ("Part");
	}
	
	public static DLC[] getVehicles(){
		return loadSpecificXML ("Vehicle");
	}
	
	public static DLC[] getAchievements(){
		return loadSpecificXML ("Achievement");
	}

	public static DLC[] getGems(){
		return loadSpecificXML ("Gem");
	}
}
