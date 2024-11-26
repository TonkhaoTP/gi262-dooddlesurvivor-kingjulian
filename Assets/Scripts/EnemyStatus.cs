using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField] private int maxHP;
    public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    
    [SerializeField] private float curHP;
    public float CurHP { get { return curHP; } set { curHP = value; } }
    
    [SerializeField] private int expDrop;
    public int ExpDrop { get { return expDrop; } }
    
    [SerializeField] private int damage;
    public int Damage { get { return damage; } }
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
