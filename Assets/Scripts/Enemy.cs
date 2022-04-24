using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character  // INHERITANCE
{
    public bool hasAction = true;

    float lowIdleHeight = 0.0f;
    float highIdleHeight = 1.5f;

    // Waits for 2 seconds, so that the player can read the text
    protected IEnumerator WaitInTurn()
    {
        yield return new WaitForSeconds(2);
        StopAllCoroutines();

        transform.position = new Vector3(transform.position.x, lowIdleHeight, transform.position.z);
        BattleField.isTurnDone = true;
    }

    // Randomly decides an action an a target
    public override void MakeTurn(Character activeChar)
    {

        if (hasAction && !IsDead)
        {
            int randomAction = Random.Range(0, 4);
            int randomTarget = Random.Range(0, 4);

            string actionString = "";

            switch (randomAction)
            {
                case 0:
                    actionString = "Attack";
                    break;
                case 1:
                    actionString = "Attack";
                    break;
                case 2:
                    actionString = "Heal";
                    break;
                case 3:
                    Block();
                    break;
            }

            if (actionString == "Attack")
            {
                switch (randomTarget)
                {
                    case 0:
                        Attack(Damage, BattleField.Friends[0]);
                        break;
                    case 1:
                        Attack(Damage, BattleField.Friends[1]);
                        break;
                    case 2:
                        Attack(Damage, BattleField.Friends[2]);
                        break;
                    case 3:
                        Attack(Damage, BattleField.Friends[3]);
                        break;
                }
            }
            else if (actionString == "Heal")
            {
                switch (randomTarget)
                {
                    case 0:
                        Heal(Healing, BattleField.Enemies[0]);
                        break;
                    case 1:
                        Heal(Healing, BattleField.Enemies[1]);
                        break;
                    case 2:
                        Heal(Healing, BattleField.Enemies[2]);
                        break;
                    case 3:
                        Heal(Healing, BattleField.Enemies[3]);
                        break;
                }
            }

            hasAction = false;
        }

        StartCoroutine(WaitInTurn());
        StartCoroutine(MoveUpAndDown(lowIdleHeight, highIdleHeight));
    }
}
