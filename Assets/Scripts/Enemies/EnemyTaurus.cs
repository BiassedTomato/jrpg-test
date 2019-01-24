using UnityEngine;
using System.Collections;

public class EnemyTaurus : EnemyBase
{
    public override int MaxHealth //св во для макс здоровья
    {
        get
        {
            return 150;
        }
    }

    EnemyTaurus()
    {
        FrostDefence = 0;
        PhysicDefence = 25;
    }

    public override int PhysicDefence
    {
        get
        {
            return base.PhysicDefence;
        }

        set
        {
            base.PhysicDefence = value;
        }
    }

    public override int FrostDefence
    {
        get
        {
            return base.FrostDefence;
        }

        set
        {
            base.FrostDefence = value;
        }
    }

    protected override void Act() //переопределенный act
    {
        Deck.PutCard(new CardFoulSmell(turnHandler.PlayerParty[Random.Range(0,3)]));
    }
}
