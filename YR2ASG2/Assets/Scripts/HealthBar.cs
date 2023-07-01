/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The HealthBar class is used for player health system
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// slider to adjust healthbar
    /// </summary>
    public Slider slider;

    /// <summary>
    /// gradient to set different states of healthbar color
    /// </summary>
    public Gradient gradient;

    /// <summary>
    /// image fill so that it will adjust according to the health values
    /// </summary>
    public Image fill;

    /// <summary>
    /// set max health value
    /// </summary>
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        //Changes color of healthbar depend on health state
        gradient.Evaluate(1f); //1 is all the way to right, 0 all the way to left
    }

    /// <summary>
    /// adjusts the health values
    /// </summary>
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
