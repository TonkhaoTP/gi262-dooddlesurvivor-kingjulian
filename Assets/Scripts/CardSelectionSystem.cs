using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public string cardName;
    public Sprite cardImage;
    public enum CardType { AttackRange, AttackCooldown, MaxHP, Damage }
    public CardType cardType;
    public float percentageIncrease;
}

public class CardSelectionSystem : MonoBehaviour
{
    [SerializeField] private List<Card> allCards;
    [SerializeField] private Transform cardSelectionPanel;
    [SerializeField] private GameObject cardButtonPrefab;

    private List<Card> selectedCards = new List<Card>();

    public void ShowCardSelection()
    {
        selectedCards = new List<Card>(RandomlySelectCards(3));
        
        foreach (Transform child in cardSelectionPanel)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Card card in selectedCards)
        {
            GameObject cardButton = Instantiate(cardButtonPrefab, cardSelectionPanel);
            CardButton buttonComponent = cardButton.GetComponent<CardButton>();
            buttonComponent.Setup(card, OnCardSelected);
        }

        cardSelectionPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnCardSelected(Card selectedCard)
    {
        ApplyCardEffect(selectedCard);
        cardSelectionPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void ApplyCardEffect(Card card)
    {
        Player player = FindObjectOfType<Player>();
        switch (card.cardType)
        {
            case Card.CardType.AttackRange:
                player.attackRange += player.attackRange * card.percentageIncrease / 100;
                break;
            case Card.CardType.AttackCooldown:
                player.attackCooldown -= player.attackCooldown * card.percentageIncrease / 100;
                break;
            case Card.CardType.MaxHP:
                player.MaxHP += player.MaxHP * card.percentageIncrease / 100;
                player.MaxHP = Mathf.Floor(player.MaxHP);
                player.CurHP = player.MaxHP;
                break;
            case Card.CardType.Damage:
                player.Damage += player.Damage * card.percentageIncrease / 100;
                break;
        }
    }

    private List<Card> RandomlySelectCards(int count)
    {
        List<Card> selected = new List<Card>();
        List<Card> pool = new List<Card>(allCards);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            selected.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }

        return selected;
    }
}
