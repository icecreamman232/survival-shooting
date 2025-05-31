using System;
using UnityEngine;

namespace SGGames.Script.Animations
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private static readonly int IsRunningAnimParam = Animator.StringToHash("Is_Running");

        public Action<bool> OnRunningAnim;
        public Action<bool> OnFlippingAnim;

        private void Start()
        {
            OnRunningAnim += UpdateRunningAnim;
            OnFlippingAnim += FlipPlayerSprite;
        }
        
        private void UpdateRunningAnim(bool isRunning)
        {
            m_animator.SetBool(IsRunningAnimParam, isRunning);
        }
        
        private void FlipPlayerSprite(bool isFlipped)
        {
            m_spriteRenderer.flipX = isFlipped;
        }

        private void OnDestroy()
        {
            OnRunningAnim -= UpdateRunningAnim;
            OnFlippingAnim -= FlipPlayerSprite;
        }
    }
}

