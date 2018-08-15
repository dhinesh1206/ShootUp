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
        if(collision.gameObject.tag != "Player" && !hit)
        {
            hit = true;
            //LevelCreation.instance.DeathLevel(collision.gameObject.GetComponent<ClearObjects>().levelname);
            if (!GameManager.instance.challengeModeActive)
            {
                GameManager.instance.DeathLevel(collision.gameObject.GetComponent<ClearObjects>().levelname);
                GameManager.instance.GameOver();
            } else {
                GameManager.instance.ChallengeGameOver();
            }
            audioSource.Play();
            // EventManager.instance.OnGameOver();

        }
    }
}
