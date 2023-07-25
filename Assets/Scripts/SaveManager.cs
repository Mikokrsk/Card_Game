using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string savePlayerFileName;
    private string savePath;
    public static SaveManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        savePlayerFileName = "/savePlayerFile.json";
        savePath = Application.persistentDataPath;
    }

    [System.Serializable]
    class SavePlayerDataClass
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

    public void SavePlayerData()
    {
        SavePlayerDataClass data = new SavePlayerDataClass();
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
        File.WriteAllText(savePath+savePlayerFileName, json);
       // File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = savePath + savePlayerFileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavePlayerDataClass data = JsonUtility.FromJson<SavePlayerDataClass>(json);

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
