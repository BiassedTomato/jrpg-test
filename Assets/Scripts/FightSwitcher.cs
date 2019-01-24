using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EnemyPattern
{

    public EnemyPattern(params string[] givenPattern)
    {
        Pattern = givenPattern;
    }
    public string[] Pattern;

}



public class FightSwitcher : MonoBehaviour
{
    public GameObject Overworld;
    public TurnHandler TurnHandler;
    public EnemyGroupHandler enemyHandler;
    public GameObject Canvas, Battlefield;

    public void StartFight(params string[] enemyPattern)
    {
        Activate();

        int index = 0;// TODO: поменять на 0

        enemyHandler.SetPoints();

        foreach (string e in enemyPattern)
        {
            Transform enemy = Instantiate(Resources.Load("prefabs/Enemies/" + e) as GameObject).transform;

            enemy.position = enemyHandler.positionPoints[index].transform.position;

            index++;
        }

        enemyHandler.UpdateEnemies();
        TurnHandler.PickEnemy(enemyHandler.enemyGroup[0]);

        TurnHandler.StartFight();
    }

    public void EndFight()
    {
        foreach(PlayerBase player in TurnHandler.PlayerParty)
        {
            if (player.Health == 0)
                player.Health = 1;

            player.State = BuffState.Nothing;
        }

        Deactivate();

        TurnHandler.StopAllCoroutines();
    }

    public void Deactivate()
    {
        foreach (EntityBase enemy in enemyHandler.enemyGroup)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }

        Canvas.SetActive(false);
        Battlefield.SetActive(false);
        Overworld.SetActive(true);


    }
    public void Activate()
    {
        Canvas.SetActive(true);
        Battlefield.SetActive(true);
        Overworld.SetActive(false);
    }

    private void Update()
    {
        if (!Battlefield.activeSelf)
            if (Input.GetKeyDown(KeyCode.L))// TODO: строка активации
            {
                StartFight(new EnemyPattern("Dummy", "Lemon Demon", "Logan").Pattern);
            }
    }
}