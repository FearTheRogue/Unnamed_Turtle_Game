using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int xPos;
    public int zPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(SpawnTheEnemy());
    }

    IEnumerator SpawnTheEnemy()
    {
        while(enemyCount < 1)
        {
            xPos = Random.Range(-40, 40);
            zPos = Random.Range(-91, -60);
            Instantiate(prefabToSpawn, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(.5f);

            enemyCount += 1;
        }
    }

}
