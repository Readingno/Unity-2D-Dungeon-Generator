using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GR_UI : MonoBehaviour
{
    public GenerateRoom generateRoom;
    public DecorateDungeon decorateDungeon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        if (generateRoom.finished)
        {
            generateRoom.GenerateMap();
        }
    }

    public void Decorate()
    {
        if (generateRoom.finished)
        {
            decorateDungeon.Decorate();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
