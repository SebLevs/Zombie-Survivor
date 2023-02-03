using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PseudoTDD_Seb : MonoBehaviour
{
    [Header("Entity")]
    public bool isEnemyHit;
    public bool isPlayerHit;

    private void Update()
    {
        // Entity
        Test(isPlayerHit, () =>
        {
            //Player.Instance.Health.OnHit();
        });

        Test(isEnemyHit, () =>
        {

        });


    }

    private void Test(bool check, Action test)
    {
        if (check)
        {
            test();
        }
    }
}
