using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public Text ShieldText;
    public Text SpeedPotionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		try
		{
			ShieldText.text = GameManager.Instance.PlayerEquipment.ShieldAmount.ToString();
			SpeedPotionText.text = GameManager.Instance.PlayerEquipment.SpeedPotionAmount.ToString();
		}
		catch(Exception ex)
		{
			Debug.LogError(ex.Message);
		}

    }

    public void ActivateShield()
    {
        if(GameManager.Instance.PlayerEquipment.ShieldAmount > 0)
        {
            GameManager.Instance.PlayerEquipment.ShieldAmount--;
            GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnShield;
        }
    }

    public void ActivateSpeedPotion()
    {
        if (GameManager.Instance.PlayerEquipment.SpeedPotionAmount > 0)
        {
            GameManager.Instance.PlayerEquipment.SpeedPotionAmount--;
            GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnSpeedPotion;
        }
    }
}
