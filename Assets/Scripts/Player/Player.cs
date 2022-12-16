using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;  
    [SerializeField] private float reloadTime;
    [SerializeField] private int maxAmmo;

    [SerializeField] private GameObject butlletPrefab;
    [SerializeField] private GameObject BloodPre;
    [SerializeField] private GameObject fireBurst;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private TMP_Text amountBullet;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip reloadSound;


    private Animator anim;
    private Rigidbody2D player;
    
    private Vector2 input;

    private float cooldownTimer = Mathf.Infinity;

    private int currentAmmo;

    private bool isReadloaing = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (currentAmmo == -1)
            currentAmmo = maxAmmo;
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        amountBullet.text = currentAmmo + "/" + maxAmmo;
        if (isReadloaing)
        {
            return;
        }

        cooldownTimer += Time.deltaTime;

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        player.velocity = input.normalized * speed;

        transform.right = (MousePos.GetMouse() - (Vector2)transform.position).normalized;
        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reloading());
            return;
        }
        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown)
        {
            Shoot();      
        }
        SetAnim();       
    }

    IEnumerator Reloading()
    {
        isReadloaing = true;
        SoundManeger.instance.PlaySound(reloadSound);
        anim.SetTrigger("reload");
        yield return new WaitForSeconds(reloadTime);
     
        currentAmmo = maxAmmo;
        isReadloaing = false;
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
        currentAmmo--;
    }

    public void Die()
    {    
        Instantiate(BloodPre, transform.position, Quaternion.identity);       
        Destroy(gameObject);       
    }
}
