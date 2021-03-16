using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RW_UI : MonoBehaviour
{
    public RandomWalk randomWalk;
    public DecorateDungeon decorateDungeon;

    public InputField newRouteRate;
    public InputField roomRate;
    public InputField maxRouteLength;

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
        if (randomWalk.finished)
        {
            randomWalk.newRouteRate = int.Parse(newRouteRate.text);
            randomWalk.roomRate = int.Parse(roomRate.text);
            randomWalk.maxRouteLength = int.Parse(maxRouteLength.text);
            randomWalk.GenerateMap();
        }
    }

    public void Decorate()
    {
        if (randomWalk.finished)
        {
            decorateDungeon.Decorate();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
