using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MushroomMovement : MonoBehaviour
{
	public Rigidbody2D rb2D;
	public Tilemap tilemap;
	public Transform PlayerTrans;

	private float speed = 1f;
	private float velocityX = 1f;
	private float velocityY = 1f;
	private float objectWidth;
	private float objectHeight;
	private static System.Random _random = new System.Random();
	private SpriteRenderer mySpriteRenderer;
	private float xMax, xMin, yMax, yMin;

	// Start is called before the first frame update
	void Start()
	{
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		rb2D = GetComponent<Rigidbody2D>();
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

		//We want the game is unexpected!!
		//velocityX = _random.Next(-90, 90);
		//velocityY = 0;
		speed = _random.Next(1, 2);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Vector3 direction = PlayerTrans.position - transform.position;
		Debug.Log("Chasing direction x: " + direction.x + "y: " + direction.y);

		if(direction.x < 0)
		{
			if (mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = true;
			}
		}
		else
		{
			if (mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = false;
			}
		}

		var availableVector = direction.normalized * speed * Time.deltaTime;
		rb2D.MovePosition(transform.position + availableVector);
	}
}
