using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum BuffState { Nothing, Poison, Curse, Down }

[RequireComponent(typeof(BoxCollider2D))]
public abstract class EntityBase : MonoBehaviour
{
    public Dictionary<string, IArmor> Equipment = new Dictionary<string, IArmor>(3);

    int health, defence, frostDefence, physicDefence;

    public BuffState State;

    virtual public int Defence
    {
        get
        {
            return defence;
        }
        set
        {
            defence = Mathf.Clamp(value, 0, 99999);
        }
    }
    virtual public int FrostDefence
    {
        get
        {
            return frostDefence;
        }
        set
        {
            frostDefence = Mathf.Clamp(value, 0, 99999);
        }
    }
    virtual public int PhysicDefence
    {
        get
        {
            return physicDefence;
        }
        set
        {
            physicDefence = Mathf.Clamp(value, 0, 99999);
        }
    }
    virtual public int MaxHealth { get { return 100; } }

    protected TurnHandler turnHandler;

    private void Awake()
    {
        turnHandler = GameObject.Find("TurnBase").GetComponent<TurnHandler>();
    }

    void ResetDefence()
    {
        this.defence = startDefence;
    }




    virtual protected int startDefence { get { return 5; } }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
        }
    }

    public bool UpdateEntity()//TODO: добавить проверку хп, и бафчиков всяких
    {
        ResetDefence();

        switch (this.State)
        {
            case BuffState.Curse:
                {
                    OutputDebugger.Write("O chaos! It took your precious " + (Health * 0.15f).ToString());
                    this.Health -= Mathf.RoundToInt(Health * 0.15f);
                }
                break;

            case BuffState.Poison:
                {
                    this.Health -= 15;
                    OutputDebugger.Write("Ew.");
                }
                break;
        }

        if (Health <= 0)
        {
            if (this.tag == "Enemy")// TODO: временно 
                Destroy(this.gameObject);
            else
            {
                this.State = BuffState.Down;
                OutputDebugger.Write(name + " has been KO'd!");
            }

        }
        return true;
    }
}
