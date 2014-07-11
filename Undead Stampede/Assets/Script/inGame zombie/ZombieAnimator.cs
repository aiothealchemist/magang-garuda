using UnityEngine;
using System.Collections;

public class ZombieAnimator : MonoBehaviour {

	public Sprite[] runningSprites;
	public Sprite[] attackingSprites;
	public float framesPerSecond;
	private SpriteRenderer spriteRenderer;
	public string state;
	private float chaseTimer = 0;
	private float attackTimer = 0;

	// Use this for initialization
	void Start () {
		state = "chasing";
		spriteRenderer = renderer as SpriteRenderer;
	}

	void animateChasing(){
		int index = (int)(chaseTimer * framesPerSecond);
		index = index % runningSprites.Length;
		spriteRenderer.sprite = runningSprites [index];
		chaseTimer += Time.deltaTime;
	}

	void animateAttacking(){
		chaseTimer = 0;
		int index = (int)(attackTimer * framesPerSecond);
		index = index % attackingSprites.Length;
		spriteRenderer.sprite = attackingSprites [index];
		attackTimer += Time.deltaTime;
		//set back to chasing after finished attacking
		if (index == attackingSprites.Length - 1){
			state = "chasing";
			attackTimer = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (state == "chasing") {
					animateChasing();
				} else if (state == "attacking"){
					animateAttacking();
		}
	}
}
