using UnityEngine;

namespace Player
{
    [CreateAssetMenu]
    public class PlayerStatsSO : ScriptableObject
    {
        public int MaxHealth;
        public float MoveSpeed;
        public float AttackSpeed;
        public float BoomDistance;
        public float BoomAttackSpeed;
        public float DodgeDelay;
        public int BigGold;
        public int SmallGold;
    }
}