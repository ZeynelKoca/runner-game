using UnityEngine;
using TMPro;

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
            Debug.Log("Player collided with an obstacle");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
