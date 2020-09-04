using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StayInSide : MonoBehaviour
{
	public Camera MainCamera; //be sure to assign this in the inspector to your main camera
	public SpriteRenderer backgroundSR;
	public RectTransform backgroundRectTrans;

	public Tilemap tilemap;
	private float xMax, xMin, yMax, yMin;

	private Vector2 screenBounds;
	private float objectWidth;
	private float objectHeight;
	// Start is called before the first frame update
	void Start()
    {
		Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
		Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);
		objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
		objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

		SetLimits(minTile, maxTile);

		//screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		////screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(backgroundSR.sprite.rect.width * backgroundRectTrans.localScale.x, (backgroundSR.sprite.rect.height * 2) * backgroundRectTrans.localScale.y, Camera.main.transform.position.z));
	}

    // Update is called once per frame
    void LateUpdate()
    {
		////screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(backgroundSR.sprite.rect.width * backgroundRectTrans.localScale.x, (backgroundSR.sprite.rect.height * 2) * backgroundRectTrans.localScale.y, Camera.main.transform.position.z));
		//Vector3 viewPos = transform.position;
		//screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

		//viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
		//viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

		//transform.position = viewPos;

		var xVector = Mathf.Clamp(transform.position.x, xMin, xMax);
		var yVector = Mathf.Clamp(transform.position.y, yMin, yMax);

		transform.position = new Vector3(xVector, yVector, transform.position.z);
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
