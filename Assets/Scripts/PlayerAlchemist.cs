using UnityEngine;
using UnityEditor;

public class PlayerAlchemist : PlayerBase
{
    public override void SetArmor()
    {
        Equipment["Head"] = new ArmorWaifCap();
        Equipment["Body"] = new ArmorFrayedShirt();
        Equipment["Trinket"] = new ArmorLuckyPendant();
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
        skillList[1] = new CardFrostSpell(enemy);
        skillList[2] = new CardWoodenShield(this);
        //skillList[3] = new CardVampireBite(enemy, player);
        //skillList[4] = new CardWoodenShield(player);
        //skillList[5] = new CardHealingRelic(player);
    }

}