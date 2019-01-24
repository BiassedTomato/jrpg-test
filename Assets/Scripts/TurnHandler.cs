using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Collections;

public class TurnHandler : MonoBehaviour
{
    int skillIndex = 0;

    public FightSwitcher Switch;

    Text[] skillButtons = new Text[6];
    public Text HealthText;
    public Text log;

    Transform crosshair;

    Transform playerCrosshair;

    public EntityBase PickedEnemy;
    public EntityBase PickedPlayer;

    public PlayerBase[] PlayerParty;
    PlayerBase currentPlayer;

    public CardBrain PickedCard;

    public EnemyGroupHandler EnemyHandler;

    public VirtualDeck Deck;

    public bool playerTurn = true;

    public void PickEnemy(EntityBase enemy)
    {
        PickedEnemy = enemy;
    }

    public void StartFight()
    {
        // TODO: почистить

        // Debug.Log("fight's on!");

        InitializeStart();
       // SetPlayerPartyHealth();
        SetPlayerArmor();

        // StartCoroutine("PlayerTurn"); //Должно быть ПОСЛЕДНИМ
        StartCoroutine("PlayerTurn");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Awake()
    {
        SetPlayerPartyHealth();
    }

    IEnumerator PlayerTurn()
    {
        playerTurn = true;

        foreach (PlayerBase player in PlayerParty)
            if (player.State != BuffState.Down)
            {
                player.UpdateSkills(PickedEnemy, PickedPlayer);
                currentPlayer = player;

                if (PickedEnemy == null)
                    PickedEnemy = EnemyHandler.enemyGroup[0];

                try
                {
                    crosshair.position = PickedEnemy.transform.position;
                    UpdateInfo();

                }
                catch (System.NullReferenceException )
                {
                    StreamWriter SW = File.CreateText("C:/Users/admin/Desktop/хрякрпг/GameCrashLog_1.dat");
                    SW.WriteLine("Yee");
                    SW.Close();

                    Switch.EndFight();// TODO: убрать эксепшн
                }

                //UpdateInfo();

                UpdateNames(player);

                OutputDebugger.Write("It's yout turn!");

                yield return new WaitWhile(TurnProcess);

                player.UpdateSkills(PickedEnemy, PickedPlayer);

                player.PickedPlayer = PickedPlayer;

                PickedCard = player.skillList[skillIndex];

                if (PickedCard == null)
                    PickedCard = new CardIdle();

                Deck.PutCard(PickedCard);

                InvertTurn();
            }

        yield return new WaitForSeconds(1);

        EnemyHandler.StartCoroutine("EnemyTurn");
    }

    public bool TurnProcess()
    {
        crosshair.position = PickedEnemy.transform.position;
        playerCrosshair.position = PickedPlayer.transform.position;
        return playerTurn;
    }

    private void Update()
    {
        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SetSkill(0);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SetSkill(1);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                SetSkill(2);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                SetSkill(3);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                SetSkill(4);
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                SetSkill(5);
        }
    }

    IEnumerator TussleResult() // Результат раунда
    {
        //TODO: имплементация расчета того, как взаимодействуют карты

        foreach (CardBrain card in Deck.virtualDeck) // Активация карт
        {
            if (card != null)
            {
                card.Activate();
                yield return new WaitForSeconds(0.3f);
            }
        }

        yield return new WaitUntil(UpdateState);// TODO: разобраться в причинах (он начинает обновлять список врагов быстрее, чем проверяет состояние для них)
        yield return new WaitUntil(EnemyHandler.UpdateEnemies);

        Deck.ResetDeck();
        //  UpdateInfo();

        StartCoroutine("PlayerTurn");
    }

    bool UpdateState()
    {
        foreach (EntityBase player in PlayerParty)
            player.UpdateEntity();

        foreach (EntityBase enemy in EnemyHandler.enemyGroup)
            if (enemy != null)
                enemy.UpdateEntity();
        return true;
    }



    public void SetSkill(int num)
    {
        PickedCard = currentPlayer.skillList[num];
        skillIndex = num;
        InvertTurn();
    }

    void SetHealth(EntityBase entity)
    {
        entity.Health = entity.MaxHealth;
    }

    void SetHealth(EntityBase entity, int hp)
    {
        entity.Health = hp;
    }

    public void UpdateInfo()
    {
        OutputDebugger.UpdateDisplayedStats(PickedPlayer);
        OutputDebugger.UpdateDisplayedEnemyStats(PickedEnemy);
    }

    public void InvertTurn()
    {
        playerTurn = !playerTurn;
    }

    void InitializeStart()
    {
        log = GameObject.Find("DebugTurnOutput").GetComponent<Text>(); // Текстовое окно

        crosshair = GameObject.Find("cross").transform; // Перекрестие
        playerCrosshair = GameObject.Find("plcross").transform;

        Deck = GameObject.Find("Deck").GetComponent<VirtualDeck>(); //Виртуальная колода

        //EnemyHandler = GameObject.Find("EnemyGroupHandler").GetComponent<EnemyGroupHandler>(); //Враги

        for (int i = 0; i < 6; i++)
        {
            skillButtons[i] = GameObject.Find((i + 1).ToString()).GetComponentInChildren<Text>();
        }

        foreach (EntityBase enemy in EnemyHandler.enemyGroup) // Установка здоровья врагов
            if (enemy != null)
                SetHealth(enemy);
        PickedPlayer = PlayerParty[0];
    }

    void UpdateNames(PlayerBase player)
    {
        for (int i = 0; i < 6; i++)
        {
            if (player.skillList[i] != null)
                skillButtons[i].text = player.skillList[i].SkillName;
            else skillButtons[i].text = "None";
        }
    }

    void SetPlayerPartyHealth()
    {
        foreach (PlayerBase player in PlayerParty)
        {
            player.Health = player.MaxHealth;
            player.State = BuffState.Nothing;
        }
    }
    void SetPlayerArmor()
    {
        foreach (PlayerBase player in PlayerParty)
        {
            //player.Equipment.Add("Head", null);
            //player.Equipment.Add("Body", null);
            //player.Equipment.Add("Trinket", null);

            player.SetArmor();
        }

    }
}
