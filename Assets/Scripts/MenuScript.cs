using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private void OnGUI()
    {
        var rect = new Rect(Screen.width / 2f - 50, Screen.height / 2f - 15, 100, 30);

        if (GUI.Button(rect, "Play"))
        {
            SceneManager.LoadScene("test");
        }
    }
}
