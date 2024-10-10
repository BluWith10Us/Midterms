using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    Player playerScript;
    Deadzone deadzone;

    public bool isGameOver = false, gameStarted = false;
    public float spawnTimerMin = 2, spawnTimerMax = 5;
    public int enemyCount;
    public GameObject[] enemyList = new GameObject[1000];

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScript = GameObject.Find("Bean (Player)").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) & isGameOver == false && gameStarted == true)
        {
            playerScript.ChangeColor();
        }
    }

    //Adds enemy to Array
    public void EnterNewEnemy(GameObject enemy)
    {
        
        enemyList[enemyCount] = enemy;
        enemyCount++;
        Debug.Log("this enemy is number: " + enemyCount);
    }

    public void GameOver()
    {
            Debug.Log("GAME OVER!");
            GameManager.Instance.isGameOver = true;
    }
}
