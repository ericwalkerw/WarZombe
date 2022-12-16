using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float radius;

    [SerializeField] private GameObject Eplode;
    [SerializeField] private Transform attackZone;
    [SerializeField] private LayerMask layerName;

    private Player player;
    private float speed;
    private float cooldownTimer = Mathf.Infinity;

    private Rigidbody2D skeleton;
    private Animator anim;

    private bool isInAttackZone;
    

    // Start is called before the first frame update
    private void Start()
    {
        skeleton = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        speed = baseSpeed * (1 + Random.Range(-0.1f, 0.1f));
    }

    // Update is called once per frame
    private void Update()
    {       
        if (player != null) 
        {
            cooldownTimer += Time.deltaTime;
            Attack();
            transform.up = (player.transform.position - transform.position).normalized * -1;
            skeleton.velocity = transform.up * speed * -1;
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
        Instantiate(Eplode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.tag == "Player" && cooldownTimer > attackCooldown)
        {           
            cooldownTimer = 0;
            collision.collider.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
