using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestBehavior : MonoBehaviour
{
    public int minValue;
    public int maxValue;

    private int nextPowerUpID;
    public int chestValue;
    private Entity_Player player = Entity_Player.Instance;

    private CommandInvoker commandInvoker;



    private void Awake()
    {
        chestValue = Random.Range(minValue, maxValue + 1);
    }

    private void Start()
    {
        commandInvoker = CommandPromptManager.Instance.playerCommandInvoker;
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
            player.currentGold -= chestValue;
            //player.RefreshExperienceBar();
            nextPowerUpID = Random.Range(2, commandInvoker.allCommands.Count + 1);
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
            player.RefreshPlayerTimerStats();
            gameObject.SetActive(false);
        }
    }
}
