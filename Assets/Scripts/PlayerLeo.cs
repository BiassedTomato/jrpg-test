using UnityEngine;
using UnityEditor;

public class PlayerLeo : PlayerBase
{
    public override void SetArmor()
    {
        Equipment["Head"] = new ArmorThickBand();
        Equipment["Body"] = new ArmorLeatherJacket();
        Equipment["Trinket"] = new ArmorSpikedCollar();
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
        skillList[1] = new CardTransfusion(turnHandler.PickedPlayer, this);
        skillList[2] = new CardWoodenShield(this);
        //skillList[3] = new CardIdle();
        //skillList[4] = new CardIdle();
        //skillList[5] = new CardIdle();
    }

}