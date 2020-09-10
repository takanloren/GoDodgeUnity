using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipmentHandler : MonoBehaviour
{
    public Button ShieldButton;
    public Button SpeedButton;
    public ParticleSystem ShieldAura;
    public ParticleSystem SpeedAura;
    public EffectRemainingBar RemainingBar;

	private float shieldTimeRemaining = Constants.SHIELD_TIME;
    private float speedPotionTimeRemaining = Constants.SPEED_POTION_TIME;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect != GameManager.BuffEffects.OnNormal)
        {
            timerIsRunning = true;
            ShieldButton.interactable = false;
            SpeedButton.interactable = false;

			switch (GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect)
			{
				case GameManager.BuffEffects.OnShield:
					RemainingBar.SetSliderMaxValue(Constants.SHIELD_TIME);
					break;
				case GameManager.BuffEffects.OnSpeedPotion:
					RemainingBar.SetSliderMaxValue(Constants.SPEED_POTION_TIME);
					break;
			}
        }
        else
        {
            RemainingBar.SetRemainingTime(0);
            RemainingBar.SetEffectName(string.Empty);

            if (GameManager.Instance.PlayerEquipment.ShieldAmount <= 0)
            {
                ShieldButton.interactable = false;
            }
            else
            {
                ShieldButton.interactable = true;
            }

            if (GameManager.Instance.PlayerEquipment.SpeedPotionAmount <= 0)
            {
                SpeedButton.interactable = false;
            }
            else
            {
                SpeedButton.interactable = true;
            }
        }

        if (timerIsRunning)
        {
            GameManager.BuffEffects currentEffect = GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect;

            if (currentEffect == GameManager.BuffEffects.OnShield)
            {
                if (shieldTimeRemaining > 0)
                {
                    ShieldAura.gameObject.SetActive(true);
                    ShieldAura.transform.position = this.transform.position;

                    shieldTimeRemaining -= Time.deltaTime;
                    RemainingBar.SetRemainingTime(shieldTimeRemaining);
                    RemainingBar.SetEffectName(Constants.PLAYER_EFFECT_SHIELD);
                }
                else
                {
                    RemainingBar.SetRemainingTime(0);
                    RemainingBar.SetEffectName(string.Empty);

                    Debug.Log("Time has run out!");
                    shieldTimeRemaining = Constants.SHIELD_TIME;
                    timerIsRunning = false;
                    GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnNormal;
                    ShieldAura.gameObject.SetActive(false);
                }
            }
            else if (currentEffect == GameManager.BuffEffects.OnSpeedPotion)
            {
                if (speedPotionTimeRemaining > 0)
                {
                    SpeedAura.gameObject.SetActive(true);
                    SpeedAura.transform.position = this.transform.position;

                    speedPotionTimeRemaining -= Time.deltaTime;
                    RemainingBar.SetRemainingTime(speedPotionTimeRemaining);
                    RemainingBar.SetEffectName(Constants.PLAYER_EFFECT_SPEED);
                }
                else
                {
                    RemainingBar.SetRemainingTime(0);
                    RemainingBar.SetEffectName(string.Empty);

                    Debug.Log("Time has run out!");
                    speedPotionTimeRemaining = Constants.SPEED_POTION_TIME;
                    timerIsRunning = false;
                    GameManager.Instance.ActiveRunAttemp.ActiveBuffEffect = GameManager.BuffEffects.OnNormal;

                    SpeedAura.gameObject.SetActive(false);
                }
            }
        }
    }
}
