using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombieMovement : MonoBehaviour
{
	public Rigidbody2D rb2D;

	private float velocityX = 200f;
	private static System.Random _random = new System.Random();
	private SpriteRenderer mySpriteRenderer;
	private float jumpRate = 2f;
	private float nextJump = -1f;
    // Start is called before the first frame update
    void Start()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		rb2D = GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void FixedUpdate()
	{
        if (Time.time < nextJump)
        {
            
        }
        else
        {
            mySpriteRenderer.flipX = !mySpriteRenderer.flipX;

            rb2D.AddForce(new Vector2(velocityX, 0), ForceMode2D.Impulse);
            velocityX *= -1;
            nextJump = Time.time + jumpRate;
        }
    }
}
