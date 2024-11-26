using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    [SerializeField] private Image cardImage;
    [SerializeField] private Button cardButton;
    private Card card;

    public void Setup(Card card, System.Action<Card> onClickAction)
    {
        this.card = card;
        cardImage.sprite = card.cardImage;
        cardButton.onClick.AddListener(() => onClickAction.Invoke(card));
    }
}
