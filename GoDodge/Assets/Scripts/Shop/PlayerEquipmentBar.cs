using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEquipmentBar : MonoBehaviour
{
	public TextMeshProUGUI gemText;
	public TextMeshProUGUI shieldText;
	public TextMeshProUGUI speedText;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		UpdatePlayerResource();

	}

	private void UpdatePlayerResource()
	{
		gemText.text = GameManager.Instance.PlayerEquipment.coins.ToString();
		shieldText.text = GameManager.Instance.PlayerEquipment.shields.ToString();
		speedText.text = GameManager.Instance.PlayerEquipment.speed_potion.ToString();
	}
}
