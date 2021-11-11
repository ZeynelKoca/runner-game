using UnityEngine;
using TMPro;
using System.Collections;

public class CollisionDetector : MonoBehaviour
{
    public TMP_Text CoinsText;

    public int TotalCoinsCollected { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Coin"))
        {
            TotalCoinsCollected++;
            CoinsText.text = TotalCoinsCollected.ToString();

            Destroy(other.gameObject);
        } 
        else if (other.gameObject.tag.Equals("Obstacle"))
        {
            StartCoroutine(EndGame(2f));
        }
    }

    IEnumerator EndGame(float waitTime)
    {
        StartCoroutine(KillPlayer());
        yield return new WaitForSeconds(waitTime);
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    IEnumerator KillPlayer()
    {
        var animator = GetComponent<Animator>();
        animator.enabled = false;
        yield return null;
    }
}
