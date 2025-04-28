using System;
using System.Collections.Generic;
using Player.Attacks;
using UnityEngine;

namespace Player
{
    public class PlayerAttackManager : MonoBehaviour
    {
        [SerializeField] private List<PlayerAttackContext> _activeAttacks = new List<PlayerAttackContext>();

        [Serializable]
        public struct PlayerAttackContext
        {
            public float TimeSinceLastAttack;
            public PlayerAttackStats AttackStats;
            
            
        }
        private void Update()
        {
            for (int i = 0; i < _activeAttacks.Count; i++)
            {
                PlayerAttackContext attackContext = _activeAttacks[i];
                attackContext.TimeSinceLastAttack += Time.deltaTime;
                if (attackContext.TimeSinceLastAttack >= attackContext.AttackStats.FireCooldown)
                {
                    // Fire the attack
                    FireAttack(attackContext);
                    attackContext.TimeSinceLastAttack = 0;
                }
                _activeAttacks[i] = attackContext;

            }
        }

        private void FireAttack(PlayerAttackContext attackContext)
        {
            PlayerAttack attack = GameObject.Instantiate(attackContext.AttackStats.ProjectilePrefab, transform.position, Quaternion.identity);
        }

        public void GiveAttack(PlayerAttackStats attackStats)
        {
            PlayerAttackContext attackContext = new PlayerAttackContext
            {
                AttackStats = attackStats,
                TimeSinceLastAttack = 0
            };
            _activeAttacks.Add(attackContext);
        }

    }
}