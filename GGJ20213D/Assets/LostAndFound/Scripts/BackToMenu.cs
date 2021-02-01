using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnHomeBtn()
    {
        SceneManager.LoadScene("Menu");
    }

}
