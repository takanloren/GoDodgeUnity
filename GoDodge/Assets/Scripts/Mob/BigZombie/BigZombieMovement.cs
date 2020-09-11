using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigZombieMovement : MonoBehaviour
{
	public Rigidbody2D rb2D;

	private float speed = 5f;
	private float velocityX = 500f;
	private static System.Random _random = new System.Random();
	private SpriteRenderer mySpriteRenderer;
	private bool jumpRight = true;
	private float jumpRate = 3.0f;
	private float nextJump = -1f;

	// Start is called before the first frame update
	void Start()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		rb2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(velocityX > 0)
		{
			mySpriteRenderer.flipX = false;
		}
		else
		{
			mySpriteRenderer.flipX = true;
		}

		if (nextJump > 0)
		{
			nextJump -= Time.deltaTime;
			Debug.Log("Next Jump = "+nextJump);
		}
		else
		{
			Debug.Log("Jumping...");
			Vector3 jumpVector = new Vector3(velocityX, 0, 0);
			var availableVector = jumpVector.normalized * speed * Time.deltaTime;
			rb2D.MovePosition(transform.position + availableVector);

			nextJump += jumpRate;
		}
	}
}
