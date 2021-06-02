using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;

public class PlayerStats : CharacterStats
{
    PlayerUI playerUI;

    GameController gameController;
    

    public XRNode inputSource;
    public InputHelpers.Button continueButton;
    public float inputThreshold = 0.1f;



    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        playerUI = GetComponent<PlayerUI> ();

        maxHealth = 100;
        currHealth = maxHealth;

        maxMana = 100;
        currMana = maxMana;

        SetStats();
    }

    private void Update()
    {
        CheckHealth();
        CheckMana();
    }

    public override void Die()
    {

        Time.timeScale = 0;

        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), continueButton, out bool isPressed, inputThreshold);


        if (isDead == true && isPressed) {
             Debug.Log("Player has pressed continue");
             gameController.ReloadLevel();
         } 



    }

    void SetStats()
    {
        gameController.healthAmount.text = currHealth.ToString("0");

        gameController.manaAmount.text = currMana.ToString("0");
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        SetStats();
    }

    public override void CheckMana()
    {
        base.CheckMana();
        SetStats();
    }


    
    
}
