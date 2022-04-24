using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayableCharakter : Character
{
    [SerializeField] protected TextMeshProUGUI healthDisplay;

    public bool isWaiting = true;  // If waiting the character can attack

    Character activeChar;  // So that not every character can activate the button at the same time
    string lastAction;  // Helps moving in and out the UI

    // The height in which the player characters go up and down
    float lowIdleHeight = 0.5f;
    float highIdleHeight = 2.0f;


    void Awake()
    {
        ActivateButtons();
    }

    // Assigns every button
    void ActivateButtons()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Action"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectAction(b); });
        }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Friend"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectFriend(b); });

            ShrinkButtonDown("Friend");
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Button b = g.GetComponent<Button>();
            b.onClick.AddListener(delegate { SelectEnemy(b); });

            ShrinkButtonDown("Enemy");
        }

        LoadStats();
    }

    protected virtual void LoadStats()
    {

    }

    // Selects the action the player takes
    void SelectAction(Button action)
    {
        string actionStr = action.GetComponentInChildren<TextMeshProUGUI>().text;

        if (!IsDead)
        {
            if (actionStr == "Heal")
            {
                ShrinkButtonDown("Action");
                SetButtonToNormalSize("Friend");
            }
            else if (actionStr == "Attack")
            {
                ShrinkButtonDown("Action");
                SetButtonToNormalSize("Enemy");
            }
            else if (actionStr == "Block")
            {
                if (isWaiting && activeChar == this)
                {
                    Block();
                    isWaiting = false;
                }
            }
        }
        else
        {
            isWaiting = false;
        }
    }

    // Heals the chosen friend
    void SelectFriend(Button friendBttn)
    {
        string friendStr = friendBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        if (isWaiting && activeChar == this)
        {
            lastAction = "Heal";

            switch (friendStr)
            {
                case "Healer":
                    Heal(Healing, BattleField.Friends[0]);
                    isWaiting = false;
                    break;
                case "DPS Left":
                    Heal(Healing, BattleField.Friends[1]);
                    isWaiting = false;
                    break;
                case "DPS Right":
                    Heal(Healing, BattleField.Friends[2]);
                    isWaiting = false;
                    break;
                case "Tank":
                    Heal(Healing, BattleField.Friends[3]);
                    isWaiting = false;
                    break;
                default:
                    SetButtonToNormalSize("Action");
                    ShrinkButtonDown("Friend");
                    break;
            }
        }
    }

    // Inflicts damage to the chosen enemy
    void SelectEnemy(Button enemyBttn)
    {
        string enemyStr = enemyBttn.GetComponentInChildren<TextMeshProUGUI>().text;

        if (isWaiting && activeChar == this)
        {
            lastAction = "Attack";

            switch (enemyStr)
            {
                case "Left":
                    Attack(Damage, BattleField.Enemies[0]);
                    isWaiting = false;
                    break;
                case "Middle L":
                    Attack(Damage, BattleField.Enemies[1]);
                    isWaiting = false;
                    break;
                case "Middle R":
                    Attack(Damage, BattleField.Enemies[2]);
                    isWaiting = false;
                    break;
                case "Right":
                    Attack(Damage, BattleField.Enemies[3]);
                    isWaiting = false;
                    break;
                default:
                    SetButtonToNormalSize("Action");
                    ShrinkButtonDown("Enemy");
                    break;
            }
        }
    }

    // After an action was selected, there is a pause of 2 seconds. This lets the player read the text
    protected IEnumerator WaitInTurn()
    {
        if (isWaiting == false)
        {
            yield return new WaitForSeconds(2);
            StopAllCoroutines();
            transform.position = new Vector3(transform.position.x, lowIdleHeight, transform.position.z);

            BattleField.isTurnDone = true;
            ResetUI();
            this.activeChar = null;
        }
    }

    // Activates the button for the current character and lets them go up and down
    public override void MakeTurn(Character activeChar)
    {
        this.activeChar = activeChar;

        StartCoroutine(WaitInTurn());
        StartCoroutine(MoveUpAndDown(lowIdleHeight, highIdleHeight));
    }

    // Bring the UI back to its normal size
    void SetButtonToNormalSize(string buttonsToNormalize)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(buttonsToNormalize))
        {
            g.transform.localScale = new Vector3(1, 1);
        }
    }

    // Makes the UI very small
    void ShrinkButtonDown(string buttonsToShrink)
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(buttonsToShrink))
        {
            g.transform.localScale = new Vector3(0.005f, 0.005f);
        }
    }

    // After a turn ends, the UI is reset
    void ResetUI()
    {
        SetButtonToNormalSize("Action");

        if (lastAction == "Heal")
        {
            ShrinkButtonDown("Friend");
        }
        else if (lastAction == "Attack")
        {
            ShrinkButtonDown("Enemy");
        }
    }
}
