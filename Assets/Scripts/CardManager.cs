using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public enum TypesOfCards
    {
        joker = 0,
        uno,
        dos,
        tres,
        cuatro,
        cinco
    }

    [Header("Field Design")]
    [SerializeField]
    float hMargin = 1.0f;
    [SerializeField]
    float wMargin = 2.0f;

    [Header("Card Prefab")]
    [SerializeField]
    GameObject cardPrefab;

    [Header("Card Distribution")]
    [SerializeField]
    ushort height = 3;
    [SerializeField]
    ushort width = 4;
    [SerializeField]
    ushort matchNumber = 2;
    [SerializeField]
    ushort maxNumOfRepeatedCards = 1;

    [Header("Debug")]
    [SerializeField]
    GameObject[] cardList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardList = new GameObject[height * width];
        ushort[] types;
        GenerateCardTypes(out types, (ushort)(height * width));
        // Shuffle();
        CreateAndPlaceCards(ref types);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateCardTypes(out ushort[]types, ushort length)
    {
        types = new ushort[length];
        Dictionary<ushort, ushort> cardPool = new Dictionary<ushort, ushort>();
        
        // Initialization.
        for (ushort i = 0; i < System.Enum.GetNames(typeof(TypesOfCards)).Length; ++i)
        {
            cardPool[i] =(ushort)(maxNumOfRepeatedCards);
        }

        // Random Generation.
        int halfLength = types.Length / 2;
        for(ushort i = 0; i < halfLength; ++i)
        {
            ushort type = cardPool.ElementAt(Random.Range(0, cardPool.Count)).Key;
            types[i] = type;
            if (--cardPool[type] == 0) { cardPool.Remove(type); }
        }
        // Generate the pairs.
        for (int i = halfLength; i < types.Length; ++i)
        {
            types[i] = types[i - halfLength];
        }
    }

    private void CreateAndPlaceCards(ref ushort[] types)
    {
        BoxCollider2D cardCollider = cardPrefab.GetComponent<BoxCollider2D>();

        float offX =  (width / 2.0f - 0.5f) * (cardCollider.size.x + wMargin);
        float offY = (height / 2.0f - 0.5f) * (cardCollider.size.y + hMargin);
        Vector3 generalOffSet = new Vector3(offX, offY, 0.0f);
        
        for(int i = 0; i < types.Length; ++i)
        {
            int r = i / width;
            int c = i % width;

            float x = (cardCollider.size.x + wMargin) * c;
            float y = (cardCollider.size.y + hMargin) * r;
            Vector3 newCardPos = new Vector3(x, y, 0.0f) - generalOffSet;

            cardList[i] = Instantiate(cardPrefab, newCardPos, Quaternion.identity);
        }

    }
}
