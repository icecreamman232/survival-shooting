using UnityEngine;

namespace SGGames.Script.Entities
{
    /// <summary>
    /// Base class for all type of entity in the game
    /// </summary>
    public class EntityBehavior : MonoBehaviour
    {
        [Header("Entity")]
        [SerializeField] protected bool m_isActive = true;
        
        public bool IsActive => m_isActive;

        public void ToggleActive(bool isActive)
        {
            m_isActive = isActive;
        }
    }
}

