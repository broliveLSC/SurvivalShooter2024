using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
                PauseGame();
            else if (Time.timeScale == 0)
                UnpauseGame();
        }
    }

    [ContextMenu("Pause")]
    public void PauseGame()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public void UnpauseGame()
    {
        SceneManager.UnloadSceneAsync("Pause");
    }
}
