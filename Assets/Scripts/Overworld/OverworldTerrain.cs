using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class OverworldTerrain : MonoBehaviour
{
    protected FightSwitcher FightSwitch;
    protected Transform Overworld;

    public string[] Pattern;

    //void SetComponents()
    //{
    //    Debug.Log("Components were set for " + name);
    //}

    private void OnEnable()
    {
        Overworld = GameObject.Find("Overworld").transform;
        FightSwitch = GameObject.Find("FightSwitcher").GetComponent<FightSwitcher>();

        if (Pattern.Length < 1)
        {
            throw new System.Exception(string.Format("ты забыл добавить {0} врагов!", name));
        }

    }



    private void OnMouseUpAsButton()
    {
        StartFight();
    }

    public void StartFight()
    {
        FightSwitch.StartFight(Pattern);
        this.gameObject.SetActive(false);

        Overworld.gameObject.SetActive(false);
    }

}
