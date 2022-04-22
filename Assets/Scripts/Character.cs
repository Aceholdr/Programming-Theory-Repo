using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class Character : MonoBehaviour, IComparable<Character>
{
    [SerializeField] protected TextMeshProUGUI infoText;

    protected float speed;
    protected float health;
    protected float damage;
    protected float healing;
    protected bool isBlocking;

    protected BattleField battleField;

    public float Speed { get { return speed; } }
    public float Health { get { return health; } }
    public float Damage { get { return damage; } }
    public float Healing { get { return healing; } }
    public bool IsBlocking { get { return isBlocking;} }

    protected GameObject battleScreenBG;

    private void Awake()
    {
        battleField = GetComponent<BattleField>();
    }

    protected void Attack(float damage, GameObject attackedGameObj)
    {
        Character attackedChar = attackedGameObj.GetComponent<Character>();

        infoText.text = this.name + " attacked " + attackedGameObj.name + " and did " + damage + " damage!";
        attackedChar.health -= damage;
    }

    protected void Heal(float healing, GameObject healedGameObj)
    {
        Character healedChar = healedGameObj.GetComponent<Character>();

        infoText.text = this.name + " healed " + healedGameObj.name + " healing " + healing + " life points!";
        healedChar.health += healing;
    }

    protected void Block()
    {
        infoText.text = this.name + " is blocking!";
        isBlocking = true;
    }

    public abstract void MakeTurn(Character activeChar);

    public int CompareTo(Character character)
    {
        if (character == null)
            return 1;

        else
            return this.Speed.CompareTo(character.Speed);
    }
}
