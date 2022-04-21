using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BattleField : MonoBehaviour
{
    [SerializeField] PlayableCharakter[] friends;
    [SerializeField] Enemy[] enemies;

    public static bool isTurnDone;
    public PlayableCharakter[] Friends { get { return friends; } }
    public Enemy[] Enemies { get { return enemies; } }

    private List<Character> turnOrder = new List<Character>();
    private LinkedList<Character> turnOrderLinked = new LinkedList<Character>();

    // Start is called before the first frame update
    void Start()
    {
        DetermineOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurnDone)
        {
            Debug.Log("New Turn");
            isTurnDone = false;
            if (turnOrderLinked.First.Next != null)
            {
                turnOrderLinked.RemoveFirst();
            }
            else
            {
                DetermineOrder();
            }
        }
        else
        {
            Character first = turnOrderLinked.First.Value;
            first.MakeTurn(first);
        }
    }

    void DetermineOrder()
    {
        foreach (Character character in friends)
        {
            turnOrder.Add(character);
        }

        foreach (Enemy enemy in enemies)
        {
            turnOrder.Add(enemy);
        }

        turnOrder.Sort();

        foreach(Character turn in turnOrder)
        {
            turnOrderLinked.AddFirst(turn);
            
        }
        foreach (Character turn in turnOrderLinked)
        {
            Debug.Log(turn.Speed);

        }
    }
}
