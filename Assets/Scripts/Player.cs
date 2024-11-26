using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Player : Charactor
{
    [SerializeField] private bool isFacingRight = true;
    public event Action OnStatsChanged;

    [Header("Player Attack Status")] 
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackRange;
    [SerializeField] public float attackCooldown = 1f;
    [SerializeField] public float nextAttackTime = 1f;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Level")]
    [SerializeField] public int level = 1;
    [SerializeField] public int currentExp = 0;
    [SerializeField] public int maxExp = 10;
    private Dictionary<int, int> expTable = new Dictionary<int, int>()
    {
        { 1, 10 },
        { 2, 20 },
        { 3, 30 },
        { 4, 50 },
        { 5, 200 }
    };
    
    [Header("Card")]
    public CardSelectionSystem cardSelectionSystem;

    public bool IsFacingRight
    {
        get { return isFacingRight;} 
        private set {
            if (isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            isFacingRight = value; }
    }

    void Start()
    {
        CurHP = MaxHP;
        UpdateMaxExp();
        OnStatsChanged?.Invoke();
    }
    
    void Update()
    {
        Move();
        SetfacingDirection(MovementInput.x);
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        if (hitEnemies.Length > 0)
        {
            Attack(hitEnemies[0].gameObject);
        }
        
        PlayerDie();
    }
    
    private void SetfacingDirection(float moveInput)
    {
        if (moveInput > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }

        if (moveInput < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    
    private void Attack(GameObject enemy)
    {
        GameObject nearestEnemy = FindNearEnemy();
        if (nearestEnemy == null) return;
        
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            State = CharactorState.Attack;
            
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.GetComponent<FireBall>().Owner = this;
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            fireball.GetComponent<Rigidbody2D>().velocity = direction * attackSpeed;
            
            fireball.transform.SetParent(GameObject.Find("Bullets").transform);
            fireball.gameObject.tag = "Bullet";
        }
    }
    
    private GameObject FindNearEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearEnemy = null;
        float shortDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortDistance)
            {
                shortDistance = distance;
                nearEnemy = enemy;
            }
        }

        return nearEnemy;
    }
    
    void UpdateMaxExp()
    {
        if (expTable.ContainsKey(level))
        {
            maxExp = expTable[level];
        }
    }
    
    public void GainExp(int amount)
    {
        currentExp += amount;
        Debug.Log($"Gained {amount} EXP. Current EXP: {currentExp}/{maxExp}");

        while (currentExp >= maxExp)
        {
            LevelUp();
        }
        
        OnStatsChanged?.Invoke();
    }
    
    void LevelUp()
    {
        currentExp -= maxExp;
        level++;
        Debug.Log($"Level Up Current Level: {level}");

        if (expTable.ContainsKey(level))
        {
            UpdateMaxExp();
        }
        else
        {
            Debug.Log("Max Level");
            currentExp = 0;
        }
        
        OnStatsChanged?.Invoke();
        cardSelectionSystem.ShowCardSelection();
    }
    
    private void PlayerDie()
    {
        if (CurHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}