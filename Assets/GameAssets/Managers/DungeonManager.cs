using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class DungeonManager : MonoBehaviour
{

    public DungeonRow dungeonRow1;
    public DungeonRow dungeonRow2;
    public DungeonRow dungeonRow3;
    public Dungeon currentDungeon;

    public GameObject EnemyBase;
    public GameObject PlayerBase;

    private RunManager _runManager;
    private DiContainer _container;

    public FloatyNumber.Factory _floatyNumberFactory;

    [Inject]
    public void Construct(RunManager runManager, DiContainer container, FloatyNumber.Factory floatyNumberFactory)
    {
        _runManager = runManager;
        _floatyNumberFactory = floatyNumberFactory;
        _container = container;
    }
    

    void Start()
    {
        _runManager.startRoundEvent.AddListener(StartRound);
    }

    void Update()
    {
    }

    public void StartRound()
    {
        foreach (DungeonRow row in new DungeonRow[] { dungeonRow1, dungeonRow2, dungeonRow3 })
        {
            foreach (Transform tileTransform in row.transform)
            {
                Tile tile = tileTransform.GetComponent<Tile>();
                if (tile != null && tile.monster != null)
                {
                    Destroy(tile.monster.gameObject);
                    tile.monster = null;
                }
            } 
        }

        //TODO adjust health based on dungeon level
        PlayerBase.GetComponent<PlayerBase>().MaxHealth = 200;
        EnemyBase.GetComponent<EnemyBase>().MaxHealth = 200;
        PlayerBase.GetComponent<PlayerBase>().Health = PlayerBase.GetComponent<PlayerBase>().MaxHealth;
        EnemyBase.GetComponent<EnemyBase>().Health = EnemyBase.GetComponent<EnemyBase>().MaxHealth;

        currentDungeon = _container.Instantiate<Dungeon>();
        currentDungeon.Initialize(_runManager.currentDungeonLevel, _runManager.roundNumber);
    }

    public void DamageEnemyBase(int damage)
    {
        EnemyBase.GetComponent<EnemyBase>().Health -= damage;
        FloatyNumber floatyNumber = _floatyNumberFactory.Create();
        floatyNumber.Initialize(damage, EnemyBase.transform.position, true);

        if (EnemyBase.GetComponent<EnemyBase>().Health <= 0)
        {
            _runManager.EndRound();
        }
    }

    public void DamagePlayerBase(int damage)
    {
        PlayerBase.GetComponent<PlayerBase>().Health -= damage;
        FloatyNumber floatyNumber = _floatyNumberFactory.Create();
        floatyNumber.Initialize(damage, PlayerBase.transform.position, true);

        if (PlayerBase.GetComponent<PlayerBase>().Health <= 0)
        {
            _runManager.EndRoundLose();
        }
    }

    public List<Monster> GetAllAllies()
    {
        List<Monster> allies = new List<Monster>();
        foreach (DungeonRow row in new DungeonRow[] { dungeonRow1, dungeonRow2, dungeonRow3 })
        {
            foreach (Transform tileTransform in row.transform)
            {
                Tile tile = tileTransform.GetComponent<Tile>();
                if (tile != null && tile.monster != null && tile.monster.team == "Ally")
                {
                    allies.Add(tile.monster);
                }
            }
        }
        return allies;
    }

    public List<Monster> GetAllEnemies()
    {
        List<Monster> enemies = new List<Monster>();
        foreach (DungeonRow row in new DungeonRow[] { dungeonRow1, dungeonRow2, dungeonRow3 })
        {
            foreach (Transform tileTransform in row.transform)
            {
                Tile tile = tileTransform.GetComponent<Tile>();
                if (tile != null && tile.monster != null && tile.monster.team == "Enemy")
                {
                    enemies.Add(tile.monster);
                }
            }
        }
        return enemies;
    }
}