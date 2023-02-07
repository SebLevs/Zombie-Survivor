using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoomrangStrategy : IProjectileStrategy
{
    public Vector2 startLocation;
    public Vector2 targetLocation;
    private bool comingBack = false;
    public AnimationCurve moveCurveGo;
    public AnimationCurve moveCurveBack;
    public float moveDuration = 2f;
    private float moveStopWatch = 0;
    public bool isShot = false;

    public IEnumerator ShootBoom(Projectile projectile)
    {
        isShot= true;
        while (isShot)
        {
            yield return new WaitForFixedUpdate();
            moveStopWatch += Time.deltaTime;

            if (moveStopWatch >= moveDuration && !comingBack)
            {
                comingBack = true;
                moveStopWatch = 0;
                projectile.transform.position = targetLocation;
            }
            if (moveStopWatch >= moveDuration && comingBack)
            {
                comingBack = false;
                moveStopWatch = 0;
                isShot = false;
                //WeaponManager.Instance.boomPool.ReturnToAvailable(projectile);

            }
            if (!comingBack)
            {
                projectile.transform.position = Vector2.Lerp(startLocation, targetLocation, moveCurveGo.Evaluate(moveStopWatch / moveDuration));
            }
            else
            {
                projectile.transform.position = Vector2.Lerp(targetLocation, Entity_Player.Instance.transform.position, moveCurveBack.Evaluate(moveStopWatch / moveDuration));
            }
        }
        yield return null;
    }

    public override void Execute(Projectile projectile)
    {
        projectile.StopAllCoroutines();
        projectile.StartCoroutine(ShootBoom(projectile));
    }

    public override void OnGetFromAvailable(Projectile projectile = null)
    {
        targetLocation.x = Player_Controller.Instance.mousePosition.x;
        targetLocation.y = Player_Controller.Instance.mousePosition.y;
        targetLocation = targetLocation.normalized * Entity_Player.Instance.boomDistance;


        startLocation = Entity_Player.Instance.transform.position;
    }

    public override void OnReturnToAvailable(Projectile projectile = null)
    {
        
    }
}
