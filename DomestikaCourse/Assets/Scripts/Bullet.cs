using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public int damage = 1;

    public float livingTime = 3f;
    public Color initialColor = Color.white;
    public Color finalColor;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;

    private float _startingTime;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //  Save initial time
        _startingTime = Time.time;

        // Destroy the bullet after some time
        Destroy(gameObject, livingTime);
    }

    // Update is called once per frame

    void Update()
    {
        // Change bullet's color over time
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }

    private void FixedUpdate()
    {
        //  Move object
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Manera 1: depender de saber a QUIEN estas afectando
            //collision.gameObject.GetComponent<PlayerHealth>().AddDamage(1);
            //Manera 2: no le importa a que objeto afecte, sino que busca directo el metodo
            //SendMessageUpwards, envia el mensaje hacia su padre si no encuentra el mensaje 
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }
    }
}
