/* Author: Gao Ziyu
 * Date: 09/ 06 /2023
 * Description: The GameManager class is used for game managing
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private PlayerMovement activePlayer;

    public static GameManager instance;

    public static int score;

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
        Debug.Log("Entering scene is : " + next.buildIndex);
        {
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
            }

        }

    }



    // Update is called once per frame
    void Update()
    {
        Debug.Log(activePlayer.gameObject.name);
    }
}
