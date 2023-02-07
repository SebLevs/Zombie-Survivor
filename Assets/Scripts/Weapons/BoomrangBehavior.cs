using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomrangBehavior : MonoBehaviour, IPoolable
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    private bool comingBack = false;
    public AnimationCurve moveCurveGo;
    public AnimationCurve moveCurveBack;
    public float moveDuration = 2f;
    private float moveStopWatch = 0;
    public bool isShot = false;
    public void OnGetFromAvailable()
    {
        targetLocation = new Vector2(Player_Controller.Instance.mousePosition.x, Player_Controller.Instance.mousePosition.y).normalized * Entity_Player.Instance.boomDistance;
        startLocation = Entity_Player.Instance.transform.position;
    }

    public void OnReturnToAvailable()
    {
        
    }

    private void Update()
    {
        if(isShot)
        {
            moveStopWatch += Time.deltaTime;

            if (moveStopWatch >= moveDuration && !comingBack)
            {
                comingBack = true;
                moveStopWatch = 0;
                transform.position = targetLocation;
            }
            if (moveStopWatch >= moveDuration && comingBack)
            {
                comingBack = false;
                moveStopWatch = 0;
                isShot = false;
                WeaponManager.Instance.boomPool.ReturnToAvailable(this);
                
            }
            if (!comingBack)
            {
                transform.position = Vector2.Lerp(startLocation, targetLocation, moveCurveGo.Evaluate(moveStopWatch/ moveDuration));
            }
            else
            {
                transform.position = Vector2.Lerp(targetLocation, Entity_Player.Instance.transform.position, moveCurveBack.Evaluate(moveStopWatch / moveDuration));
            }
        }
    }

    public void ShootBoom()
    {
        isShot = true;
    }
  
}
