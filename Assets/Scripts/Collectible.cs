using UnityEngine;
using UnityEngine.UI;
public class Collectible : MonoBehaviour
{
    public Text text;
    public int counter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = GameObject.Find("Player").GetComponent<PlayerMovement>().collectable;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = counter.ToString();
    }
}
