using System;
using UnityEngine;

public class PseudoTDD_Seb : MonoBehaviour
{
    [Header("Entity")]
    public bool isEnemyHit;
    public bool isPlayerHit;


    [Header("UI")]
    public bool isOptionPopup;

    private void Update()
    {
        // UI
        Test(ref isOptionPopup, () =>
        {
            TestOptionsPopup();
            isOptionPopup = true;
        });

        // Entity
        Test(ref isPlayerHit, () =>
        {
            //Player.Instance.Health.OnHit();
        });

        Test(ref isEnemyHit, () =>
        {

        });
    }

    private void Test(ref bool check, Action test)
    {
        if (check)
        {
            check = false;
            test();
        }
    }

    private void TestOptionsPopup()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!UIManager.Instance.ViewOptionMenu.gameObject.activeSelf)
            {
                UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewOptionMenu);
            }
            else
            {
                UIManager.Instance.OnSwitchViewSequential(UIManager.Instance.ViewEmpty);
            }
        }
    }
}
