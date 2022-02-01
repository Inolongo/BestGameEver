using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Enemy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float attackRange;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    public event Action<float> HealthChanged;
    public float Health { get; private set; } = 100f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isOnGround = true;
    private bool _isRun;
    private static readonly int VelocityY = Animator.StringToHash("velocityY");
    private static readonly int IsRun = Animator.StringToHash("isRun");
    private static readonly int IsAttack = Animator.StringToHash("isAttack");
    private static readonly int IsDash = Animator.StringToHash("isDash");
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private float _axisX;
    private EnemyPrototype _enemy;


    private void Start()
    {
        //GetComponent<SpriteRenderer>().color = Color.yellow; //for test

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        Jump();
        Attack();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }

        if (Health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("ground");
            _isOnGround = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_enemy == null && other.TryGetComponent(typeof(EnemyPrototype), out var enemy))
        {
            _enemy = enemy as EnemyPrototype;
        }
    }

    public void StopMovementWhenDialogStarted()
    {
        _animator.SetBool(IsRun, false);
        _animator.SetBool(IsAttack, false);
        _animator.SetFloat(VelocityY, 0);
        enabled = false;
    }

    public void StartMovementWhenDialogEnded()
    {
        enabled = true;
    }

    private void Run()
    {
        _axisX = Input.GetAxis("Horizontal");
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.position = new Vector2(transform.position.x + (_playerSpeed * _axisX * Time.deltaTime),
                transform.position.y);
        }


        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("run");
            _isRun = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = true;

            _isRun = true;
        }
        else
        {
            _isRun = false;
        }

        _animator.SetBool(IsRun, _isRun);
        _animator.SetFloat(VelocityY, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            Debug.Log("jump");
            _isOnGround = false;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log(_rigidbody.velocity);

        if (GetComponent<SpriteRenderer>().flipX)
        {
            _rigidbody.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
            _animator.SetBool(IsDash, true);
        }
        else
        {
            _rigidbody.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);
            _animator.SetBool(IsDash, true);
        }

        yield return new WaitForSeconds(dashDuration);
        _animator.SetBool(IsDash, false);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Debug.Log("attack");
            _animator.SetBool(IsAttack, true);
            if (_enemy != null && Vector2.Distance(transform.position, _enemy.transform.position) < attackRange)
            {
                _enemy.TakeDamage(damage);
            }
        }

        else
        {
            _animator.SetBool(IsAttack, false);
        }
    }

    public void ChangeHealth(float changedHealthPoints)
    {
        if (Health + changedHealthPoints > 100)
        {
            Health = 100;
        }
        else
        {
            Health += changedHealthPoints;
        }

        if (changedHealthPoints < 0)
        {
            _animator.SetTrigger("takeDamage");
        }

        HealthChanged?.Invoke(Health);
        Debug.Log(Health);
    }

    private void Death()
    {
        _animator.SetBool(IsDead, true);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(GetComponent<Rigidbody2D>());
        //Destroy(this);
    }
}