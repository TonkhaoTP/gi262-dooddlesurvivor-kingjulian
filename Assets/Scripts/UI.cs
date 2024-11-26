using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text playerHpText;
    [SerializeField] private TMP_Text playerLevelText;
    [SerializeField] private TMP_Text playerExpText;
    
    [SerializeField] private TMP_Text playerDamageText;
    [SerializeField] private TMP_Text playerAttackRangeText;
    [SerializeField] private TMP_Text playerCooldownText;

    private Player p;
    
    void Start()
    {
        p = FindObjectOfType<Player>();
        p.OnStatsChanged += UpdatePlayerStatsUI;
    }

    public void UpdatePlayerStatsUI()
    {
        if (playerHpText != null && p != null)
        {
            playerHpText.text = $"HP: {Mathf.FloorToInt(p.CurHP)}/{Mathf.FloorToInt(p.MaxHP)}";
        }
        if (playerLevelText != null && p != null)
        {
            playerLevelText.text = $"Level: {p.level}";
        }
        if (playerExpText != null && p != null)
        {
            playerExpText.text = $"EXP: {p.currentExp}/{p.maxExp}";
        }
        
        if (playerDamageText != null && p != null)
        {
            playerDamageText.text = $"Damage: {p.Damage.ToString("0.00")}";
        }
        if (playerAttackRangeText != null && p != null)
        {
            playerAttackRangeText.text = $"Range: {p.attackRange.ToString("0.00")}";
        }
        if (playerCooldownText != null && p != null)
        {
            playerCooldownText.text = $"CD: {p.attackCooldown.ToString("0.00")}";
        }
    }
}
