using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    // Sound effect
    [SerializeField] AudioSource deathSoundEffect;
    [SerializeField] private float damage;
    [SerializeField] private float startingHealth;

    private Animator anim1;
    private Rigidbody2D rb;

    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private float restartDelay = 3f; // Adjust this value as needed


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim1 = GetComponent<Animator>();
    }

    public bool IsDead()
    {
        return dead;
    }

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
                dead = true;
                Debug.Log("Player died!");
                Die();
            }
        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        dead = true; // Mark the player as dead

        // Disable the PlayerMovement script
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Disable other components or actions related to movement, jumping, dashing, etc.
        // If there are other relevant components, disable them here.

        anim.SetTrigger("death");
        Invoke("Restartlevel", restartDelay);
    }



    private void Restartlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}