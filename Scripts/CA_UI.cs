using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CA_UI : MonoBehaviour
{
    public CellularAutomata cellularAutomata;
    public DecorateDungeon decorateDungeon;

    public Toggle randomSeed;
    public InputField seed;

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
        if (cellularAutomata.finished)
        {
            cellularAutomata.useRandomSeed = randomSeed.isOn;
            cellularAutomata.seed = seed.text;
            cellularAutomata.GenerateMap();
        }
    }

    public void Decorate()
    {
        if (cellularAutomata.finished)
        {
            decorateDungeon.Decorate();
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
