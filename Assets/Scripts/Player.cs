using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private GameObject butlletPrefab;
    [SerializeField] private GameObject BloodPre;
    [SerializeField] private Transform gunPoint;

    private Animator anim;
    private Rigidbody2D player;
    private Vector2 input;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.velocity = input.normalized * speed;

        transform.right = (MousePos.GetMouse() - (Vector2)transform.position).normalized;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        SetAnim();
        
    }

    private void SetAnim()
    {
        anim.SetBool("move",input.x != 0 || input.y != 0);
    }

    private void Shoot()
    {
        anim.SetTrigger("shoot");
        Instantiate(butlletPrefab,gunPoint.position,transform.rotation);
    }

    public void Die()
    {
        Instantiate(BloodPre, transform.position, Quaternion.identity);
        
        FindObjectOfType<TMP_Text>().enabled = true;
        Destroy(gameObject);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Zombie = collision.collider.GetComponent<Enemy>();

        if (Zombie != null)
        {
            Die();
        }
    }
}
