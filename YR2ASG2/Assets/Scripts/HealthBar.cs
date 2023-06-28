using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    //set max hp
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //Changes color of healthbar depend on health state
        gradient.Evaluate(1f); //1 is all the way to right, 0 all the way to left
    }
    //adjust value
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
