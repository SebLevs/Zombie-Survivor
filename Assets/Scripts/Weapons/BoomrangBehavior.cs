using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoomrangBehavior : BaseProjectile, IPoolable
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    private bool comingBack = false;
    public AnimationCurve moveCurveGo;
    public AnimationCurve moveCurveBack;
    public float moveDurationGo = 2f;
    public float moveDurationBack = 1f;
    private float moveStopWatch = 0;
    public bool isShot = false;
    private float rotationZ;
    private float rotationSpeed = 720f;
    protected override void OnStart()
    {
        
    }

    protected override void OnAwake()
    {
        
    }

    protected override void OnUpdate()
    {
        if (isShot)
        {
            moveStopWatch += Time.deltaTime;
            rotationZ += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            if (moveStopWatch >= moveDurationGo && !comingBack)
            {
                comingBack = true;
                moveStopWatch = 0;
                transform.position = targetLocation;
            }
            if (moveStopWatch >= moveDurationBack && comingBack)
            {
                comingBack = false;
                moveStopWatch = 0;
                isShot = false;
                WeaponManager.Instance.boomPool.ReturnToAvailable(this);

            }
            if (!comingBack)
            {
                transform.position = Vector2.Lerp(startLocation, targetLocation, moveCurveGo.Evaluate(moveStopWatch / moveDurationGo));
            }
            else
            {
                transform.position = Vector2.Lerp(targetLocation, Entity_Player.Instance.transform.position, moveCurveBack.Evaluate(moveStopWatch / moveDurationBack));
            }
        }
    }

    public void OnGetFromAvailable()
    {
        rotationZ = 0f;
        rotationSpeed = 720f;
        startLocation = Entity_Player.Instance.transform.position;
        targetLocation = startLocation + (Player_Controller.Instance.normalizedLookDirection * Entity_Player.Instance.boomDistance);
    }

    public void OnReturnToAvailable()
    {
        
    }

    public void ShootBoom()
    {
        isShot = true;
    }

    
}
