using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float velocityX = 2f;
    public float velocityY = 2f;
    public float speed = 1f;
    private float objectWidth;
    private float objectHeight;
    private static System.Random _random = new System.Random();
    private SpriteRenderer mySpriteRenderer;
    private int _hardCodeMargin = 20;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        //We want the game is unexpected!!
        velocityX = _random.Next(-90, 90);
        velocityY = 0;
        speed = _random.Next(1, 3);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);

        if (screenPos.x - objectWidth - _hardCodeMargin <= 0 || screenPos.x + objectWidth + _hardCodeMargin >= Screen.width)
        {
            velocityX *= -1;
        }
        else if (screenPos.y - objectHeight - _hardCodeMargin <= 0 || screenPos.y + objectHeight + _hardCodeMargin >= Screen.height)
        {
            velocityY *= -1;
        }

        MoveObject(velocityX, velocityY);
    }

    void MoveObject(float x, float y)
    {
        if (x < 0)
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

        Vector3 tempVect = new Vector3(x, y, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb2D.MovePosition(rb2D.transform.position + tempVect);
    }
}
