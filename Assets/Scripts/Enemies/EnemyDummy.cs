using UnityEngine;
using System.Collections;

public class EnemyDummy : EnemyBase
{
    EnemyDummy()
    {
        FrostDefence = 0;
        PhysicDefence = 0;
    }

    public override int MaxHealth //св во для макс здоровья
    {
        get
        {
            return 15;
        }
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
        Deck.PutCard(new CardDummyPunch(turnHandler.PlayerParty[Random.Range(0,3)]));
    }
}
