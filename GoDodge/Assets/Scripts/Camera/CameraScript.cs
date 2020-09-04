using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraScript : MonoBehaviour
{
	public Transform player;
	public Tilemap tilemap;
	private float xMax, xMin, yMax, yMin;

	private void Start()
	{
		Vector3 minTile = tilemap.CellToWorld(tilemap.cellBounds.min);
		Vector3 maxTile = tilemap.CellToWorld(tilemap.cellBounds.max);

		SetLimits(minTile, maxTile);
	}

	// Update is called once per frame
	void Update()
	{
		//var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

		////Debug.Log($"Camera position: {transform.position.x}");
		//var xVector = player.position.x;
		//var yVector = player.position.y;

		////Debug.Log($"Player position: {xVector}");
		////Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
		////Debug.Log($"screenPos position X: {screenBounds.x}");
		////if (xVector < -1 || xVector > 1) // << This muse be calculate to fit all screen
		////{
		////	xVector = transform.position.x;
		////}

		////if (yVector < 0 || yVector > 11)
		////{
		////	yVector = transform.position.y;
		////}

		//transform.position = new Vector3(xVector, yVector, transform.position.z);

		if(player != null)
		{
			var xVector = Mathf.Clamp(player.position.x, xMin, xMax);
			var yVector = Mathf.Clamp(player.position.y, yMin, yMax);

			transform.position = new Vector3(xVector, yVector, transform.position.z);
		}
	}

	private void SetLimits(Vector3 minTile, Vector3 maxTile)
	{
		Camera cam = Camera.main;

		float height = 2f * cam.orthographicSize;
		float width = height * cam.aspect;

		xMin = minTile.x + width / 2;
		xMax = maxTile.x - width / 2;

		yMin = minTile.y + height / 2;
		yMax = maxTile.y - height / 2;
	}
}
