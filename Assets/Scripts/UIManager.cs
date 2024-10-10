using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    Sequence cameraSequence;
    public Transform cameraTransform;

    public GameObject startMenu, deathMenu;

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
        //freezes game
        Time.timeScale = 0;
        startMenu.SetActive(true);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
        GameManager.Instance.gameStarted = true;
    }

    public void RetryGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        deathMenu.SetActive(false);
    }

    public void OpenFailMenu()
    {
        cameraSequence.Append(cameraTransform.DOShakePosition(0.5f, 1, 10, 90));
        deathMenu.SetActive(true);
    }
}
