using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public abstract class Character : MonoBehaviour, IComparable<Character>
{
    [SerializeField] protected TextMeshProUGUI infoText;

    float maxHealth;
    float speed;
    float health;
    float damage;
    float healing;
    bool isBlocking;
    bool isDead;

    protected BattleField battleField;

    public float MaxHealth { get { return maxHealth; } protected set { maxHealth = value; } }
    public float Speed { get { return speed; } protected set { speed = value; } }
    public float Health { get { return health; } protected set { health = value; } }
    public float Damage { get { return damage; } protected set { damage = value; } }
    public float Healing { get { return healing; } protected set { healing = value; } }
    public bool IsBlocking { get { return isBlocking;} protected set { isBlocking = value; } }
    public bool IsDead { get { return isDead;} protected set { isDead = value; } }

    void Awake()
    {
        battleField = GetComponent<BattleField>();
    }

    // Inflicts damage to the opponent
    protected void Attack(float damage, GameObject attackedGameObj)
    {
        Character attackedChar = attackedGameObj.GetComponent<Character>();

        if (attackedChar.IsBlocking)
        {
            damage -= 15;
            attackedChar.IsBlocking = false;
        }

        infoText.text = this.name + " attacked " + attackedGameObj.name + " and did " + damage + " damage!";
        attackedChar.Health -= damage;

        CheckIfDead(attackedChar);
    }

    // Heals the character on the same team
    protected void Heal(float healing, GameObject healedGameObj)
    {
        Character healedChar = healedGameObj.GetComponent<Character>();
        healedChar.Health += healing;

        if (healedChar.Health + healing > healedChar.MaxHealth)
        {
            healing = healedChar.MaxHealth - healedChar.Health + healing;
            healedChar.Health = healedChar.MaxHealth;
        }

        infoText.text = this.name + " healed " + healedGameObj.name + " healing " + healing + " life points!";
    }

    // Reduces damage of the next incoming attack
    protected void Block()
    {
        infoText.text = this.name + " is blocking!";
        IsBlocking = true;
    }

    public abstract void MakeTurn(Character activeChar);

    // Sorts by speed
    public int CompareTo(Character character)
    {
        if (character == null)
            return 1;

        else
            return this.Speed.CompareTo(character.Speed);
    }

    // Lets the active character idle up and down
    protected IEnumerator MoveUpAndDown(float firstHeight, float secondHeight)
    {
        if(transform.position.y == firstHeight)
        {
            yield return new WaitForSeconds(1);

            transform.position = new Vector3(transform.position.x, secondHeight, transform.position.z);

            yield return new WaitForSeconds(1);

            transform.position = new Vector3(transform.position.x, firstHeight, transform.position.z);
        }
    }

    // If a character is killed he will be teleported away
    void CheckIfDead(Character deadChar)
    {
        if (deadChar.Health <= 0)
        {
            if (!deadChar.IsDead)
            {
                if (deadChar is PlayableCharakter)
                {
                    BattleField.deadPlayerCounter++;
                    Debug.Log("Player dead: " + BattleField.deadPlayerCounter);
                }
                else if (deadChar is Enemy)
                {
                    BattleField.deadEnemyCounter++;
                    Debug.Log("Enemies dead: " + BattleField.deadEnemyCounter);
                }
            }

            deadChar.Health = 0;

            infoText.text = deadChar + " is dead.";
            deadChar.IsDead = true;
            deadChar.transform.position = new Vector3(deadChar.transform.position.x, -15, deadChar.transform.position.z);
        }
    }
}
