using UnityEngine;
using UnityEditor;

public class PlayerShell : PlayerBase
{
    public override void SetArmor()
    {
        Equipment["Head"] = null;
        Equipment["Body"] = null;
        Equipment["Trinket"] = null;


    }
    public override int MaxHealth
    {
        get
        {
            return 100;
        }
    }

    public override void UpdateSkills(EntityBase enemy, EntityBase player)
    {
        skillList[0] = new CardTestSword(enemy);
        skillList[1] = new CardHealingRelic(player);
        skillList[2] = new CardWoodenShield(this);
        //skillList[3] = new CardIdle();
        //skillList[4] = new CardIdle();
        //skillList[5] = new CardIdle();
    }

}