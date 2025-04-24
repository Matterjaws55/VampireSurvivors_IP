using UnityEngine;

namespace Player.Attacks
{
    [CreateAssetMenu(fileName = "PlayerAttackStats", menuName = "PlayerAttackStats", order = 0)]
    public class PlayerAttackStats : ScriptableObject
    {
        public PlayerAttack ProjectilePrefab;
        public float FireCooldown = 5f;
    }
}