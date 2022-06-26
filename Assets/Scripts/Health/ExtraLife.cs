using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    [SerializeField] private float value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().Life(value);
            gameObject.SetActive(false);
        }
    }
}
