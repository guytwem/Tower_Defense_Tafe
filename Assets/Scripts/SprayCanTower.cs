using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;
using TowerDefence.Utilities;

namespace TowerDefence.Towers
{
    public class SprayCanTower : Tower
    {
        [SerializeField] private Transform sprayCanTop;
        [SerializeField] private Transform firePoint;
        [SerializeField] private LineRenderer attackVisual;

        private bool hasResetVisuals = false;

        protected override void RenderAttackVisuals()
        {
            MathUtils.DistanceAndDirection(out float distance, out Vector3 direction, sprayCanTop, TargetedEnemy.transform);
            sprayCanTop.rotation = Quaternion.LookRotation(direction);

            RenderBulletLine(firePoint);
            hasResetVisuals = false;
        }

        protected override void Update()
        {
            base.Update();

            if (TargetedEnemy == null && !hasResetVisuals)
            {
                if (currentTime < fireRate)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    attackVisual.positionCount = 0;
                    currentTime = 0;
                    hasResetVisuals = true;
                }
            }
        }

        private void RenderBulletLine(Transform start)
        {
            attackVisual.positionCount = 2;
            attackVisual.SetPosition(0, start.position);
            attackVisual.SetPosition(1, TargetedEnemy.transform.position);
        }

    }
}
