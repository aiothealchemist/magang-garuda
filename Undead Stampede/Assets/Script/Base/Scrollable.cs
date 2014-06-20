using UnityEngine;
using System.Collections;

public class Scrollable : MonoBehaviour {

	Vector3 touchStart, touchEnd;
	Bounds kotakTujuan;
	float offset, maxOffset;
	System.Collections.Generic.List<Button> buttons;
	public bool isHorizontal;

	// Use this for initialization
	void Start () {
		touchStart = Vector3.zero;
		kotakTujuan = transform.FindChild("background").renderer.bounds;
		buttons = new System.Collections.Generic.List<Button> (
			transform.GetComponentsInChildren<Button> ());
		maxOffset = buttons.Count 
			* 1.2f //faktor buat memperhitungkan selisih antarbutton
			* buttons [0].transform.GetComponent<SpriteRenderer> ().bounds.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			touchStart = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			touchStart.z = 0;
			if (!kotakTujuan.Contains (touchStart)) {
				touchStart = Vector3.zero;
			}
		} else if (Input.GetButton("Fire1") && touchStart != Vector3.zero) {
			touchEnd = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float delta = (isHorizontal ? (touchEnd.x - touchStart.x) : (touchEnd.y - touchStart.y));
			touchStart = touchEnd;

			offset += delta;
			if (0 <= offset && offset <= maxOffset)
				buttons.ForEach (delegate(Button obj) {
						obj.transform.Translate((isHorizontal ? delta : 0), (isHorizontal ? 0 : delta), 0);
					});
		} else if (Input.GetButtonUp ("Fire1")) {
			touchStart = Vector3.zero;
		}
	}
}
