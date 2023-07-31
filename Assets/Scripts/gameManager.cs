//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class gameManager : MonoBehaviour
{
    // public GameState gameState;
    // public bool isAdventure;
    // public bool isMainMenu;
    public bool isBattle;
    [SerializeField] private GameObject battleCanvas;
    [SerializeField] private GameObject battleManager;
    [SerializeField] private GameObject adventureCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> characters;
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
        StartGame();
    }

    void StartGame()
    {
        if (player == null)
        {
            Debug.Log("Player is null");
            if (characters.Count <= 0)
            {
                characters.AddRange(Array.ConvertAll(Resources.LoadAll("Ñharacters", typeof(GameObject)), assets => (GameObject)assets));
            }

            var newPlayer = Instantiate(characters.First());
            newPlayer.transform.SetParent(gameObject.transform, false);
            player = newPlayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*   if (isMainMenu && menuCanvas.active == false)
           {
             //  SetState(gameState);
           }        
           if (isBattle && battleCanvas.active == false)
           {
               //SetState(gameState);
           }*/
    }

    public void MainMenuState(bool active)
    {
        if (active)
        {
            LoadScene(0);
            menuCanvas.SetActive(true);
            adventureCanvas.SetActive(false);
            if (player != null) player.SetActive(false);
            gameOverCanvas.SetActive(false);
            if (isBattle)
            {
                BattleState(false);
            }
        }
        else
        {
            menuCanvas.SetActive(false);
            adventureCanvas.SetActive(true);
            if (player == null) { StartGame(); }
            player.SetActive(true);
            if (isBattle)
            {
                BattleState(true);
            }
        }

    }

    /*public void AdventureState(bool active)
    {
        if (active)
        {
            isAdventure = true;
        }
        adventureCanvas.SetActive(active);
        player.SetActive(active);
    }*/

    public void BattleState(bool active)
    {
        isBattle = active;
        battleManager.SetActive(active);
        battleCanvas.SetActive(active);
    }
    /*
        public void SetState()
        {
            if (isMainMenu)
            {
                MenuState(true);
                AdventureState(false);
                BattleState(false);
            }
            else
            {
                if (isAdventure)
                {
                    MenuState(false);
                    AdventureState(true);
                    BattleState(false);
                }
                else
                {
                    if (isBattle)
                    {
                        MenuState(false);
                        AdventureState(true);
                        BattleState(true);
                    }
                }
            }
        }*/

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

    public void GameOverState()
    {
        gameOverCanvas.SetActive(true);
        Destroy(player);
        foreach(var card in CardDeck.Instance.cardsOnCardDeck)
        {
            Destroy(card);
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void NextGameTurn()
    {
        EventManager.Instance.ActiveRandomEvent();
    }
}

