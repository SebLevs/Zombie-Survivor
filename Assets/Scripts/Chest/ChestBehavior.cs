using System;
using System.Collections.Generic;
using TMPro;
using TNRD;
using UnityEngine;
using Random = UnityEngine.Random;
using TMP_Text = TMPro.TMP_Text;

public class ChestBehavior : MonoBehaviour
{
    public int minValue;
    public int maxValue;

    private int nextPowerUpID;
    public int chestValue;
    private Entity_Player player = Entity_Player.Instance;
    private Collider2D col;

    private CommandInvoker commandInvoker;

    private TMP_Text uiValue;

    private Animator anim;



    private void Awake()
    {
        chestValue = Random.Range(minValue, maxValue + 1);
        uiValue = GetComponentInChildren<TMP_Text>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
        uiValue.text = "$ " + chestValue;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            TryOpenChest();
        }
    }

    private void TryOpenChest()
    {
        if (player.currentGold >= chestValue)
        {
            anim.Play("OpenChest");
            player.currentGold -= chestValue;
            //player.RefreshExperienceBar();
            nextPowerUpID = Random.Range(2, commandInvoker.allCommands.Count);
            switch (nextPowerUpID)
            {
                case 2 :
                    commandInvoker.DoCommand(commandInvoker.command2.Value);
                    break;
                case 3 :
                    commandInvoker.DoCommand(commandInvoker.command3.Value);
                    break;
                case 4 :
                    commandInvoker.DoCommand(commandInvoker.command4.Value);
                    break;
                case 5 :
                    commandInvoker.DoCommand(commandInvoker.command5.Value);
                    break;
                default:
                    commandInvoker.DoCommand(commandInvoker.command2.Value);
                    break;
            }
            Debug.Log(commandInvoker.commandName[nextPowerUpID - 1]);
            player.RefreshPlayerStats();
            col.enabled = false;
            uiValue.enabled = false;
        }
    }
}
