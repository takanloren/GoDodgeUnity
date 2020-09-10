using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectRemainingBar : MonoBehaviour
{
    public Slider slider;
    public Text effectName;

    public void SetRemainingTime(float time)
    {
        slider.value = time;
    }

    public void SetEffectName(string name)
    {
        effectName.text = name;
    }

	public void SetSliderMaxValue(float maxValue)
	{
		slider.maxValue = maxValue;
	}
}
