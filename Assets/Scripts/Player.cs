using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TowerDefence.Towers;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public GameObject playerBase;

    [SerializeField] private GameObject gameOverScreen;

    public Text healthText;
    public Text moneyText;

    public float health = 100f;

    [SerializeField]
    public int money = 100;//money player starts with

    public bool gameIsOver = false;

    public void AddMoney(int _money)
    {
        money += _money;// adds money to player method
    }

    public void PurchaseTower(Tower _tower)
    {
        money -= _tower.Cost;//player loses money when buying towers
    }

    private void Update()
    {
        healthText.text = "Health: " + health.ToString("F0");
        moneyText.text = "Money: " + money.ToString("F0");

        GameOver();
    }

    public void GameOver()
    {
        if (health <= 0)
        {
            gameOverScreen.SetActive(true);
            gameIsOver = true;
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameIsOver = false;
        gameOverScreen.SetActive(false);
    }
}

