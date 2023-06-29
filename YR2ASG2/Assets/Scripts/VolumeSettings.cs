/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The VolumeSettings class is used for changing volume settings on the main menu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
