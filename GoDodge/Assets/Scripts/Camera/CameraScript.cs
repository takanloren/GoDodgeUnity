using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;

	// Update is called once per frame
	void Update()
	{
		var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

		//Debug.Log($"Camera position: {transform.position.x}");
		var xVector = player.position.x;
		var yVector = player.position.y;

		//Debug.Log($"Player position: {xVector}");
		//Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
		//Debug.Log($"screenPos position X: {screenBounds.x}");
		//if (xVector < -1 || xVector > 1) // << This muse be calculate to fit all screen
		//{
		//	xVector = transform.position.x;
		//}

		if (yVector < 0 || yVector > 11)
		{
			yVector = transform.position.y;
		}

		transform.position = new Vector3(xVector, yVector, transform.position.z);
	}
}
