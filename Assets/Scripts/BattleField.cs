using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleField : MonoBehaviour
{
    [SerializeField] PlayableCharakter[] characters;
    [SerializeField] Enemy[] enemies;

    public PlayableCharakter[] Charakters { get { return characters; } }
    public Enemy[] Enemies { get { return enemies; } }

    private List<Character> turnOrder = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        DetermineOrder();
        foreach(Character turn in turnOrder)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DetermineOrder()
    {
        foreach(Character character in characters)
        {
            turnOrder.Add(character);
        }

        foreach(Enemy enemy in enemies)
        {
            turnOrder.Add(enemy);
        }

        turnOrder.Sort();
    }
}
