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

        private PlayerAnimationController m_playerAnimationController;
        private InputAction m_moveAction;
        public Action<bool> OnFlipping;
        
        private void Start()
        {
            m_playerAnimationController = GetComponent<PlayerAnimationController>();
            m_moveAction = InputSystem.actions.FindAction("Move");
        }

        private void Update()
        {
            if (!m_isActive) return;

            HandleInput();
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

        private void UpdateAnimation()
        {
            OnFlipping?.Invoke(m_moveDirection.x < 0);
            m_playerAnimationController.OnRunningAnim?.Invoke(m_moveDirection != Vector2.zero);
        }
    }
}

