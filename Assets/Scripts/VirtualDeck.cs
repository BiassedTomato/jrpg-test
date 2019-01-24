using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VirtualDeck : MonoBehaviour
{
    public CardBrain[] virtualDeck = new CardBrain[10];
    public int virtualDeckIndex = 0;

    public void PutCard(CardBrain card)
    {
        virtualDeck[virtualDeckIndex] = card;
        OutputDebugger.PutCard(card.Name, virtualDeckIndex);
        // deckImage.sprite = 

        virtualDeckIndex++;
    }

    public void ResetDeck()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < 10; i++)
            virtualDeck[i] = null;
        virtualDeckIndex = 0;
    }



}
