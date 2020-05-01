using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ProcessDeath()
    {
        playerLives--;
        if (playerLives <= 0) {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
