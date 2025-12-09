using UnityEngine;
using UnityEngine.SceneManagement;

public class MudaCena : MonoBehaviour
{
    public void Mudar(string cofrinho)
    {
        SceneManager.LoadScene(cofrinho);
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
