using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class CardBrain
{

    //Каждый объект карты должен в итоге выводить текст активации. Не забудь!

    //  protected static CameraShaker cam = GameObject.Find("Camera").GetComponent<CameraShaker>();

    protected EntityBase User;//поле для ссылки на отправителя
    protected GameObject Effect; //поле эффекта 
    protected string name;//поле, хранящее имя карты

    public abstract string Name { get; }//св-во имени
    public abstract string SkillName { get; }//св-во имени
    public abstract string EffectName { get; }//имя эффекта

    protected EntityBase Target;//поле ссылки на того, кто будет получать урон

    public void CastBuff(EntityBase target, BuffState buff)
    {
        if (target.State != BuffState.Down)
            target.State = buff;
    }

    public abstract void Activate(); //переопределяемый метод активации
    protected void Shake()
    {
        CameraShaker cam = GameObject.Find("Camera").GetComponent<CameraShaker>();
        cam.Shake();
    }
    protected void Hurt(EntityBase target, int damage, int defenceToConsider) //метод нанесения урона 
    {
        if (target.tag == "Enemy")
        {
            int cutDamage = Mathf.Clamp(damage - defenceToConsider, 1, 99999);
            target.Health -= cutDamage;
        }
        else
        {
            int cutDamage = Mathf.Clamp(damage - target.Equipment["Head"].PhysicalDefence, 1, 99999);
            target.Health -= cutDamage;
        }
    }
    protected void Hurt(EntityBase target, int damage) //метод нанесения урона (игнорирует броню)
    {
        if (target.tag == "Enemy")
        {
            int cutDamage = Mathf.Clamp(damage, 1, 99999);
            target.Health -= cutDamage;
        }
        else
        {
            int defence = 0;

            foreach (KeyValuePair<string, IArmor> armor in target.Equipment)
            {
                if (armor.Value != null)
                    defence += armor.Value.PhysicalDefence;
            }

            int cutDamage = Mathf.Clamp(damage - defence, 1, 99999);
            target.Health -= cutDamage;
        }
    }

    protected void HurtThroughArmor(EntityBase target, int damage)
    {
        target.Health -= damage;
    }

    protected void Heal(EntityBase target, int val) //метод лечения 
    {
        target.Health += val;
    }
    protected GameObject LoadEffect(string cardName, EntityBase target)//загрузка эффекта по имени
    {
        return GameObject.Instantiate(Resources.Load("Sprites/Effects/Prefabs/" + cardName) as GameObject, target.transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
    }
}

public class CardCursedPin : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_cursedpin";
        }
    } //имя

    override public string SkillName
    {
        get
        {
            return "CurPin";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "cleave_effect";
        }
    } //имя эффекта

    public CardCursedPin(EntityBase target)//конструктор установления таргета
    {
        Target = target;

    }

    public override void Activate() //активация
    {
        CastBuff(Target, BuffState.Curse);

        Shake();

        Hurt(Target, 30);

        Effect = LoadEffect(EffectName, Target);
        OutputDebugger.Write("Ouch! Enemy cursed you!");
    }
}
public class CardDummyPunch : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_punch";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "star_effect";
        }
    } //имя эффекта

    override public string SkillName
    {
        get
        {
            return "DPunch";
        }
    } //имя

    public CardDummyPunch(EntityBase target)//конструктор установления таргета
    {
        Target = target;

    }

    public override void Activate() //активация
    {
        Hurt(Target, 5);
        SpawnStarEffect(Target);
        Shake();
        OutputDebugger.Write("Knock! Enemy stroke you with its wooden arm!");
    }

    void SpawnStarEffect(EntityBase target) //спаун эффекта
    {
        for (int i = 0; i < 4; i++)
        {
            Effect = LoadEffect(EffectName, Target);
            GameObject.Destroy(Effect, 3f);
        }
    }
}
public class CardHealingRelic : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_healingrelic";
        }
    } //имя 
    override public string EffectName
    {
        get
        {
            return "star_effect";
        }
    } //имя эффекта

    override public string SkillName
    {
        get
        {
            return "HealRelic";
        }
    } //имя

    public CardHealingRelic(EntityBase target)//конструктор
    {
        Target = target;

    }

    public override void Activate() // активация
    {
        if (Target.State != BuffState.Down)
        {
            Heal(Target, Mathf.RoundToInt(Target.MaxHealth * 0.15f));// Хил 15% (при 100 хп это 15)
            if (Target.State != BuffState.Nothing)
            {
                OutputDebugger.Write("Relic healed your ailment!");
                Target.State = BuffState.Nothing;
            }
        }
        else
        {
            Heal(Target, Mathf.RoundToInt(Target.MaxHealth * 0.15f));// Хил 15% (при 100 хп это 15)
            Target.State = BuffState.Nothing;
            OutputDebugger.Write(string.Format("Relic revived fallen {0} !", Target.name));
        }

        Effect = LoadEffect(EffectName, Target);
        GameObject.Destroy(Effect, 3f);
        OutputDebugger.Write("гг отхилился!");
    }
}
public class CardIdle : CardBrain
{
    override public string Name { get { return "card_idle"; } }//имя
    override public string EffectName
    {
        get
        {
            return "";
        }
    }
    override public string SkillName
    {
        get
        {
            return "Skip";
        }
    } //имя
    public override void Activate()//активация карты айдла
    {
        OutputDebugger.Write("Skipped turn");
    }
}
public class CardTestSword : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_sword_light";
        }
    } //имя 
    override public string EffectName
    {
        get
        {
            return "cleave_effect";
        }
    } //имя эффекта

    override public string SkillName
    {
        get
        {
            return "MetalKnife";
        }
    } //имя

    public CardTestSword(EntityBase target)//конструктор
    {
        Target = target;
    }

    public override void Activate() //активация
    {
        Hurt(Target, 20, Target.PhysicDefence);
        Effect = LoadEffect(EffectName, Target);
        Target.GetComponent<ParticleSystem>().Play();
        Shake();

        GameObject.Destroy(Effect, 3f);
        OutputDebugger.Write("огреб!");
    }
}
public class CardVampireBite : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_bite";
        }
    } //имя 

    override public string SkillName
    {
        get
        {
            return "VamBite";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "cleave_effect";
        }
    } //имя эффекта

    public CardVampireBite(EntityBase target, EntityBase sender)//конструктор (тут есть помимо врага еще и тот, кто его использует)
    {
        Target = target;
        User = sender;
    }

    public override void Activate() //активация
    {
        Hurt(Target, 9);
        Heal(User, 5);

        SpawnCleaveEffect(Target);


        OutputDebugger.Write("Hss! Enemy bit you!");
    }

    void SpawnCleaveEffect(EntityBase target)//спаун эффекта
    {
        for (int i = 0; i < 2; i++)
        {
            Effect = LoadEffect(EffectName, Target);
            Effect.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
            GameObject.Destroy(Effect, 3f);
        }
    }
}
public class CardVoodooShot : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_shot";
        }
    } //имя 

    override public string SkillName
    {
        get
        {
            return "VoodooShot";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    public CardVoodooShot(EntityBase target)//конструктор
    {
        Target = target;
    }

    public override void Activate() //активация
    {
        GameObject.Destroy(Effect, 3f);
        OutputDebugger.Write("Photo has been binded!");
    }
}
public class CardWoodenShield : CardBrain
{
    EntityBase player;
    override public string Name
    {
        get
        {
            return "card_defence";
        }
    } //имя 

    override public string SkillName
    {
        get
        {
            return "WShield";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    public CardWoodenShield(EntityBase entityToDefend)//конструктор
    {
        player = entityToDefend;
    }

    public override void Activate() //активация
    {
        player.Defence = Mathf.RoundToInt(player.Defence * 1.8f);
        OutputDebugger.Write(player.name+ "'s defence has been increased!");
    }
}
public class CardFoulSmell : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_template";
        }
    } //имя 

    override public string SkillName
    {
        get
        {
            return "FoulSmell";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    public CardFoulSmell(EntityBase target)//конструктор
    {
        Target = target;
    }

    public override void Activate() //активация
    {
        CastBuff(Target, BuffState.Poison);
        Target.Defence = Mathf.RoundToInt(Target.Defence * .5f);

        OutputDebugger.Write(string.Format("Eww.. {0} is poisoned!", Target.name));
    }
}
public class CardFrostSpell : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_coldspell";
        }
    } //имя 



    override public string SkillName
    {
        get
        {
            return "FrostTouch";
        }
    } //имя

    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    public CardFrostSpell(EntityBase target)//конструктор
    {
        Target = target;
    }

    public override void Activate() //активация
    {
        Hurt(Target, 20, Target.FrostDefence);

        OutputDebugger.Write("Frost!");
    }
}
public class CardBoosterShot : CardBrain
{
    override public string Name
    {
        get
        {
            return "card_template";
        }
    } //имя 
    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    override public string SkillName
    {
        get
        {
            return "BoosterShot";
        }
    } //имя

    public CardBoosterShot(EntityBase target)//конструктор
    {
        Target = target;

    }

    public override void Activate() // активация
    {
        if (Target.State == BuffState.Down)
        {
            Heal(Target, Mathf.RoundToInt(Target.MaxHealth * 0.15f));
            Target.State = BuffState.Nothing;

            OutputDebugger.Write("Booster revives "+ Target.name+"!" );
        }
    }
}
public class CardTransfusion : CardBrain
{
    override public string Name
    {
        get
        {
            return "";
        }
    } //имя 
    override public string EffectName
    {
        get
        {
            return "";
        }
    } //имя эффекта

    override public string SkillName
    {
        get
        {
            return "Transfusion";
        }
    } //имя

    public CardTransfusion(EntityBase target, EntityBase user)//конструктор
    {
        Target = target;
        User = user;
    }

    public override void Activate() // активация
    {
        if (Target.State == BuffState.Down)
        {
            int health = Mathf.RoundToInt(User.Health / 2);

            Heal(Target, health);
            HurtThroughArmor(User, health);

            Target.State = BuffState.Nothing;

            OutputDebugger.Write(User.name+" donates their blood to "+Target.name+"!");
        }
    }
}

