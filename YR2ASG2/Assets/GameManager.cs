/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The GameManager class is used for game managing
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //prefab of player used for spawning
    public GameObject playerPrefab;

    public bool ResetPlayer;
    //
    private PlayerMovement activePlayer;

    public static GameManager instance;

    public Image fadeImage;
    private Color fadeColor = new Color();

    private bool OnTransition;
    private bool OnFadeIn;
    private bool OnFadeOut;

    private float fadeDuration = 1.5f;
    private float fadeTimer;

    //public static int score;

    private void FadeOut()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer >= fadeDuration)
        {
            fadeTimer = fadeDuration;
            OnFadeOut = false;
        }
        fadeColor.a = fadeTimer / fadeDuration;
        fadeImage.color = fadeColor;
    }

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

    private void Start()
    {
        OnTransition = true;
        OnFadeOut = true;
        fadeTimer = fadeDuration;
    }

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
    // Start is called before the first frame update
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
                Debug.Log(activePlayer.gameObject.name);
            }
            else
            {
                activePlayer.transform.position
                    = marker.transform.position;
                activePlayer.transform.rotation
                    = marker.transform.rotation;
                Debug.Log("Player position:" + playerPrefab.transform.position );
            }

        }

    }

}
