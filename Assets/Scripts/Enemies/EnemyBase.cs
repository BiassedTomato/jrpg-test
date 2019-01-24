using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyBase : EntityBase
{
    public bool IsBlocked()
    {
        return true; //TODO: сделать блок урона
    }

    protected Text log;
    protected EntityBase Player;

    protected CardBrain[] Skills = new CardBrain[10]; //навыки

    protected VirtualDeck Deck;//колода, куда кидаются карты

    private void OnMouseUpAsButton()
    {
        if (turnHandler.playerTurn)// выбор
        {
            if (turnHandler.PickedEnemy != this)
            {
                turnHandler.PickedEnemy = this;
                OutputDebugger.UpdateDisplayedEnemyStats(this);
            }

        }
    }

    private void OnEnable()
    {
        turnHandler = GameObject.Find("TurnBase").GetComponent<TurnHandler>();

        log = GameObject.Find("DebugTurnOutput").GetComponent<Text>();
        Deck = GameObject.Find("Deck").GetComponent<VirtualDeck>();
        Player = turnHandler.PlayerParty[0];
    }

    IEnumerator EnemyTurn()
    {
        //TODO: логика. Возможно, дополнить
        Act();
        yield return 0f;
    }

    protected virtual void Act()
    {
        Debug.Log("You forgot an implementation!");
    } //на случай, если забыл сделать act
}
