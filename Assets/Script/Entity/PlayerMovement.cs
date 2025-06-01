using System;
using SGGames.Script.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Script.Entities
{
    [DisallowMultipleComponent]
    public class PlayerMovement : EntityBehavior
    {
        [Header("Movement")]
        [SerializeField] private float m_movespeed;
        [SerializeField] private Vector2 m_moveDirection;
        [SerializeField] private float m_raycastDistance;
        [SerializeField] private LayerMask m_obstacleLayer;

        private PlayerAnimationController m_playerAnimationController;
        private InputAction m_moveAction;
        private BoxCollider2D m_boxCollider2D;
        private RaycastHit2D m_raycastHit2D;
        
        public Action<bool> OnFlipping;
        
        private void Start()
        {
            m_boxCollider2D = GetComponent<BoxCollider2D>();
            m_playerAnimationController = GetComponent<PlayerAnimationController>();
            m_moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Update()
        {
            if (!m_isActive) return;

            HandleInput();
            UpdateCollision();
            UpdateMovement();
            UpdateAnimation();
        }

        private void HandleInput()
        {
            m_moveDirection = m_moveAction.ReadValue<Vector2>();
        }

        private void UpdateMovement()
        {
            transform.Translate(m_moveDirection * (m_movespeed * Time.deltaTime));
        }

        private void UpdateCollision()
        {
            if (m_moveDirection == Vector2.zero) return;
            
            m_raycastHit2D = Physics2D.BoxCast(transform.position,m_boxCollider2D.size,0,m_moveDirection,m_raycastDistance,m_obstacleLayer);

            if (m_raycastHit2D.collider != null)
            {
                m_moveDirection = Vector2.zero;
            }
        }

        private void UpdateAnimation()
        {
            OnFlipping?.Invoke(m_moveDirection.x < 0);
            m_playerAnimationController.OnRunningAnim?.Invoke(m_moveDirection != Vector2.zero);
        }
    }
}

