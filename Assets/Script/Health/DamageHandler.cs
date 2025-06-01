using SGGames.Script.Common;
using UnityEngine;

namespace SGGames.Script.HealthSystem
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private int m_damage;
        [SerializeField] private float m_invulnerableDuration;
        [SerializeField] private LayerMask m_targetMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;
            
            CauseDamage(other.gameObject);
        }

        private void CauseDamage(GameObject targetObject)
        {
            if (targetObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(m_damage,this.gameObject,m_invulnerableDuration);
            }
        }
    }
}
