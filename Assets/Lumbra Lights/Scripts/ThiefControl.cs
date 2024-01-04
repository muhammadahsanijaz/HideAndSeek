using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefControl : MonoBehaviour {
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer SR;
    public float Speed = 0;
    private Transform hijo;
    private bool D = false;
    private bool A = false;
    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        SR = gameObject.GetComponent<SpriteRenderer>();
        hijo = this.gameObject.transform.GetChild(0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            A = true;
            animator.SetBool("Walk", true);
            SR.flipX = true;
            hijo.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKey(KeyCode.D))
        {
            D = true;
            animator.SetBool("Walk", true);
            SR.flipX = false;
            hijo.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }
    private void FixedUpdate()
    {
        if (D)
        {
            rb.MovePosition(rb.position + Vector2.right * Time.fixedDeltaTime * Speed);
            animator.SetBool("Walk", false);
            D = false;
        }
        if (A)
        {
            rb.MovePosition(rb.position + Vector2.left * Time.fixedDeltaTime * Speed);
            animator.SetBool("Walk", false);
            A = false;
        }
    }
}
