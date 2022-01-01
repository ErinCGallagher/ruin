﻿using UnityEngine;
using System.Collections;
using System;

public class ControlScriptRight : MonoBehaviour {
	
	public float maxSpeed = 5f;
	bool facingRight = true;
	Animator anim;
	SpriteRenderer rend;
	bool grounded = false;
	public Transform groundedCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;
	public float jumpForce = 550f;
	public float jumpBuffer = 0.1f; //Amount of time user can hold down the jump key before landing to initiate another jump.
	float timeTrack;
	bool jumpBufferEnable = false;
	bool bufferCanBeEnabled = true;
	float jumpBufferDifference = 0;
	float move = 0f;
	
	
	void Start () {
		anim = GetComponent<Animator>();
		
	}
	
	
	void FixedUpdate () {
		
		
		grounded = Physics2D.OverlapCircle(groundedCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
		
		if (grounded || move*Input.GetAxis("HorizontalRight") < 0 || (move*Input.GetAxis ("HorizontalRight") > 0 && Mathf.Abs(Input.GetAxis("HorizontalRight")) > Mathf.Abs(move)) || move == 0) {
			
			move = Input.GetAxis ("HorizontalRight");
		}
		anim.SetFloat ("Speed", Mathf.Abs (move));
		GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
		
		if(move > 0 && !facingRight || move < 0 && facingRight) 
			Flip ();
	}
	
	void Update() {
		if (!SceneControls.controller.paused) {
			if(Input.GetKeyDown (KeyCode.UpArrow)) {
				if (grounded) {
					Jump ();
					bufferCanBeEnabled = false;
				} else {
					if (bufferCanBeEnabled) {
						timeTrack = Time.time;
						jumpBufferEnable = true;
						bufferCanBeEnabled = false;
					}
				}
			} else if (Input.GetKey (KeyCode.UpArrow) && grounded) {
				if (jumpBufferEnable) {
					jumpBufferDifference = Time.time - timeTrack;
					if (jumpBufferDifference < jumpBuffer) {
						Jump ();
					} else {
						bufferCanBeEnabled = false;
                        jumpBufferEnable = false;
                    }
                } 
            }
            if(Input.GetKeyUp (KeyCode.UpArrow)) {
                jumpBufferEnable = false;
                bufferCanBeEnabled = true;
            }
        }
    }
    
    void Jump() {
        anim.SetBool("Ground", false);
        GetComponent<Rigidbody2D>().AddForce (new Vector2(0, jumpForce));
        jumpBufferEnable = false;
    }
    
    void Flip() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
	}
}
