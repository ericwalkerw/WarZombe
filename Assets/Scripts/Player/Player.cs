using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;  

    [SerializeField] private GameObject butlletPrefab;
    [SerializeField] private GameObject BloodPre;
    [SerializeField] private GameObject fireBurst;

    [SerializeField] private Transform gunPoint;
    [SerializeField] private AudioClip shootSound;


    private Animator anim;
    private Rigidbody2D player;
    
    private Vector2 input;

    private float cooldownTimer = Mathf.Infinity;

    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.velocity = input.normalized * speed;

        transform.right = (MousePos.GetMouse() - (Vector2)transform.position).normalized;

        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown )
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
        SoundManeger.instance.PlaySound(shootSound);
        cooldownTimer = 0;
        anim.SetTrigger("shoot");
        Instantiate(butlletPrefab, gunPoint.position, transform.rotation);
        Instantiate(fireBurst, gunPoint.position, transform.rotation);
    }
    public void Die()
    {    
        Instantiate(BloodPre, transform.position, Quaternion.identity);       
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }
}
