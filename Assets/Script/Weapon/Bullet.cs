using UnityEngine;

namespace SGGames.Script.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float m_movespeed;
        [SerializeField] private float m_range;
        private Vector2 m_moveDirection;
        private bool m_canMove;
        private float m_travelledDistance;

        public void Spawn(Vector2 position, Vector2 direction)
        {
            transform.position = position;
            m_moveDirection = direction;
            m_travelledDistance = 0;
            m_canMove = true;
        }
        
        private void Update()
        {
            if (!m_canMove) return;
            
            m_travelledDistance += m_movespeed * Time.deltaTime;
            transform.Translate(m_moveDirection * (m_movespeed * Time.deltaTime));

            if (m_travelledDistance >= m_range)
            {
                m_canMove = false;
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            this.gameObject.SetActive(false);
        }
    }
}


