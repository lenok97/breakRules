using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesScript : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<Character>();
        if (player)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
