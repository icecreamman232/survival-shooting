using System;
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
        
        private InputAction m_moveAction;
        private Animator m_animator;
        private SpriteRenderer m_spriteRenderer;
        
        private static readonly int IsRunning = Animator.StringToHash("Is_Running");

        public Action<bool> OnFlipping;
        
        private void Start()
        {
            m_moveAction = InputSystem.actions.FindAction("Move");
            m_animator = GetComponentInChildren<Animator>();
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
            m_spriteRenderer.flipX = m_moveDirection.x < 0;
            OnFlipping?.Invoke(m_moveDirection.x < 0);
            m_animator.SetBool(IsRunning,m_moveDirection != Vector2.zero);
        }
    }
}

