using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointClip;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void PlayerRespawn()
    {
        playerHealth.Respawn();//reset player health and animation
        transform.position = currentCheckpoint.position; //reset player position to checkpoint
        //Camera.main.GetComponent<CameraFollow>().ChangeRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundSystem.instance.Play(checkpointClip);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
