using UnityEngine;
using UnityEngine.SceneManagement;

public class porta : MonoBehaviour
{

    public Vector3 destino;
    public string cena;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("Player"))
        {
            
            gameMonange.spawnPosition = destino;
            SceneManager.LoadScene(cena);

        }


    }

}
