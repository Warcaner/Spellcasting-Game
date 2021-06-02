using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : CharacterStats
{
    GameController gameController;

    Spawner spawn;

    private float scoreAddAmount = 10;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spawn = gameController.GetComponentInChildren<Spawner>();

        maxHealth = 100;
        currHealth = maxHealth;

        maxMana = 100;
        currMana = maxMana;
    }

    private void Update()
    {
        CheckHealth();
        CheckMana();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
    }

    public override void CheckMana()
    {
        base.CheckMana();
    }


    public override void Die()
    {
        gameController.AddScore(scoreAddAmount);
        spawn.enemiesKilled++;
        
        Destroy(gameObject);

        if (spawn.enemiesKilled >= spawn.enemySpawnAmount)
        {
            spawn.NextWave();
        } 
    }


}
