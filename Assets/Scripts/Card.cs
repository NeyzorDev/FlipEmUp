using UnityEngine;

public class Card : MonoBehaviour
{
    enum CardState
    {
        Hidden = 0,
        show
    };

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CardState state;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Flip()
    {
        if(state == CardState.Hidden)
        {
            state = CardState.show;
            animator.SetBool("isHidden", false);
            
        }
    }
}
