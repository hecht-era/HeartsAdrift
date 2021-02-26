using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Configuration;
using UnityEngine;

public enum GameState
{
    SAILING,
    DOCKING,
    UNDOCKING,
    DOCKED,
    WALKING,
    READING,
    TREASURE
}
public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public GameState state;

    private void Awake()
    {
        Instance = this;
    }

    public GameState GetState()
    {
        return state;
    }

    public void SetState(GameState type)
    {
        state = type;
    }
}

