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
        SetCursorOnNearestEnemy();
        PrimaryAttack();
        SecondaryAttack();
        Dodge();
        MoveToTarget();
    }

    private void PrimaryAttack()
    {
        var action = new PlayerAction(PlayerActionsType.SHOOT);
        AddActionToPlayer(action);
    }

    private void SecondaryAttack()
    {
        var action = new PlayerAction(PlayerActionsType.SPECIALSHOOT);
        AddActionToPlayer(action);
    }

    private void Dodge()
    {
        var action = new PlayerAction(PlayerActionsType.DODGE);
        AddActionToPlayer(action);
    }

    private void ManagerSetCursor()
    {

    }

    private void SetCursorOnNearestEnemy()
    {
        HashSet<Enemy> enemies = EnemyManager.Instance.CurrentlyActiveEnemies;

        if (enemies.Count == 0)
        {
            _player.Controller.SetLookAt(_player.transform.position);
            return;
        }

        float leastDistance = Vector2.Distance(_player.transform.position, enemies.ElementAt(0).transform.position);
        Enemy targetEnemy = enemies.ElementAt(0);
        for (int i = 1; i < enemies.Count; i++)
        {
            Enemy enemy = enemies.ElementAt(i);
            float currentDistance = Vector2.Distance(_player.transform.position, enemies.ElementAt(i).transform.position);
            if (currentDistance < leastDistance) 
            {
                leastDistance = currentDistance;
                targetEnemy = enemies.ElementAt(i);
            }
        }

        _player.Controller.SetLookAt(targetEnemy.transform.position);
    }

    private void SetCursorOnBoss()
    {

    }

    private void MoveToTarget()
    {
        if (!target)
        {
            // get target around player here
        }
        else
        {
            Vector2 distance = target.transform.position - _player.transform.position;

            _player.Controller.UpdateMoveDirection(distance.normalized);
        }
    }

    private void AddActionToPlayer(PlayerAction action) => _player.DesiredActions.AddAction(action);
}
