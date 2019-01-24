using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class PlayerBase : EntityBase
{
    public EntityBase PickedPlayer;

    public CardBrain[] skillList = new CardBrain[6];

    private void OnEnable()
    {
        PickedPlayer = this;
        turnHandler = GameObject.Find("TurnBase").GetComponent<TurnHandler>();
    }

    public abstract void SetArmor();

    override public int Defence
    {
        get
        {
            return 5;// TODO: убрать
        }

        set
        {
            base.Defence = Mathf.Clamp(value, 5, 99999);
        }
    }

    private void OnMouseUpAsButton()
    {
        if (turnHandler.playerTurn)//варианты выбора
        {
            if (turnHandler.PickedPlayer != this)
            {
                turnHandler.PickedPlayer = this;
                OutputDebugger.UpdateDisplayedStats(this);
            }
        }
    }


    public virtual void UpdateSkills(EntityBase enemy, EntityBase player)
    {
        skillList[0] = new CardDummyPunch(enemy);
        skillList[1] = new CardIdle();
        skillList[2] = new CardTestSword(enemy);
        skillList[3] = new CardVampireBite(enemy, player);
        skillList[4] = new CardWoodenShield(player);
        skillList[5] = new CardHealingRelic(player);
    }

}
