using System;
using System.Collections;
using UnityEngine;

namespace SGGames.Script.HealthSystem
{
    public class Health : MonoBehaviour
    {
        [Header("Base")] 
        [SerializeField] protected int m_maxHealth;
        [SerializeField] protected int m_currentHealth;
        [SerializeField] protected bool m_isInvulnerable;

        public Action OnDeath;
        public Action<GameObject, int> OnHit;
        public int CurrentHealth => m_currentHealth;

        protected virtual void Start()
        {
            Initialize();
        }
        
        protected virtual void Initialize()
        {
            m_currentHealth = m_maxHealth;
        }

        protected virtual bool CanTakeDamage()
        {
            if (m_isInvulnerable) return false;
            if (m_currentHealth <= 0) return false;
            return true;
        }
        
        public virtual void TakeDamage(int damage, GameObject source, float invulnerableDuration)
        {
            if (!CanTakeDamage()) return;
            
            m_currentHealth -= damage;
            
            OnHit?.Invoke(source, damage);

            if (m_currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(OnInvulnerable(invulnerableDuration));
            }
            
        }

        protected virtual IEnumerator OnInvulnerable(float duration)
        {
            m_isInvulnerable = true;
            yield return new WaitForSeconds(duration);
            m_isInvulnerable = false;
        }

        protected virtual void Kill()
        {
            OnDeath?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
