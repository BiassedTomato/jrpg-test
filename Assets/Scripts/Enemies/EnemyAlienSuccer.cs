using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAlienSuccer : EnemyBase
{
    bool state=false;
    // TODO: Тут стоит поработать с этим 
    CardTestSword swordLight;
    CardDummyPunch punch;
    CardVampireBite succ;

    CardBrain currentSkill;

    public override int MaxHealth //св-во для макс здоровья
    {
        get
        {
            return 120;
        }
    }

    protected override void Act() //переопределенный act
    {
        punch = new CardDummyPunch(turnHandler.PlayerParty[0]);
        succ = new CardVampireBite(turnHandler.PlayerParty[0], this);

        if (!state)
            currentSkill = punch;
        else currentSkill = succ;

        state = !state;

        Deck.PutCard(currentSkill);
    }
}
