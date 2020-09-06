using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
	public Rigidbody2D rb2D;
	public Tilemap tilemap;

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
		Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
		Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

		SetLimits(minTile, maxTile);

		mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        //We want the game is unexpected!!
        velocityX = _random.Next(-30, 30);
        velocityY = _random.Next(-30, 30);
		speed = _random.Next(2, 5);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if (rb2D.position.x <= xMin || rb2D.position.x >= xMax)
		{
			velocityX *= -1;
		}
		else if(rb2D.position.y <= yMin || rb2D.position.y >= yMax)
		{
			velocityY *= -1;
		}

		MoveObject(velocityX, velocityY);
	}

	void MoveObject(float x, float y)
	{
        if(x < 0)
        {
            if (mySpriteRenderer != null)
            {
                // flip the sprite
                mySpriteRenderer.flipX = true;
            }
        }else
        {
            if (mySpriteRenderer != null)
            {
                // flip the sprite
                mySpriteRenderer.flipX = false;
            }
        }

		Vector3 tempVect = new Vector3(x, y, 0);
		tempVect = tempVect.normalized * speed * Time.deltaTime;

		var availableVector = rb2D.transform.position + tempVect;
		availableVector.x = Mathf.Clamp(availableVector.x, xMin, xMax);
		availableVector.y = Mathf.Clamp(availableVector.y, yMin, yMax);
		rb2D.MovePosition(availableVector);		
	}

	private void SetLimits(Vector3 minTile, Vector3 maxTile)
	{
		Camera cam = Camera.main;

		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;

		xMin = minTile.x + objectWidth;
		xMax = maxTile.x - objectWidth;

		yMin = minTile.y + objectHeight;
		yMax = maxTile.y - objectHeight;
	}
}
