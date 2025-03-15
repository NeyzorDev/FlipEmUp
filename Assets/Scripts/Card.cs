using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using static CardManager;

public class Card : MonoBehaviour
{
    enum CardState
    {
        Hidden = 0,
        Show,
        Unflip
    };

    //private Animation anim;
    private SpriteRenderer spriteRender;
    private CardState state;

    [SerializeField]
    public TypesOfCards type;
    [SerializeField]
    private float matchedTime = 1.0f;
    [SerializeField]
    private float unflipTime = 0.5f;
    [SerializeField]
    private Sprite[] sprites;

    public bool canBeFlipped;
    public delegate void IsFlippedEvent(Card c);
    public event IsFlippedEvent IsFlipped;

    private bool matched;
    private float matchedTimer;
    private float unflipTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = sprites[0];

        canBeFlipped = true;
        matched = false;
        matchedTimer = 0.0f;
        unflipTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if (matched)
        {
            matchedTimer += dt;
            if (matchedTimer > matchedTime)
            {
                Destroy(gameObject);
            }
        }
        
        if (state == CardState.Unflip)
        {
            unflipTimer += dt;
            if (unflipTimer > unflipTime)
            {
                state = CardState.Hidden;
                spriteRender.sprite = sprites[0];
            }
        }

    }

    public void Flip()
    {
        if(state == CardState.Hidden)
        {
            state = CardState.Show;
            spriteRender.sprite = sprites[(int)type + 1];

            IsFlipped.Invoke(this);
        }
    }

    public void Unflip()
    {
        if (state == CardState.Show)
        {
            state = CardState.Unflip;
            unflipTimer = 0.0f;
        }
    }

    public void Matched()
    {
        matched = true;
    }
}
