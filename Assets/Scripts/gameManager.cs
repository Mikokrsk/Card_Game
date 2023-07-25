//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;

public class gameManager : MonoBehaviour
{
    public GameState gameState = GameState.menu;
    [SerializeField] private GameObject battleCanvas;
    [SerializeField] private GameObject battleManager;
    [SerializeField] private GameObject adventureCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject menuCanvas;
    public static gameManager Instance;
    // [SerializeField] private Player playerController;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        // SaveManager.Instance.LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.menu && menuCanvas.active == false)
        {
            MenuState(true);
        }
        if (gameState == GameState.adventure && adventureCanvas.active == false)
        {
            AdventureState(true);
        }
        if (gameState == GameState.battle && battleCanvas.active == false)
        {
            BattleState(true);
        }
    }

    public void MenuState(bool active)
    {
        if (active)
        {
            gameState = GameState.menu;            
            menuCanvas.SetActive(active);
            LoadScene(0);           
        }
        else
        {
            gameState = GameState.adventure;
            menuCanvas.SetActive(active);
            AdventureState(!active);

        }

    }

    public void AdventureState(bool active)
    {
        if (active)
        {
            gameState = GameState.adventure;
            SaveManager.Instance.LoadPlayerData();
        }
        else BattleState(false);
        
        adventureCanvas.SetActive(active);
        player.SetActive(active);
    }

    public void BattleState(bool active)
    {
        if (active)
        {
            if (adventureCanvas.active == false) AdventureState(true);
            gameState = GameState.battle;
        }
        battleManager.SetActive(active);
        battleCanvas.SetActive(active);
    }

    public void PauseState(bool active)
    {
        if (active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
public enum GameState
{
    menu,
    adventure,
    battle
}
