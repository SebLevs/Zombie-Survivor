using System;
using UnityEngine;

public class PseudoTDD_Seb : MonoBehaviour
{
    [Header("Entity")]
    public bool isEnemyHit;
    public bool isPlayerHit;

    [Header("UI")]
    public bool isOptionPopup;

#if UNITY_EDITOR
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
            Entity_Player.Instance.Health.Hit(10);
        });

        Test(ref isEnemyHit, () =>
        {

        });
    }
#endif

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
                UIManager.Instance.ViewController.SwitchViewSequential(UIManager.Instance.ViewOptionMenu);
            }
            else
            {
                UIManager.Instance.ViewController.SwitchViewSequential(UIManager.Instance.ViewEmpty);
            }
        }
    }
}
