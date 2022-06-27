using UnityEngine;

public class ShootArrows : TrapDamage //damage player on every touch
{
    [SerializeField] private float speed;
    [SerializeField] private float reset; //deactive object after certain period of time 
    private float time; //lifetime

    public void StartShooting()
    {
        time = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float shootingSpeed = speed * Time.deltaTime;
        transform.Translate(shootingSpeed, 0, 0);

        time += Time.deltaTime;
        if(time > reset)
            gameObject.SetActive(false);
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //execute code from 'EnemyDamage' script
        gameObject.SetActive(false); //deativate arrows when it hits any other collider
    }
}
