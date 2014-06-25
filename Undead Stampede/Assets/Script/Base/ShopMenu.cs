using UnityEngine;
using System.Collections;

public class ShopMenu : Menu {
	
	public Transform buyPrefab;
	
	private string chosenButton;
	private string objectName, objectCurrency;
	private int objectPrice;
	
	// Use this for initialization
	protected override void Start () {
		base.Start ();

		objectCurrency = "";
		objectName = "";
		objectPrice = 0;
	}
	
	public override void chosen (string name){
		switch (name) {
		case "buy":
			instantPrompt(buyPrefab, 
					new string[] {"Do you want to buy "+objectName+"? \nIt wil cost you "+objectCurrency+objectPrice+".","Okay","Nope"},
					new delegatedMethod[] { buy });
			break;
		case "back":
			Destroy(gameObject);
			break;
		default:
			//some dude is choosing something to buy from us
			//if (name complies the rules about things to buy) then do something
			break;
		}
	}

	//buy the thing
	void buy() {}
}
