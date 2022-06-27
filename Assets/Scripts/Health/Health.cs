using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Heahlt")]
    [SerializeField] private float initialHealth;
    public float health { get; private set; }
    private Animator animator;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float immunePeriod;
    [SerializeField] private int playerBlinks;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        health = initialHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Damage(float _damage)
    {
        health = Mathf.Clamp(health - _damage, 0, initialHealth);
        
        if(health > 0)
        {
            //hurt
            animator.SetTrigger("hurt");

            //iframes
            StartCoroutine(Immune());

        }
        else
        {
            if(!dead)
            {
                //dead
                animator.SetTrigger("dead");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void Life(float _value)
    {
        health = Mathf.Clamp(health + _value, 0, initialHealth);
    }

    private IEnumerator Immune()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);

        //immune period
        for(int i = 0; i < playerBlinks; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.7f);
            yield return new WaitForSeconds(immunePeriod / (playerBlinks * 4));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(immunePeriod / (playerBlinks * 4));
        }

        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

}
