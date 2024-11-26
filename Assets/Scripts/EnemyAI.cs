using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : EnemyStatus
{
    /*[SerializeField] private GameObject player;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float distanceBetween = 0f;
    private float distance;
    private NavMeshAgent agentEnemy;

    void Start()
    {
        agentEnemy = GetComponent<NavMeshAgent>();

        agentEnemy.updateRotation = false;
        agentEnemy.updateUpAxis = false;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            transform.position =
                Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }


    }*/
    
    [SerializeField] private CharactorState state;
    [SerializeField] private Rigidbody2D rbEnemy;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    private float lastAttackTime = 0.0f;
    
    private GameObject player;
    private NavMeshAgent agentEnemy;
    
    void Start()
    {
        CurHP = MaxHP;
        
        agentEnemy = GetComponent<NavMeshAgent>();
        rbEnemy = GetComponent<Rigidbody2D>();
        
        agentEnemy.updateRotation = false;
        agentEnemy.updateUpAxis = false;

        player = GameObject.Find("Player");
    }

    void Update()
    {
        agentEnemy.SetDestination(player.transform.position);
        
        if (rbEnemy.velocity == Vector2.zero)
        {
            state = CharactorState.Idle;
        }
        else if (rbEnemy.velocity != Vector2.zero)
        {
            state = CharactorState.Move;
        }
        
        EnemyDie();
        
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
        {
            //state = CharacterState.Attack;
            
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }
    
    private void AttackPlayer()
    {
        Player player = FindObjectOfType<Player>();
        player.CurHP -= Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Charactor charactor = collision.gameObject.GetComponent<FireBall>().Owner;
            if (charactor != null)
            {
                CurHP -= charactor.Damage;
            }
        }
    }

    private void EnemyDie()
    {
        if (CurHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnDestroy()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.GainExp(ExpDrop);
        }
    }
}
