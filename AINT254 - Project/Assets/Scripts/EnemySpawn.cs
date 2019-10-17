using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int xPos;
    public int zPos;
    public int enemyCount;

    public int amountOfEnemiesToSpawn;

    void Start()
    {
        StartCoroutine(SpawnTheEnemy());
    }

    IEnumerator SpawnTheEnemy()
    {
        while(enemyCount < amountOfEnemiesToSpawn)
        {
            xPos = Random.Range(-40, 40);
            zPos = Random.Range(-91, -60);
            Instantiate(prefabToSpawn, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(.5f);

            enemyCount += 1;

            GameUI.instance.enemiesText.text = "Amount of enemies Spawned " + enemyCount;
        }
    }

}
