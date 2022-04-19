using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float speed;
    protected float health;
    protected float damage;
    protected float healing;
    protected bool isBlocking;

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
}
