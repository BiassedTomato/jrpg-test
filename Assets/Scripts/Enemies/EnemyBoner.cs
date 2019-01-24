using UnityEngine;
using System.Collections;

public class EnemyBoner : EnemyBase
{
    public EntityBase Target;

    int bindCounter = 0;

    CardBrain pickedSkill;
    CardCursedPin pin;//TODO: дополнить -- набор скиллов для врага
    CardVoodooShot bindEnemy;
    CardFoulSmell breath;
    //CardTestSword swordLight;
    //CardDummyPunch punch;
    //CardVampireBite bite;


    public override int MaxHealth //св во для макс здоровья
    {
        get
        {
            return 200;
        }
    }

    protected override void Act() //переопределенный act
    {

        if (bindCounter > 0)// TODO: возможно, почистить
        {
            bindCounter--;
            if (bindCounter == 0)
                OutputDebugger.Write("The photo fades. Binding has been lifted!");
            pickedSkill = new CardCursedPin(turnHandler.PlayerParty[0]);
        }

        else
        {
            pickedSkill = new CardVoodooShot(turnHandler.PlayerParty[0]);
            bindCounter = 3;
        }

        //pickedSkill = new CardFoulSmell(turnHandler.PlayerParty[0]);// TODO: убрать

        Deck.PutCard(pickedSkill);
    }
}
