using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OutputDebugger : MonoBehaviour
{
    static Text debugLog = GameObject.Find("DebugTurnOutput").GetComponent<Text>();

    static Text PlayerNameDisplay = GameObject.Find("NameDisplay").GetComponent<Text>();
    static Text PlayerHealthDisplay = GameObject.Find("HealthDisplay").GetComponent<Text>();
    static Text PlayerAilmentDisplay = GameObject.Find("AilmentDisplay").GetComponent<Text>();
    static Text PlayerArmorDisplay = GameObject.Find("ArmorDisplay").GetComponent<Text>();

    static Text EnemyNameDisplay = GameObject.Find("EnemyNameDisplay").GetComponent<Text>();
    static Text EnemyHealthDisplay = GameObject.Find("EnemyHealthDisplay").GetComponent<Text>();
    static Text EnemyAilmentDisplay = GameObject.Find("EnemyAilmentDisplay").GetComponent<Text>();

    static TurnHandler turnHandler = GameObject.Find("TurnBase").GetComponent<TurnHandler>();

    public static void Write(string Message)
    {
        debugLog.text += Message + "\n";
    }

    public static void PutCard(string name, int index)
    {
        GameObject card = Instantiate(Resources.Load("Sprites/cards/Prefabs/card_template"), turnHandler.Deck.transform) as GameObject;
        card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/cards/" + name);

        card.transform.localPosition = new Vector3(Random.Range(0.13f, 0.15f) * (index + 1), index * Random.Range(-0.1f, 0.1f), 0);
        card.GetComponent<SpriteRenderer>().sortingOrder = index;
    }

    public static void UpdateDisplayedStats(EntityBase pickedPlayer)
    {
        string armorText = "";

        PlayerNameDisplay.text = pickedPlayer.name;
        PlayerHealthDisplay.text = pickedPlayer.Health.ToString() + " hp";

        foreach (KeyValuePair<string, IArmor> armor in pickedPlayer.Equipment)
        {
            if (armor.Value != null)
                armorText += armor.Value.Name + "\n";
        }

        if (armorText == "")
            armorText = "None";

        PlayerArmorDisplay.text = armorText;


        switch (pickedPlayer.State)
        {
            case BuffState.Curse:
                PlayerAilmentDisplay.text = "Cursed"; break;

            case BuffState.Down:
                PlayerAilmentDisplay.text = "Blacked out"; break;

            case BuffState.Poison:
                PlayerAilmentDisplay.text = "Poisoned"; break;

            case BuffState.Nothing:
                PlayerAilmentDisplay.text = "No ailment"; break;
        }

    }

    public static void UpdateDisplayedEnemyStats(EntityBase pickedEnemy)
    {
        EnemyNameDisplay.text = pickedEnemy.name;
        EnemyHealthDisplay.text = pickedEnemy.Health.ToString() + " hp";
        switch (pickedEnemy.State)
        {
            case BuffState.Curse:
                EnemyAilmentDisplay.text = "Cursed"; break;

            case BuffState.Down:
                EnemyAilmentDisplay.text = "Blacked out"; break;

            case BuffState.Poison:
                EnemyAilmentDisplay.text = "Poisoned"; break;

            case BuffState.Nothing:
                EnemyAilmentDisplay.text = "No ailment"; break;
        }

    }

}
