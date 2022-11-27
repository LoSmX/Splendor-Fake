using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawCard : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardPos;
    
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
                Card randCard = deck[Random.Range(0, deck.Count)];
                //transform.position = Vector3.Lerp(transform.position, cardPos[i].position, 1);
                randCard.transform.position = cardPos[i].position;
                randCard.gameObject.SetActive(true);
                freePos[i] = false;
                deck.Remove(randCard);
                return;
            }
        }
    }
}
