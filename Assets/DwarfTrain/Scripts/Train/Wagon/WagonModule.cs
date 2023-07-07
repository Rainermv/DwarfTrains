using UnityEngine;

namespace Assets.DwarfTrain.Scripts.Train.Wagon
{
    public interface IWagonModule
    {
        
    }

    class ShooterWagonModule : IWagonModule
    {
        public Transform projectilePrefab; // The projectile prefab to be fired
        public float projectileSpeed = 10f; // The speed of the projectile
        public float fireRate = 1f; // The rate of fire of the wagon
        public float range = 5f; // The range of the wagon
        public float projectileArcHeight = 1f; // The height of the projectile's arc

        private float lastFireTime = 0f; // The time the wagon last fired
        private Transform target; // The target to be attacked

        public void UpdateModule()
        {
            // Check if the wagon can fire
            if (Time.time - lastFireTime > 1f / fireRate)
            {
                // Find the nearest target in range
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance && distance <= range)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy.transform;
                    }
                }

                // Fire at the target
                if (closestEnemy != null)
                {
                    target = closestEnemy;
                    Fire();
                }
            }
        }

        private void Fire()
        {
            // Calculate the direction of the projectile
            Vector3 targetPosition = target.position;
            Vector3 direction = targetPosition - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            // Calculate the velocity of the projectile
            float projectileTime = distance / projectileSpeed;
            float verticalVelocity = (projectileArcHeight - 0.5f * Physics.gravity.y * projectileTime * projectileTime) / projectileTime;
            Vector3 velocity = direction * projectileSpeed + Vector3.up * verticalVelocity;

            // Instantiate the projectile and set its velocity
            Transform projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = velocity;

            // Reset the fire time
            lastFireTime = Time.time;
        }
    }
}