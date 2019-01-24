using System;
using UnityEngine;
using System.Collections;

public class EnemyGroupHandler : MonoBehaviour
{
    public GameObject[] positionPoints;

    public TurnHandler TurnHandler;
    //GameObject[] enemies = new GameObject[5];

    public EntityBase[] enemyGroup = new EntityBase[5];

    public void SetPoints()
    {
        positionPoints = GameObject.FindGameObjectsWithTag("EnemyPosition");
    }

    public void AddEnemies()
    {
        //TODO: добавление врагов
    }

    public bool UpdateEnemies() //обновить список врагов на поле
    {

        // Array.Clear(enemies, 0, enemies.Length);
        Array.Clear(enemyGroup, 0, enemyGroup.Length);

        int i = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemyGroup[i] = enemy.GetComponent<EntityBase>();

            i++;
        }

        return true;
    }

    IEnumerator EnemyTurn()
    {
        foreach (EntityBase enemy in enemyGroup)
        {
            if (enemy != null)
            {
                enemy.StartCoroutine("EnemyTurn");
                yield return new WaitForSeconds(0.25f);
            }
        }

        TurnHandler.InvertTurn();
        TurnHandler.StartCoroutine("TussleResult");
    }
}
