using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private bool hasAction = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator WaitInTurn()
    {
        yield return new WaitForSeconds(2);
        infoText.text = "vorbei";
        StopAllCoroutines();
        BattleField.isTurnDone = true;
    }

    public override void MakeTurn(Character activeChar)
    {

        if (hasAction)
        {
            int randomAction = Random.Range(0, 3);
            int randomTarget = Random.Range(0, 4);

            string actionString = "";

            switch (randomAction)
            {
                case 0:
                    actionString = "Attack";
                    break;
                case 1:
                    actionString = "Heal";
                    break;
                case 2:
                    Block();
                    break;
            }

            if (actionString == "Attack")
            {
                switch (randomTarget)
                {
                    case 0:
                        Attack(damage, battleField.Friends[0]);
                        break;
                    case 1:
                        Attack(damage, battleField.Friends[1]);
                        break;
                    case 2:
                        Attack(damage, battleField.Friends[2]);
                        break;
                    case 3:
                        Attack(damage, battleField.Friends[3]);
                        break;
                }
            }
            else if (actionString == "Heal")
            {
                switch (randomTarget)
                {
                    case 0:
                        Heal(healing, battleField.Enemies[0]);
                        break;
                    case 1:
                        Heal(healing, battleField.Enemies[1]);
                        break;
                    case 2:
                        Heal(healing, battleField.Enemies[2]);
                        break;
                    case 3:
                        Heal(healing, battleField.Enemies[3]);
                        break;
                }
            }

            hasAction = false;
        }

        StartCoroutine(WaitInTurn());
    }
}
