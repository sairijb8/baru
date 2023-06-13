//using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocScript : MonoBehaviour
{
    public float speed = 5f;  // The walking speed of the character
    public float jumpForce = 5f;
    public Rigidbody2D projectile;
    public float projectileSpeed = 4;
    public Transform bulloc;
    public Rigidbody2D rb2D; // Rigidbody2D component reference
    private bool isJumping = false;
    private float moveDirection;
    public SpriteRenderer spriteRenderer; // SpriteRenderer component reference
    public Animator anim;
    
    string _currentState;
    const string IDLE = "Idle";
    const string RUN = "Walk";
    const string JUMP = "Jump";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set moveDirection based on input
        moveDirection = Input.GetAxis("Horizontal");
        
        // Move the character
        rb2D.velocity = new Vector2(moveDirection * speed, rb2D.velocity.y);

        // Flip the character sprite if moving in the opposite direction
        if (moveDirection < 0)
        {
            spriteRenderer.flipX = true;
            //ChangeAnimationState(RUN);
        }            
        else if (moveDirection > 0)
        {
            spriteRenderer.flipX = false;
            
        }
        Debug.Log(rb2D.velocity.magnitude);
        if (rb2D.velocity.magnitude <= 0.5f)
        {
            ChangeAnimationState(IDLE);
        }
        else
        {
            ChangeAnimationState(RUN);
        }
            

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            // Terapkan gaya lompatan pada rigidbody
            rb2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)){
            Rigidbody2D p = Instantiate(projectile, bulloc.position, bulloc.rotation);
            p.AddForce(new Vector2(4f, 0f), ForceMode2D.Impulse);
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (newState == _currentState)
        {
            return;
        }

        anim.Play(newState);
        _currentState = newState;
    }

    bool isAnimationPlaying(Animator animator, string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ground"){
            Debug.Log("Tana");
            isJumping = false;
        }
        
    }
    
}
