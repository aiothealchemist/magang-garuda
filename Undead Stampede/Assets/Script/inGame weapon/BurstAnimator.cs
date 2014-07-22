using UnityEngine;
using System.Collections;

public class BurstAnimator : MonoBehaviour {
	public Sprite[] burstingSprites;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;
	private float burstTimer = 0;
	private Transform objPos;
	float a = 0;

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
		objPos = transform;
		objPos.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		animateBursting ();
		Vector3 newPos = new Vector3 (transform.position.x - 0.02f,transform.position.y,transform.position.z);
		objPos.position = newPos;
	}

	void animateBursting(){
		int index = (int)(burstTimer * framesPerSecond);
		index = index % burstingSprites.Length;
		spriteRenderer.sprite = burstingSprites [index];
		burstTimer += Time.deltaTime;
	}
}
