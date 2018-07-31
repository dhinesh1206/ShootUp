using UnityEngine;

public class ParachuteCollision : MonoBehaviour {

    AudioSource audioSource;
    public bool hit;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" && !hit && !ScoreManagement.instance.gameOver)
        {
            hit = true;
            LevelCreation.instance.DeathLevel(collision.gameObject.GetComponent<ClearObjects>().levelname);
            audioSource.Play();
            EventManager.instance.OnGameOver();
        }
    }
}
