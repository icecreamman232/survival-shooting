using System;
using SGGames.Script.Animations;
using SGGames.Script.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SGGames.Script.Weapons
{
    public class PlayerWeaponHandler : EntityBehavior
    {
        [Header("Weapons")] 
        [SerializeField] private Vector2 m_flipValue;
        [SerializeField] private Transform m_weaponPivot;
        [SerializeField] private Weapon m_weapon;

        private InputAction m_shootAction;
        private Camera m_camera;
        private PlayerAnimationController m_playerAnimationController;
        private Vector2 m_aimDirection;
        
        private void Start()
        {
            m_playerAnimationController = GetComponent<PlayerAnimationController>();
            m_camera = Camera.main;
            m_shootAction = InputSystem.actions.FindAction("Shoot");
        }

        private void Update()
        {
            m_aimDirection = GetShootDirection();
            OnPlayerFlipping(m_aimDirection.x < 0);
            
            if (m_shootAction.WasPressedThisFrame())
            {
                m_weapon.Shoot(GetShootDirection());    
            }
        }

        private Vector2 GetShootDirection()
        {
            var worldPos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            return (worldPos - m_weapon.transform.position).normalized;
        }

        private void OnPlayerFlipping(bool isFlipped)
        {
            var curLocalPos = m_weaponPivot.localPosition;
            curLocalPos.x = isFlipped ? m_flipValue.x : -m_flipValue.x;
            m_weaponPivot.localPosition = curLocalPos;
            
            m_weapon.FlipWeapon(isFlipped);
            
            m_playerAnimationController.OnFlippingAnim?.Invoke(isFlipped);
        }
    }
}
