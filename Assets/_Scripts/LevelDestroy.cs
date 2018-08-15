using UnityEngine;

public class LevelDestroy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Level_End")
        {

            if (GameManager.instance.challengeModeActive)
                GameManager.instance.ChallengeCompleted();
            Destroy(collision.transform.parent.gameObject);
        }
    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
	}
}
