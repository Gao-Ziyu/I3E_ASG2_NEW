/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The Dialogue class is used for in-game dialogues
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI mapDialogue;
    public string[] lines;
    public string[] maps;
    public float textSpeed;

    private int index;
    private bool dialogue = true;
    private bool map = false;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        mapDialogue.enabled = false;
        StartDialogue();
        FindObjectOfType<PlayerMovement>().dialogue = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == lines[index])
                {
                    OnClick();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
        }
        else if (map)
        {
            Debug.Log(maps[index]);
            if (Input.GetMouseButtonDown(0))
            {
                if (textComponent.text == maps[index])
                {
                    OnClick();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = maps[index];
                }
            }
        }
        print("dialogue: " + dialogue);
        print("map: " + map);
    }

    public void MapDialogue()
    {
        map = true;
        if (Input.GetMouseButtonDown(0))
        {
            if (mapDialogue.text == maps[index])
            {
                OnClick();
            }
            else
            {
                StopAllCoroutines();
                mapDialogue.text = maps[index];
            }
        }
        gameObject.SetActive(true);
        StartDialogue();
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (dialogue)
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else if (map)
        {
            foreach (char c in maps[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void OnClick()
    {
        if (dialogue)
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                dialogue = false;
                textComponent.text = string.Empty;
                gameObject.SetActive(false);
            }
        }
        else if (map)
        {
            if (index < maps.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                map = false;
                textComponent.text = string.Empty;
                gameObject.SetActive(false);
            }
        }
    }
}