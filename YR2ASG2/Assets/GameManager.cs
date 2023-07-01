/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The GameManager class is used for scenes managing & player spawn
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// prefab of player used for spawning
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// reset player respawn
    /// </summary>
    public bool ResetPlayer;

    /// <summary>
    /// scene load index
    /// </summary>
    private int loadIndex;

    /// <summary>
    /// check if player is active in the scene
    /// </summary>
    private PlayerMovement activePlayer;

    public static GameManager instance;

    /// <summary>
    /// black screen fade transition
    /// </summary>
    public Image fadeImage;
    private Color fadeColor = new Color();

    private bool OnTransition;
    private bool OnFadeIn;
    private bool OnFadeOut;

    private float fadeDuration = 1.5f;
    private float fadeTimer;

    /// <summary>
    /// black screen transition when teleport to the next scene
    /// </summary>
    public void LoadScene(int index)
    {
        loadIndex = index;
        fadeTimer = 0;
        OnTransition = true;
        OnFadeOut = true;
        OnFadeIn = false;
    }

    /// <summary>
    /// fade out transition
    /// </summary>
    private void FadeOut()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer >= fadeDuration)
        {
            fadeTimer = fadeDuration;
            OnFadeOut = false;
            SceneManager.LoadScene(loadIndex);
        }
        fadeColor.a = fadeTimer / fadeDuration;
        fadeImage.color = fadeColor;
    }

    /// <summary>
    /// fade in transition
    /// </summary>
    private void FadeIn()
    {
        fadeTimer -= Time.deltaTime;
        if (fadeTimer <= 0)
        {
            fadeTimer = 0;
            OnFadeIn = false;
            OnTransition = false;
        }
        fadeColor.a = fadeTimer / fadeDuration;
        fadeImage.color = fadeColor;
    }

    private void Update()
    {
        if (OnTransition)
        {
            if (OnFadeOut)
            {
                FadeOut();
            }
            if (OnFadeIn)
            {
                FadeIn();
            }
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        OnTransition = true;
        OnFadeOut = true;
        fadeTimer = fadeDuration;
    }

    /// <summary>
    /// spaawn player when game starts
    /// </summary>
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SpawnPlayerOnLoad;

            instance = this;
        }

    }

    /// <summary>
    /// spawn player on load
    /// </summary>
    void SpawnPlayerOnLoad(Scene prev, Scene next)
    {
        if (ResetPlayer)
        {
            ResetPlayer = false;

            if(activePlayer != null)
            {
                Destroy(activePlayer.gameObject);
                activePlayer = null;
            }
        }
        OnFadeIn = true;
        {
            if(next.buildIndex == 0)
            {
                return;
            }
            PlayerSpawnSpot marker =
                FindObjectOfType<PlayerSpawnSpot>();

            if (activePlayer == null)
            {
                GameObject player = Instantiate(playerPrefab, marker.transform.position, marker.transform.rotation);
                activePlayer = player.GetComponent<PlayerMovement>();
            }
            else
            {
                activePlayer.transform.position
                    = marker.transform.position;
                activePlayer.transform.rotation
                    = marker.transform.rotation;
            }
        }

    }

}
