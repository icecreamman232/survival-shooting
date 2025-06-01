using UnityEngine;

namespace SGGames.Script.Entities
{
    public class EnemyMovement : EntityBehavior
    {
        [SerializeField] private float m_movespeed;
        [SerializeField] private Vector2 m_moveDirection;
        [SerializeField] private LayerMask m_obstacleLayer;

        private static float s_RAY_CAST_DISTANCE = 0.3f;
        private BoxCollider2D m_collider;
        private bool m_canMove;
        private RaycastHit2D m_raycastHit2D;

        private void Start()
        {
            m_collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            if (!m_canMove) return;
            UpdateCollision();
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            transform.Translate(m_moveDirection * (m_movespeed * Time.deltaTime));
        }

        private void UpdateCollision()
        {
            if (m_moveDirection == Vector2.zero) return;
            
            m_raycastHit2D = Physics2D.BoxCast(transform.position,m_collider.size,0,m_moveDirection,s_RAY_CAST_DISTANCE,m_obstacleLayer);

            if (m_raycastHit2D.collider != null)
            {
                m_moveDirection = Vector2.zero;
            }
        }
    }
}

