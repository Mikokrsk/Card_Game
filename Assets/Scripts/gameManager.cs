//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class gameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

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
        data.health = playerController.s_health;
        data.maxHealth = playerController.s_maxHealth;
        data.armor = playerController.s_armor;
        data.maxArmor = playerController.s_maxArmor;
        data.strength = playerController.s_strength;
        data.agility = playerController.s_agility;
        data.intelligence = playerController.s_intelligence;
        data.endurance = playerController.s_endurance;
        data.money = playerController.s_money;
        data.experience = playerController.s_experience;
        data.blockingPower = playerController.s_blockingPower;
        data.isAlife = playerController.s_isAlife;

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

            playerController.s_health = data.health;
            playerController.s_maxHealth = data.maxHealth;
            playerController.s_armor = data.armor;
            playerController.s_maxArmor = data.armor;
            playerController.s_strength = data.strength;
            playerController.s_agility = data.agility;
            playerController.s_intelligence = data.intelligence;
            playerController.s_endurance = data.endurance;
            playerController.s_money = data.money;
            playerController.s_experience = data.experience;
            playerController.s_blockingPower = data.blockingPower;
            playerController.s_isAlife = data.isAlife;
            Debug.Log(Application.persistentDataPath);
           // Debug.Log($"Load1 Health = {data.health} MaxHealth = {data.maxHealth}");
          //  Debug.Log($"Load1 Health = {PlayerController.s_health} MaxHealth = {PlayerController.s_maxHealth}");
        }
    }
    /*  public void SaveColor()
      {
          SaveData data = new SaveData();
          data.TeamColor = TeamColor;

          string json = JsonUtility.ToJson(data);
          File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
      }

      public void LoadColor()
      {
          string path = Application.persistentDataPath + "/savefile.json";
          if (File.Exists(path))
          {
              string json = File.ReadAllText(path);
              SaveData data = JsonUtility.FromJson<SaveData>(json);

              TeamColor = data.TeamColor;
          }

      }*/
}
