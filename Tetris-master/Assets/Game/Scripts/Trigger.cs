using System;
using UnityEngine;

//every method of this class returning a bool value can be used to trigger the sensors update.
public class Trigger:ScriptableObject{
    private int tetrominoProgr;
    private TetrominoSpawner spawner;

    void Awake()
    {
        tetrominoProgr = 0;
        spawner = GameObject.FindObjectOfType<TetrominoSpawner>();
    }
    public bool spawned()
    {
        if (spawner.progressiveNumber != tetrominoProgr)
        {
            tetrominoProgr = spawner.progressiveNumber;
            Debug.Log("returning true");
            return true;
        }
        return false;
    }
}