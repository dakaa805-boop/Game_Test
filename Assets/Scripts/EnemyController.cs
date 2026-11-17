using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //basic status enemy
    [Header("status")]
    public float health = 20;
    public float attack = 5;
    public Transform attackTarget;

    [Header("Configuration")]
    [SerializeField] private float moveSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        Movement();
        FallDie();
    }
    protected virtual void Movement()
    {

    }
    //fungsi untuk mengurangi HP enemy dri damge yang diterima
    public void DamageBy(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }

    }
    void FallDie()
    {
        if (transform.position.y < -20)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
    public float GetAttackDamage()
    {
        return attack;

    }
    //fungsi untuk mentrigger collider yang menyentuhnya
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet.targetTag == "Enemy")
            {
                float damage = bullet.GetDamage();
                DamageBy(damage);
                Destroy(collision.gameObject);
            }
        }
    }
}