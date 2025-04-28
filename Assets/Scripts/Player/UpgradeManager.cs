using Player.Attacks;
using UnityEngine;

namespace Player
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerAttackManager _playerAttackManager;


        public void GiveUpgrade(PlayerAttackStats attack)
        {
            
            // Give the attack to the player
            _playerAttackManager.GiveAttack(attack);
        }

        public void DestroyButton(GameObject button)
        {
            Destroy(button);
        }

        
    }
}