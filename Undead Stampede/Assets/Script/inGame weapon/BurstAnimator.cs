using UnityEngine;
using System.Collections;

public class BurstAnimator : MonoBehaviour {
	public Sprite[] burstingSprites;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;
	private float burstTimer = 0;

	// Use this for initialization
	void Start () {
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		animateBursting ();
	}

	void animateBursting(){
		int index = (int)(burstTimer * framesPerSecond);
		index = index % burstingSprites.Length;
		spriteRenderer.sprite = burstingSprites [index];
		burstTimer += Time.deltaTime;
		if (index == burstingSprites.Length - 1){
			DestroyObject(gameObject);
		}
	}
}
