using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string name;
    [SerializeField] private Text nameText;
    public int health;
    public int maxHealth;
    [SerializeField] private Slider healthSlider;
    public int armor;
    public int maxArmor;
    [SerializeField] private Slider armorSlider;
    public int strength;
    public int blockingPower;
    public int minBlockingPower = 1;
    public bool isAlive;
    public Animator animator;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform cam;

    private void Awake()
    {
        SetEnemyAttributes();
        SetDefaultEnemyValues();
    }

    private void SetDefaultEnemyValues()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.forward);
        blockingPower = minBlockingPower;
        healthSlider.maxValue = health = maxHealth;
        healthSlider.value = health;
        armorSlider.maxValue = armor = maxArmor;
        armorSlider.value = armor;
        nameText.text = name;
    }

    private void SetEnemyAttributes()
    {
        canvas = gameObject.GetComponentInChildren<Canvas>();
        nameText = canvas.transform.Find("EnemyName").GetComponent<Text>();
        healthSlider = canvas.transform.Find("HealthSlider").GetComponent<Slider>();
        armorSlider = canvas.transform.Find("ArmorSlider").GetComponent<Slider>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.GetComponent<Transform>();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void UpdateHUD()
    {
        healthSlider.value = health;
        armorSlider.value = armor;
    }

}

