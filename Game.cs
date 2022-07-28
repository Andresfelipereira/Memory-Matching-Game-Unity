using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Card[] cards;
    public List<Sprite> sprites;
    public string firstSelectedCard;
    public string lastSelectedCard;
    int numSelectedCards;
    int attempts;
    float time;
    public GameObject resultScreen;
    public TextMeshProUGUI attemptsTxt;
    public TextMeshProUGUI timerTxt;
    public TextMeshProUGUI attemptResultTxt;
    public TextMeshProUGUI timeResultTxt;

    void Start()
    {
        resultScreen.SetActive(false);
        firstSelectedCard = "";
        lastSelectedCard = "";
        numSelectedCards = 0;
        SetCardSprite();
    }

    void Update()
    {
        attemptsTxt.text = "Intentos: " + attempts;
        if (!FoundAllMatches())
        {
            DisplayTime(time);
            time += Time.deltaTime;
        }
    }

    public void SelectCard(Card card)
    {
        card.SelectCard();
        if(numSelectedCards == 0)
        {
            numSelectedCards++;
            firstSelectedCard = card.cardName;
        }
        else if(numSelectedCards == 1)
        {
            lastSelectedCard = card.cardName;
            StartCoroutine(CompareCards());
        }
    }

    IEnumerator CompareCards()
    {
        DisableCards();
        yield return new WaitForSeconds(2f);
        if (FoundMatch())
        {
            FindMatchList(firstSelectedCard);
        }
        else { 
            HideCards();
        }
        attempts++;
        if (!FoundAllMatches())
        {
            numSelectedCards = 0;
            firstSelectedCard = "";
            lastSelectedCard = "";
            EnableCards();
        }
        else
        {
            resultScreen.SetActive(true);
            attemptResultTxt.text = "Total de intentos: " + attempts;
            timeResultTxt.text = "Tiempo total: " + (string.Format("{0:00}:{1:00}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60)));
        }
    }
    
    bool FoundAllMatches()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            if(cards[i].foundMatch == false)
            {
                return false;
            }
        }   
        return true;
    }

    bool FoundMatch()
    {
        if(firstSelectedCard == lastSelectedCard)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    void SetCardSprite()
    { 
            for (int i = 0; i < cards.Length; i++)
            {
                int selectedSprite = Random.Range(0, sprites.Count);
                cards[i].SetCard(sprites[selectedSprite]);
                sprites.RemoveAt(selectedSprite);
            }
    }

    void HideCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (!cards[i].foundMatch)
            {
                cards[i].hideCard.enabled = true;
            }
        }
    }

    void EnableCards()
    {
        for(int i = 0; i < cards.Length; i++)
        {
            if (!cards[i].foundMatch)
            {
                cards[i].GetComponent<Button>().enabled = true;
            }
        }
    }

    void DisableCards()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].GetComponent<Button>().enabled = false;
        }
    }

    void FindMatchList(string card)
    {
        for (int i = 0;i < cards.Length; i++)
        {
            if(cards[i].cardName == card)
            {
                cards[i].foundMatch = true;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

}
