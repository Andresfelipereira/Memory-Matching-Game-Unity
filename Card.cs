using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card: MonoBehaviour
{

    public string cardName;
    public Image cardImage;
    public Image hideCard;
    public bool foundMatch = false;

    public void SetCard(Sprite sprite) {cardImage.sprite = sprite; cardName = sprite.name; }

    public void SelectCard()
    {
        hideCard.enabled = false;
        this.GetComponent<Button>().enabled = false;
    }

    public void HideCard() { hideCard.enabled = true; this.GetComponent<Button>().enabled = true; }

}
