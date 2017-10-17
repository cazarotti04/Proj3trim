using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
0 - idle
1 - run
2 - jump
3 - fall
4 - land
5 - attack
6 - dead
 */


public class PlayerController : MonoBehaviour {

	public float horizontalSpeed = 10f;
	public float jumpSpeed = 600f;

	Rigidbody2D rb;

	SpriteRenderer sr;

	Animator anao;

	bool isJumping = false;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anao = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontalInput = Input.GetAxisRaw("Horizontal");//-1 = Left // 1 = Right
		float horizontalPlayerSpeed = horizontalSpeed * horizontalInput;

		if(horizontalPlayerSpeed != 0){
			MoveHorizontal(horizontalPlayerSpeed);
		}
		else{
			StopMovingHorizontal();
		}

		if(Input.GetButtonDown("Jump")){
			Jump();
		}
		
		ShowFall();
	}

	void MoveHorizontal(float speed){
		rb.velocity = new Vector2(speed, rb.velocity.y);

		if(speed < 0f){
			sr.flipX = true;
		}
		else if(speed > 0f){
			sr.flipX = false;
		}
		if(!isJumping){
			anao.SetInteger("State", 1);
		}
	}

	void StopMovingHorizontal(){
		rb.velocity = new Vector2(0f,rb.velocity.y);
		if(!isJumping){
			anao.SetInteger("State", 0);
		}
	}

	void ShowFall(){
		if(rb.velocity.y<0f){
			anao.SetInteger("State", 3);
		}
	}

	void Jump(){
		isJumping = true;
		rb.AddForce(new Vector2(0f, jumpSpeed));
		anao.SetInteger("State", 2);
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
			isJumping = false;
		}
	}

}
