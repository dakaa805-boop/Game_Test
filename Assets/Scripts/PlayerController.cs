using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Properties")]
    [SerializeField] private SpriteRenderer graphic;

    [Header("Status")]
    public float health = 100;
    public float attack = 5;
    public bool canBeMoved = true;
    public float healthMax { private set; get; }
    private bool isShooting = true;

    [Header("Configuration")]
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float shootingTimeMax = 1.0f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private Vector2 gunOffset;
    private float shootingTime = 0;
    private Rigidbody2D rb2;

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        healthMax = health;
        shootingTime = shootingTimeMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeMoved)
        {
            MovementController();
            ShootController();
        }
    }

    void MovementController()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 direction = rb2.velocity;
        direction.x = x * moveSpeed;
        rb2.velocity = direction;

        //Gambar Hadap Kanan Kiri
        if (direction.x < 0)
        {
            graphic.flipX = true;
        }
        else if (direction.x > 0)
        {
            graphic.flipX = false;
        }
    }

    void ShootController()
    {
        isShooting = Input.GetKey(KeyCode.Z);
        if (isShooting)
        {
            shootingTime -= Time.deltaTime;
        }
        else
        {
            shootingTime = shootingTimeMax;
        }
        if (isShooting && shootingTime < 0)
        {
            shootingTime = shootingTimeMax;
            Shoot();
        }
    }
    void Shoot()
    {
        int direction = (graphic.flipX == false ? 1 : -1);
        Vector2 gunPos = new Vector2(gunOffset.x * direction * transform.position.x, gunOffset.y + transform.position.y);
        GameObject bulletObj = Instantiate(bulletPrefab, gunPos, Quaternion.identity);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        if (bullet)
        {
            bullet.Launch(new Vector2(direction, 0), "Enemy", bulletSpeed, attack);
        }
    }
}