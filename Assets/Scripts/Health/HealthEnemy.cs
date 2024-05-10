using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                // Disable collider on current GameObject
                Collider2D ownCollider = GetComponent<Collider2D>();
                if (ownCollider != null)
                {
                    ownCollider.enabled = false;
                }

                // Disable collider on parent GameObject
                Collider2D parentCollider = transform.parent.GetComponent<Collider2D>();
                if (parentCollider != null)
                {
                    parentCollider.enabled = false;
                }

                // Disable specific enemy components
                EnemyPatrol enemyPatrol = GetComponentInParent<EnemyPatrol>();
                if (enemyPatrol != null)
                {
                    enemyPatrol.enabled = false;
                }

                MeleeEnemy meleeEnemy = GetComponent<MeleeEnemy>();
                if (meleeEnemy != null)
                {
                    meleeEnemy.enabled = false;
                }

                dead = true;
            }
        }
    }
}
