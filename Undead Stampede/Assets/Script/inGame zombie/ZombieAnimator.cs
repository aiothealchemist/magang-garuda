using UnityEngine;
using System.Collections;

public class ZombieAnimator : MonoBehaviour {

	public Sprite[] runningSprites;
	public Sprite[] attackingSprites;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;
	public string state;

	// Use this for initialization
	void Start () {
		state = "chasing";
		spriteRenderer = renderer as SpriteRenderer;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "chasing") {
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % runningSprites.Length;
						spriteRenderer.sprite = runningSprites [index];
				} else if (state == "attacking"){
						int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
						index = index % attackingSprites.Length;
						spriteRenderer.sprite = attackingSprites [index];
						//set back to chasing after finished attacking
						if (index == attackingSprites.Length - 1){
							state = "chasing";
						}
		}
	}
}
