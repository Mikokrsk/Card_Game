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
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    [System.Serializable]
    class SaveDataClass
    {
        public int health;
        public int maxHealth;
        public int armor;
        public int maxArmor;
        public int strength;
        public int agility;
        public int intelligence;
        public int endurance;
        public int money;
        public int experience;
        public int blockingPower;
        public bool isAlife;
    }

    public void SaveData()
    {
        SaveDataClass data = new SaveDataClass();
        data.health = Player.Instance.health;
        data.maxHealth = Player.Instance.maxHealth;
        data.armor = Player.Instance.armor;
        data.maxArmor = Player.Instance.maxArmor;
        data.strength = Player.Instance.strength;
        data.agility = Player.Instance.agility;
        data.intelligence = Player.Instance.intelligence;
        data.endurance = Player.Instance.endurance;
        data.money = Player.Instance.money;
        data.experience = Player.Instance.experience;
        data.blockingPower = Player.Instance.blockingPower;
        data.isAlife = Player.Instance.isAlive;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        //Debug.Log($"Save Health = {data.health} MaxHealth = {data.maxHealth}");
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);

             Player.Instance.health = data.health;
             Player.Instance.maxHealth = data.maxHealth;
             Player.Instance.armor = data.armor;
             Player.Instance.maxArmor = data.maxArmor;
             Player.Instance.strength = data.strength;
             Player.Instance.agility = data.agility;
             Player.Instance.intelligence = data.intelligence;
             Player.Instance.endurance = data.endurance;
             Player.Instance.money = data.money;
             Player.Instance.experience = data.experience;
             Player.Instance.blockingPower = data.blockingPower;
             Player.Instance.isAlive = data.isAlife;
             Debug.Log(Application.persistentDataPath);
        }
        Player.Instance.UpdateHUD();
    }

}
