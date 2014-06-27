using UnityEngine;
using System.Collections;

public abstract class LoadableContent {

	//di sini termasuk achievement, parts, powerups
	public enum currency { gem, coin, realMoney }
	public enum spriteTypes { button, ingame, explode, projectile }
	//	enum spriteTypes: 
	//		zombie{ingame, explode}; 
	//		weapon{all}; 
	//		vehicle{button, ingame, explode}; 
	//		powerups{null};
	//		part{button}; 
	//		gem{button};
	//		achievement{null}; 

	//properties
	public string name { get; set; }
	public string description  { get; set; }
	public System.Collections.Generic.Dictionary<currency, int> pricing { get; private set; }
	public System.Collections.Generic.Dictionary<spriteTypes, Sprite> sprites { get; private set; }

	public LoadableContent () {
		sprites = new System.Collections.Generic.Dictionary<spriteTypes, Sprite> ();
		pricing = new System.Collections.Generic.Dictionary<currency, int> ();
	}
	
	public void setPrice(currency money, int price){
		pricing [money] = price;
	}

	public void setSprite(spriteTypes tipe, Sprite sprite){
		sprites [tipe] = sprite;
	}

	// ToParse price & sprite
	public void setPrice(string money, string price){
		setPrice (money == "realMoney" ? currency.realMoney : money == "gem" ? currency.gem : currency.coin,
		          int.Parse(price));
	}
	public void setSprite(string tipe, string sprite){
		setSprite (tipe == "button" ? spriteTypes.button : tipe == "ingame" ? spriteTypes.ingame :
		           tipe == "explode" ? spriteTypes.explode : spriteTypes.projectile,
		           null);	//parse sprite path: 
	}
}

public class ZombieXML : LoadableContent {
	public enum zombieSide {atas, belakang, samping};

	//properties
	public zombieSide sisi { get; set; }
	public int baseSpeed { get; set; } 
	public int baseHealth{ get; set; } 
	public int baseDamage{ get; set; } 
	public bool isGaet { get; set; }

	public ZombieXML() : base() { }
}

class WeaponXML : LoadableContent {
	//properties
	public bool isDariLangit { get; set; }
	public bool isParabolic { get; set; } 
	public int blastRadius { get; set; } 
	public int initSpeed { get; set; }
	public int fireRate { get; set; } 
	public int damage { get; set; } 

	public WeaponXML() : base() { }
}

class VehicleXML : LoadableContent {
	//properties
	public int baseHealth{ get; set; } 
	public int baseSpeed { get; set; } 

	public VehicleXML() : base() { }
}

class PowerupXML : LoadableContent {
	public PowerupXML() : base() { }
}

class PartXML : LoadableContent {
	public PartXML() : base() { }
}

class GemXML : LoadableContent {
	public GemXML() : base() { }
}

class AchievementXML : LoadableContent {
	public AchievementXML() : base() { }
}
