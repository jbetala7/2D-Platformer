using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballs;
    [SerializeField] private AudioClip fireballClip;
    [SerializeField] private Text fireballCountText;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    public int initialFireballs = 3;
    public int fireballValue = 2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.attackAllowed())
            Attack();

        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        if (initialFireballs < 1) return;


        initialFireballs--;
        fireballCountText.text = "" + initialFireballs;

        SoundSystem.instance.Play(fireballClip);

        animator.SetTrigger("attack");
        cooldownTimer = 0;

        var fireball = new GameObject();
        fireball = Instantiate(fireballs, firePoint.position, Quaternion.identity);

        fireball.GetComponent<Fireball>().SetDirection(Mathf.Sign(transform.localScale.x));

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fireball")
        {
            initialFireballs += fireballValue;
            fireballCountText.text = "" + initialFireballs;
            other.gameObject.SetActive(false);
        }
    }
}
