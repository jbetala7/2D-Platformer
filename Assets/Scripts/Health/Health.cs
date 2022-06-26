using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float initialHealth;
    public float health { get; private set; }
    private Animator animator;
    private bool dead;

    private void Awake()
    {
        health = initialHealth;
        animator = GetComponent<Animator>();
    }

    public void Damage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, initialHealth);
        
        if(health > 0)
        {
            //hurt
            animator.SetTrigger("hurt");

            //iframes

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

}
