using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharactorState
{
    Idle,
    Move,
    Attack,
    Die
}

public class Charactor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Movement")]
    [SerializeField] private int moveSpeed = 5;
    public int MoveSpeed { get { return moveSpeed; } }
    
    private Vector2 movementInput;
    public Vector2 MovementInput { get { return movementInput; } }
    
    [Header("Charaactor Status")]
    [SerializeField] private float maxHP;
    public float MaxHP { get { return maxHP; } set { maxHP = value; } }
    
    [SerializeField] private float curHP;
    public float CurHP { get { return curHP; } set { curHP = value; } }
    
    /*[SerializeField] private int level;
    public int Level { get { return level; } }
    
    [SerializeField] private int exp;
    public int Exp { get { return exp; } }*/
    
    [SerializeField] private float damage;
    public float Damage { get { return damage; } set { damage = value; }}
    
    [Header("Charactor State")]
    [SerializeField] private CharactorState state;
    public CharactorState State { get { return state; } set { state = value; } }
    public bool isAlive;
    public bool isMove;
    
    [Header("NavAgent")]
    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get { return navAgent; } }
    
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        
    }
    
    public void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(horizontalInput, verticalInput);
        movementInput.Normalize();

        rb.velocity = movementInput * moveSpeed;
        
        if (rb.velocity == Vector2.zero)
        {
            state = CharactorState.Idle;
            Debug.Log("isIde");
        }
        else if (rb.velocity != Vector2.zero)
        {
            state = CharactorState.Move;
            Debug.Log("isMove");
        }
    }
}
