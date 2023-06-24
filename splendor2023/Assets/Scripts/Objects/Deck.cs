using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardPos;
    public Card[] cards = new Card[4];
    public Card.Type type;

    private bool[] freePos = { true,true,true,true};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < cardPos.Length; i++)
        {
            if(freePos[i] == true)
            {
                if (deck.Count > 0)
                {
                    Card randCard = deck[Random.Range(0, deck.Count)];
                    //transform.position = Vector3.Lerp(transform.position, cardPos[i].position, 1);
                    randCard.transform.position = Vector3.Lerp(transform.position, cardPos[i].position, 1);
                    cards[i] = randCard;
                    randCard.gameObject.SetActive(true);
                    randCard.slotIndex = i;
                    freePos[i] = false;
                    deck.Remove(randCard);
                }
                return;
            }
        }
    }

    public void onPurchaseEvent(object data)
    {
        if(data is Card)
        {
            Card card = (Card)data;
            if(card.type == type)
            {
                freePos[card.slotIndex] = true;
            }
        }
    }
}
