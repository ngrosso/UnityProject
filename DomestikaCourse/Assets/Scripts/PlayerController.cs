using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2f; //velocidad del player
    public float jumpForce = 5f;

    public Transform groundCheck; //checkea el juego, el floorpoint
    public LayerMask groundLayer; //nos va a permitir que layer es el suelo
    public float groundCheckRadius; //

    //References
    private Rigidbody2D _rigidbody; //para moverlo
    private Animator _animator; //para animar

    //movement
    private Vector2 _movement; //vector de movimiento para mover al player
    private bool _facingRight = true;
    private bool _isGrounded;

    //Attack
    private bool _isAttacking;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame, best for calling input from the player
    void Update()
    {
        Debug.Log("attacking? " + _isAttacking);

        if (!_isAttacking)
        {
            /*
            El personaje va a tener input lag en las pruebas, para revisar por que es esto
            en project settings -> input -> sensibilidad y gravedad  esta definido la variacion
            para solucionar esto en el codigo se reemplaza
            Input.GetAxis -> Input.GetAxisRaw
            */
            //movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            //flip character
            if (horizontalInput < 0f && _facingRight)
            {
                Flip();
            }
            else if (horizontalInput > 0f && !_facingRight)
            {
                Flip();
            }
        }

        //Is grounded?
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //checkea si esta tocando el suelo

        //Is Jumping?
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //wanna attack?
        if (Input.GetButtonDown("Fire1") && _isGrounded && !_isAttacking)
        {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("attack");
        }
    }

    //Para mover cualquier objeto fisico en unity
    void FixedUpdate()
    {
        if (!_isAttacking)
        {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y); //(velocidad el juegador, velocidad actual del rigidbody)
        }
    }

    //Ultimo update justo antes de renderizar en pantalla, aqui se usa el codigo de animaciones
    void LateUpdate()
    {
        _animator.SetBool("isIdle", _movement == Vector2.zero);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("verticalVelocity", _rigidbody.velocity.y);

        //animator cuando cierto animator tiene un tag (en esta caso saber si esta en idle)
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
        {
            _isAttacking = true;
        }
        else
        {
            _isAttacking = false;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x; //obtiene la escala
        localScaleX = localScaleX * -1f; //le pone un -1 a la escala y epeja el sprite
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z); //reemplaza el valor del localScale
    }
}
