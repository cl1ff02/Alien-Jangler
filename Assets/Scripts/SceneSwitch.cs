using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NextScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
