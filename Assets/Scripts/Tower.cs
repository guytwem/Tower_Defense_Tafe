using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TowerDefence.Towers
{
    public abstract class Tower : MonoBehaviour
    {
        #region tower properties
        public string TowerName { get => towerName; } // accessor for towerName Variable

        public string Description { get => description; }// accessor for description Variable

        public int Cost { get => cost; }// accessor for cost Variable

        public float MaximumRange { get { return maximumRange; } }//max range the tower can reach

        public float Damage { get { return damage; } }//damage the tower does

        protected Enemy TargetedEnemy { get { return target; } }//the enemy our turret is targeting

        #endregion

        [Header("Tower Info")]
        [SerializeField]
        private string towerName = "";
        [SerializeField, TextArea]
        private string description = "";
        [SerializeField, Range(1, 10)]
        public static int cost = 1;

        [Header("Tower Stats")]
        [SerializeField, Min(0.1f)]
        private float damage = 1;
        [SerializeField, Min(0.1f)]
        private float minimumRange = 1;
        [SerializeField]
        private float maximumRange = 5;
        [SerializeField, Min(0.1f)]
        protected float fireRate = 0.1f;
        [SerializeField]
        private float health = 100;

        public float currentTime = 0;

        private Enemy target = null;

        private void Target()
        {
            Enemy[] closeEnemies = EnemyManager.instance.EnemiesInRange(transform, MaximumRange, minimumRange);

            target = GetClosestEnemy(closeEnemies);

        }

        private Enemy GetClosestEnemy(Enemy[] enemies)
        {
            float closestDistance = float.MaxValue;
            Enemy closest = null;

            foreach (Enemy enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closest = enemy;
                }
            }

            return closest;
        }

        private void Fire()
        {
            if (target != null)
            {
                target.Damage(Damage);

                RenderAttackVisuals();
            }
        }

        private void FireWhenReady()
        {
            if (target != null)
            {
                if (currentTime < fireRate)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    currentTime = 0;
                    Fire();
                }
            }
        }

        protected abstract void RenderAttackVisuals();

        protected virtual void Update()
        {
            Target();
            FireWhenReady();
        }

        private void OnDrawGizmosSelected()
        {
            // Draw a mostly transparent red sphere indicating the minimum range
            Gizmos.color = new Color(1, 0, 0, 0.25f);
            Gizmos.DrawSphere(transform.position, minimumRange);



            // Draw a mostly transparent blue sphere indicating the maximum range
            Gizmos.color = new Color(0, 0, 1, 0.25f);
            Gizmos.DrawSphere(transform.position, MaximumRange);
        }
    }
}
