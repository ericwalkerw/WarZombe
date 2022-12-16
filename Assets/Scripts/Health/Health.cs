using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    public float currentHealth { get; private set; }

    private Animator anim;
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();      
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManeger.instance.PlaySound(hurtSound);            
        }
        else
        {
            player.Die();
            SoundManeger.instance.PlaySound(deathSound);
        }
    }
    public void AddHeal(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}
