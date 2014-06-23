using UnityEngine;
using System.Collections;

public class DLC {

	public string description { get; private set; }
	public string name { get; private set; }
	public Hashtable pricing { get; private set; }
	public Texture buttonImage;
	public Sprite ingameImage;

	public DLC(string toParse){
		pricing = new Hashtable ();
		pricing.Add ("rp", 20);
		pricing.Add ("gem", 20);
		pricing.Add ("coin", 20);
	}
}
