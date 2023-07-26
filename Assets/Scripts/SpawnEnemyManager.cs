using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnEnemies;

    private void FindEnemies()
    {
        spawnEnemies = new List<GameObject>();
        spawnEnemies.AddRange(Array.ConvertAll(Resources.LoadAll("Enemies", typeof(GameObject)), assets => (GameObject)assets));
    }
    public void SpawnRandomEnemy()
    {
        if (spawnEnemies.Count<=0)
        {
            FindEnemies();
        }
        var index = UnityEngine.Random.Range(0, spawnEnemies.Count);
        SpawnEnemy(index);
    }

    public void SpawnEnemy(int id)
    {
        Debug.Log($"Enemies Count = {spawnEnemies.Count} Spawn id = {id}");
        var enemy = Instantiate(spawnEnemies[id]);
        enemy.transform.SetParent(gameObject.transform, false);
        BattleManager.Instance.AddEnemyInList(enemy.GetComponent<Enemy>());
    }
}
