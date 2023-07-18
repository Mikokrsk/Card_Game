using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static int s_health;
    public static int s_maxHealth = 100;
    public static bool s_isAlife = true;
    // Start is called before the first frame update
    void Start()
    {
        s_health = s_maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
