using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnPlayButtonClick()
    {
        Debug.Log("Clicked PLay Button");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SnakeGameScene");
    }
}
