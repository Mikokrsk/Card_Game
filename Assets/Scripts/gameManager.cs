//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;
    //[SerializeField] private PlayerController playerController;
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
        data.health = PlayerController.Instance.health;
        data.maxHealth = PlayerController.Instance.maxHealth;
        data.armor = PlayerController.Instance.armor;
        data.maxArmor = PlayerController.Instance.maxArmor;
        data.strength = PlayerController.Instance.strength;
        data.agility = PlayerController.Instance.agility;
        data.intelligence = PlayerController.Instance.intelligence;
        data.endurance = PlayerController.Instance.endurance;
        data.money = PlayerController.Instance.money;
        data.experience = PlayerController.Instance.experience;
        data.blockingPower = PlayerController.Instance.blockingPower;
        data.isAlife = PlayerController.Instance.isAlife;
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

             PlayerController.Instance.health = data.health;
             PlayerController.Instance.maxHealth = data.maxHealth;
             PlayerController.Instance.armor = data.armor;
             PlayerController.Instance.maxArmor = data.armor;
             PlayerController.Instance.strength = data.strength;
             PlayerController.Instance.agility = data.agility;
             PlayerController.Instance.intelligence = data.intelligence;
             PlayerController.Instance.endurance = data.endurance;
             PlayerController.Instance.money = data.money;
             PlayerController.Instance.experience = data.experience;
             PlayerController.Instance.blockingPower = data.blockingPower;
             PlayerController.Instance.isAlife = data.isAlife;
             Debug.Log(Application.persistentDataPath);
        }
    }

}
