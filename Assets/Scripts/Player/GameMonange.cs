using UnityEngine;

public class gameMonange : MonoBehaviour
{
    public static Vector3 spawnPosition = Vector3.zero;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (gameMonange.spawnPosition != Vector3.zero)
        {
            transform.position = gameMonange.spawnPosition;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}

