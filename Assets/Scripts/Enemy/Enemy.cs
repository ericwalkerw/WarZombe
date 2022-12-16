using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float radius;

    [SerializeField] private GameObject BloodPre;
    [SerializeField] private Transform attackZone;
    [SerializeField] private LayerMask layerName;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip runSound;


    private Player player;
    private float speed;
    private float cooldownTimer = Mathf.Infinity;

    private Rigidbody2D Zombie;
    private Animator anim;

    private bool isInAttackZone;
    

    // Start is called before the first frame update
    private void Start()
    {
        Zombie = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        speed = baseSpeed * (1 + Random.Range(-0.1f, 0.1f));
        SoundManeger.instance.PlaySound(runSound);
    }

    // Update is called once per frame
    private void Update()
    {       
        if (player != null) 
        {
            cooldownTimer += Time.deltaTime;
            Attack();
            transform.right = (player.transform.position - transform.position).normalized;
            Zombie.velocity = transform.right * speed;
            SetAnim();
        }
    }
    private void Attack()
    {       
        isInAttackZone = Physics2D.OverlapCircle(attackZone.position, radius, layerName);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackZone.position, radius);
    }
    private void SetAnim()
    {
        if (isInAttackZone)
        { 
            anim.SetTrigger("attack");
        }
    }
    public void Die()
    {
        SoundManeger.instance.PlaySound(deathSound);
        Instantiate(BloodPre, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.tag == "Player" && cooldownTimer > attackCooldown)
        {           
            cooldownTimer = 0;
            SoundManeger.instance.PlaySound(attackSound);
            collision.collider.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
