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
    private GameObject BattleCanvas;
    private GameObject AdventureCanvas;
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
        SaveManager.Instance.LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.battle)
        {
            BattleState(true);
        }
    }

    private void MenuState(bool active)
    {
        if (active)
        {
            //Menu activate
        }
        else
        {
            AdventureState(!active);
            BattleState(!active);
        }

    }

    private void AdventureState(bool active)
    {
        AdventureCanvas.SetActive(active);

    }

    private void BattleState(bool active)
    {
        if (active)
        {
            AdventureState(true);
        }
        BattleManager.Instance.gameObject.SetActive(active);
        BattleCanvas.SetActive(active);
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
