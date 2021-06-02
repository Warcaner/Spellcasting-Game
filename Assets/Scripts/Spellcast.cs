using PDollarGestureRecognizer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.IO;
using UnityEngine.Events;
using System.Linq;

class Spellcast : MonoBehaviour
{
    CharacterStats playerStats;
    MovementRecognizer movementRecognizer;

    //All of our GameObjects
    public GameObject FireSpell;
    public GameObject FireIceSpell;
    public GameObject FireThunderSpell;
    public GameObject IceSpell;
    public GameObject IceFireSpell;
    public GameObject IceThunderSpell;
    public GameObject ThunderSpell;
    public GameObject ThunderFireSpell;
    public GameObject ThunderIceSpell;

    [SerializeField] float smallManaLoss ;
    [SerializeField] float bigManaLoss ;
    
    float manaRegenSpeed = 1f;
    

    [SerializeField] private float forceAmount = 10f;

    public Transform movementSource;
    public Transform cube;

    GameObject target;

    public float Spellcost { get ; set;}

    public float SpellDamage { get; set;}

    private Rigidbody rigidBody;

    private void Start()
    {
        playerStats = GetComponent<CharacterStats>(); //skift måske til CharacterStats
        target = GameObject.FindGameObjectWithTag("Player");
        movementRecognizer = GetComponent<MovementRecognizer>();

    }

    public void Update()
    {
        RegenMana();
    }


    public void Fire()
    {
        

        if (target.GetComponent<CharacterStats>().currMana >= smallManaLoss)
        {
            target.GetComponent<CharacterStats>().SpendMana(smallManaLoss);
            GameObject spawnFire = Instantiate(FireSpell, cube.position, cube.rotation);
            rigidBody = spawnFire.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnFire, 3.0f);

        }
        
        else
        {
            Debug.Log("Not enough Mana");
        }

    }
    
    public void FireIce()
    {

        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {
            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);

            GameObject spawnFireIce = Instantiate(FireIceSpell, cube.position, cube.rotation);
            rigidBody = spawnFireIce.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnFireIce, 3.0f);


        }
            

    }

    public void FireThunder()
    {
        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {

            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);
            GameObject spawnFireThunder = Instantiate(FireThunderSpell, cube.position, cube.rotation);
            rigidBody = spawnFireThunder.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnFireThunder, 3.0f);


        }

    }

    public void Ice()
    {


        if (target.GetComponent<CharacterStats>().currMana >= smallManaLoss)
        {
            target.GetComponent<CharacterStats>().SpendMana(smallManaLoss);
            GameObject spawnIce = Instantiate(IceSpell, cube.position, cube.rotation);
            rigidBody = spawnIce.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnIce, 3.0f);

        }

        else
        {
            Debug.Log("Not enough Mana");
        }

    }

    public void IceFire()
    {
        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {

            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);
            GameObject spawnIceFire = Instantiate(IceFireSpell, cube.position, cube.rotation);
            rigidBody = spawnIceFire.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnIceFire, 3.0f);


        }

    }

    public void IceThunder()
    {
        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {

            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);
            GameObject spawnIceThunder = Instantiate(IceThunderSpell, cube.position, cube.rotation);
            rigidBody = spawnIceThunder.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnIceThunder, 3.0f);


        }

    }

    public void Thunder()
    {


        if (target.GetComponent<CharacterStats>().currMana >= smallManaLoss)
        {
            target.GetComponent<CharacterStats>().SpendMana(smallManaLoss);
            GameObject spawnThunder = Instantiate(ThunderSpell, cube.position, cube.rotation);
            rigidBody = spawnThunder.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnThunder, 3.0f);

        }

        else
        {
            Debug.Log("Not enough Mana");
        }

    }

    public void ThunderFire()
    {
        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {

            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);
            GameObject spawnThunderFire = Instantiate(ThunderFireSpell, cube.position, cube.rotation);
            rigidBody = spawnThunderFire.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnThunderFire, 3.0f);


        }

    }

    public void ThunderIce()
    {
        if (target.GetComponent<CharacterStats>().currMana >= bigManaLoss)
        {

            target.GetComponent<CharacterStats>().SpendMana(bigManaLoss);
            GameObject spawnThunderIce = Instantiate(ThunderIceSpell, cube.position, cube.rotation);
            rigidBody = spawnThunderIce.GetComponent<Rigidbody>();
            rigidBody.AddForce(cube.transform.forward * forceAmount);

            Object.Destroy(spawnThunderIce, 3.0f);


        }

    }



    /*void RegenMana()
    {
        if (Time.time - lastRegen > manaRegenSpeed)
        {
            playerStats.currMana += manaRegenAmount;
            lastRegen = Time.time;
            playerStats.CheckMana();


        }
    }*/

    void RegenMana()
    {
        target.GetComponent<CharacterStats>().currMana += manaRegenSpeed * Time.deltaTime;
    }



}
