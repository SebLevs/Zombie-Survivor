using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutomatedTestPlayer : MonoBehaviour, IPauseListener, IUpdateListener
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isAutomatedTest = false;
    private Entity_Player _player;

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
        GameManager.Instance.SubscribeToPauseGame(this);
        UpdateManager.Instance.SubscribeToUpdate(this);
    }

    private void Start()
    {
        _player = Entity_Player.Instance;
    }

    public void OnPauseGame()
    {
    }

    public void OnResumeGame()
    {
    }

    public void OnUpdate()
    {
        Dodge();
        MoveToTarget();

        TrySetPotionAsTarget();

        SetAttackTargetHandler();
        PrimaryAttack();
        SecondaryAttack();
    }

    public List<T> GetOverllapedComponentsInCircle<T>(Transform from, float radius, int maxOverlapAmount) where T:Component
    {
        // TODO: Go to a potion when reaching a life percentage treshold
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

    private void PrimaryAttack()
    {
        if (EnemyManager.Instance.CurrentlyActiveEnemies.Count == 0) { return; }
        var action = new PlayerAction(PlayerActionsType.SHOOT);
        AddActionToPlayer(action);
    }

    private void SecondaryAttack()
    {
        if (EnemyManager.Instance.CurrentlyActiveEnemies.Count == 0) { return; }
        var action = new PlayerAction(PlayerActionsType.SPECIALSHOOT);
        AddActionToPlayer(action);
    }

    private void Dodge()
    {
        // TODO: Dodge when enemy OR bone gets closer than X distance
        var action = new PlayerAction(PlayerActionsType.DODGE);
        AddActionToPlayer(action);
    }

    private void SetAttackTargetHandler()
    {
        EnemyManager enemyManager = EnemyManager.Instance;

/*        List<TypeZombieBoss> bosses = GetOverllapedComponentsInCircle<TypeZombieBoss>(_player.transform, overlapSphereradius, 10);
        if (bosses.Count == 0)
        {
            SetCursorOnNearestObject(bosses);
            return;
        }*/

        List<Enemy> enemies = GetOverllapedComponentsInCircle<Enemy>(_player.transform, overlapSphereradius, 100);
        SetCursorOnNearestObject(enemies);
    }

    private void SetCursorOnNearestObject<T>(List<T> objects) where T: Component
    {
        if (objects.Count == 0)
        {
            _player.Controller.SetLookAt(_player.transform.position);
            return;
        }
        _player.Controller.SetLookAt(LinearAlgebraUtilities.GetClosestObject(objects, _player.transform).transform.position);
    }

    private void SetTarget(Transform target) => this.target = target;

    [SerializeField] private float reachedDistance = 0.5f;
    private void MoveToTarget()
    {
        // TODO: Cast a circle overlap to get objects
        // TODO: Move where enemy OR bone isnt in path

        if (!target)
        {
            // TODO: Set target here
        }
        else
        {
            Vector2 distance = target.transform.position - _player.transform.position;

            if (distance.magnitude <= reachedDistance) 
            {
                _player.Controller.UpdateMoveDirection(Vector2.zero);
                return;
            }

            _player.Controller.UpdateMoveDirection(distance.normalized);
        }
    }

    [SerializeField] private float overlapSphereradius = 5f;
    [SerializeField][Range(0, 1)] private float hpTreshold = 0.5f;
    private PotionBehavior _lastPotion;
    private bool TrySetPotionAsTarget()
    {
        if (_player.Health.Normalized > hpTreshold) { return false; }
        if (target == _lastPotion?.transform) { return true; }

        List<PotionBehavior> potionBehaviours = GetOverllapedComponentsInCircle<PotionBehavior>(_player.transform, overlapSphereradius, 100);
        target = LinearAlgebraUtilities.GetClosestObject(potionBehaviours, _player.transform).transform;

        return true;
    }

    private void TryOpenChest()
    {
        // TODO: Open a chest when reaching a currency treshold (circle overlay for fetching nearby chests and get currency requirement from them?)
    }

    private void StartBossEncounter()
    {
        // TODO: Goto boss portal and Start boss encounter when reaching N bonus and M money
    }

    private void AddActionToPlayer(PlayerAction action) => _player.DesiredActions.AddAction(action);
}
