using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class Character : MonoBehaviour, IComparable<Character>
{
    protected float speed;
    protected float health;
    protected float damage;
    protected float healing;
    protected bool isBlocking;

    public float Speed { get { return speed; } }
    public float Health { get { return health; } }
    public float Damage { get { return damage; } }
    public float Healing { get { return healing; } }
    public bool IsBlocking { get { return isBlocking;} }

    protected GameObject battleScreenBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Attack(float damage, Character attackedChar)
    {
        Debug.Log("Angiff");
        attackedChar.health -= damage;
    }

    protected void Heal(float healing, Character healedChar)
    {
        healedChar.health += healing;
    }

    protected void Block()
    {
        isBlocking = true;
    }

    protected abstract void MakeTurn();

    public int CompareTo(Character character)
    {
        if (character == null)
            return 1;

        else
            return this.Speed.CompareTo(character.Speed);
    }
}
