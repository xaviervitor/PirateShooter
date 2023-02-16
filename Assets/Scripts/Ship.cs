using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {
    [SerializeField]
    protected float Speed = 4;
    [SerializeField]
    protected float Torque = 90;
    [SerializeField]
    private GameObject Fire;
    [SerializeField]
    private GameObject Smoke;
    [SerializeField]
    private List<Sprite> Sprites;
    [SerializeField]
    private GameObject DeathExplosionPrefab;

    public delegate void HealthChangedDelegate(float health, float maxHealth);
    public event HealthChangedDelegate HealthChangedEvent;

    public delegate void ShipDestroyedDelegate();
    public event ShipDestroyedDelegate ShipDestroyedEvent;

    protected new Rigidbody2D rigidbody;
    protected SpriteRenderer spriteRenderer;

    protected float health = 4f;
    public float maxHealth = 4f;

    protected float currentSpeed = 0;
    protected float currentTorque = 0;

    void OnEnable() {
        ShipDestroyedEvent += DestroyShip;
    }

    void OnDisable() {
        ShipDestroyedEvent -= DestroyShip;        
    }

    protected void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void FixedUpdate() {
        rigidbody.angularVelocity = currentTorque;
        rigidbody.velocity = -transform.up * currentSpeed;
    }

    private void UpdateSprite(float health) {
        spriteRenderer.sprite = Sprites[Mathf.CeilToInt(health - 1)];
        if (health == 1) {
            Smoke.SetActive(true);
        }
    }
    
    public void DestroyShip() {
        Instantiate(DeathExplosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            if (ShipDestroyedEvent != null) {
                ShipDestroyedEvent();
            }
        } else {
            UpdateSprite(health);
        }
        if (HealthChangedEvent != null) {
            HealthChangedEvent(health, maxHealth);
        }
    }
}
