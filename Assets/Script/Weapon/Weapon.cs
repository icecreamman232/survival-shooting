using System;
using System.Collections;
using SGGames.Script.Common;
using UnityEngine;

namespace SGGames.Script.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_model;
        [SerializeField] private ObjectPooler m_bulletPooler;
        [SerializeField] private Transform m_shootPivot;
        [SerializeField] private float m_delayBetweenTwoShots;
        [SerializeField] private Vector2 m_shootPivotFlipValue;

        private bool m_canShot;
        private WaitForSeconds DelayAfterShot;
        private Coroutine DelayAfterShotCoroutine;


        private void Start()
        {
            DelayAfterShot = new WaitForSeconds(m_delayBetweenTwoShots);
            m_canShot = true;
        }

        public void Shoot(Vector2 shootDirection)
        {
            if (!m_canShot) return;
            var bulletGO = m_bulletPooler.GetPooledObject();
            var bullet = bulletGO.GetComponent<Bullet>();
            bullet.Spawn(m_shootPivot.position, shootDirection);
            bulletGO.SetActive(true);
            if (DelayAfterShotCoroutine == null)
            {
                DelayAfterShotCoroutine = StartCoroutine(OnDelayAfterShot());;
            }
            else
            {
                StopCoroutine(DelayAfterShotCoroutine);
            }
        }

        private IEnumerator OnDelayAfterShot()
        {
            m_canShot = false;
            yield return DelayAfterShot;
            
            m_canShot = true;
        }
        
        public void FlipWeapon(bool isFlipped)
        {
            m_model.flipX = isFlipped;
            var curPos = m_shootPivot.localPosition;
            curPos.x = isFlipped ? m_shootPivotFlipValue.x : -m_shootPivotFlipValue.x;
            m_shootPivot.localPosition = curPos;
        }
    }
}
