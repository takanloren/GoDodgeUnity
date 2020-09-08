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
        ShieldText.text = GameManager.Instance.PlayerEquipment.shields.ToString();
        SpeedPotionText.text = GameManager.Instance.PlayerEquipment.speed_potion.ToString();
    }

    public void ActivateShield()
    {
        if(GameManager.Instance.PlayerEquipment.shields > 0)
        {
            GameManager.Instance.PlayerEquipment.shields--;
            GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnShield;

            SQLiteHelper.INSTANCE.UpdatePlayerEquipment(GameManager.Instance.PlayerEquipment);
        }
    }

    public void ActivateSpeedPotion()
    {
        if (GameManager.Instance.PlayerEquipment.speed_potion > 0)
        {
            GameManager.Instance.PlayerEquipment.speed_potion--;
            GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnSpeedPotion;

            SQLiteHelper.INSTANCE.UpdatePlayerEquipment(GameManager.Instance.PlayerEquipment);
        }
    }
}
