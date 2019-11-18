using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    public GameObject enemyExplosion;

    public void Die()
    {
        Instantiate(enemyExplosion, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
