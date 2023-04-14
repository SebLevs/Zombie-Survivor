using System.Collections.Generic;
using UnityEngine;

public class PlayerAutomatedTestController : MonoBehaviour, IPauseListener, IUpdateListener
{
    [SerializeField] private bool isAutomatedTest = false;
    [Space]

    [SerializeField] private Transform backupTarget;
    public Transform Target;
    
    public Entity_Player Player { get; private set; }

    [Header("Tests")]
    [SerializeField] private AutomatedTestPrimaryAttack primaryAttackTest;
    [SerializeField] private AutomatedTestSecondaryAttack secondaryAttackTest;
    [SerializeField] private AutomatedTestDodge dodgeTest;
    [SerializeField] private AutomatedTestPotionPickup potionPickupTest;
    [SerializeField] private AutomatedTestChestPickup chestPickupTest;
    [SerializeField] private AutomatedTestStartBossEncounter startBossTest;

    [Header("Movement")]
    [SerializeField] private float reachedDistance = 0.5f;
    public float GetDistanceToTarget() => (Target.transform.position - Player.transform.position).magnitude;
    public Vector2 GetDirectionToTarget() => (Target.transform.position - Player.transform.position).normalized;
    public bool HasReachedTarget() => GetDistanceToTarget() <= reachedDistance;

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) { return; }

        potionPickupTest.DrawHandleGizmo(transform);
        chestPickupTest.DrawHandleGizmo(transform);

        dodgeTest.DrawHandleGizmo(transform);
    }
#endif

    public void OnDisable()
    {
        if (!isAutomatedTest) { return; }

        GameManager gameManager = GameManager.Instance;
        if (gameManager) { gameManager.UnSubscribeFromPauseGame(this); }

        UpdateManager updateManager = UpdateManager.Instance;
        if (updateManager) { updateManager.UnSubscribeFromUpdate(this); }
    }

    public void OnEnable()
    {
        Target = backupTarget;
        GameManager.Instance.SubscribeToPauseGame(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    private void Awake()
    {
#if !UNITY_EDITOR
    this.enabled = false;
#else
        //UIManager.Instance.ViewOptionMenu.EnableAutomatedTestButton();
#endif
    }

    private void Start()
    {
        Player = Entity_Player.Instance;
    }

    public void OnPauseGame()
    {
    }

    public void OnResumeGame()
    {
    }

    public void OnUpdate()
    {
        dodgeTest.ExecuteTest(this);
        MoveToTarget(); // TODO: Replace with move around with backuptarget

        if (!potionPickupTest.ExecuteTest(this))
        {
            if (!chestPickupTest.ExecuteTest(this))
            {
                if (startBossTest.ExecuteTest(this))
                {
                    Target = backupTarget;
                }
            }
        }

        primaryAttackTest.ExecuteTest(this);
        secondaryAttackTest.ExecuteTest(this);
    }

    public List<T> GetOverllapedComponentsInCircle<T>(Transform from, float radius, int maxOverlapAmount) where T:Component
    {
        Collider2D[] results = new Collider2D[maxOverlapAmount];
        Physics2D.OverlapCircleNonAlloc(from.position, radius, results);

        List<T> components = new();
        foreach (var item in results)
        {
            if (item == null) { break; }
            T component = item.GetComponent<T>();
            if (component) { components.Add(component); }
        }

        return components;
    }

    public bool TrySetNextAttackCursorPosition(float attackFromRange)
    {
        List<Enemy> enemies = GetOverllapedComponentsInCircle<Enemy>(Player.transform, attackFromRange, 100);
        if (enemies.Count == 0) { return false; }
        SetCursorOnNearestObject(enemies);
        return true;
    }

    private void SetCursorOnNearestObject<T>(List<T> objects) where T: Component
    {
        if (objects.Count == 0)
        {
            Player.Controller.SetLookAt(Player.transform.position);
            return;
        }
        Player.Controller.SetLookAt(LinearAlgebraUtilities.GetClosestObject(objects, Player.transform).transform.position);
    }

    private void MoveToTarget()
    {
        // TODO: raycast check for obstacles and use dodge if an obstacle exists
/*        Vector2 distance = Target.transform.position - Player.transform.position;

        if (distance.magnitude <= reachedDistance)
        {
            Player.Controller.UpdateMoveDirection(Vector2.zero);
            return;
        }*/

        if (HasReachedTarget())
        {
            Player.Controller.UpdateMoveDirection(Vector2.zero);
            return;
        }

        Player.Controller.UpdateMoveDirection(GetDirectionToTarget());
    }
}
