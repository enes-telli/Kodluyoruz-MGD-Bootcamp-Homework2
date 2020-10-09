using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] List<State> gameStates = new List<State>();

    private static IState currentState;

    void Start()
    {
        SetGameState(StateType.PreGameState);
    }

    #region STATE MACHINE
    public void SetGameState(StateType stateType)
    {
        IState newState = gameStates.FirstOrDefault(i => i.stateType == stateType).stateScript as IState;

        if (currentState != null) 
            currentState.Exit();
        if (newState == currentState) 
            return;

        currentState = newState;
        currentState.Enter();
    }

    public static IState GetCurrentState()
    {
        return currentState;
    }
    #endregion

}

[System.Serializable]
public class State
{
    public StateType stateType;
    public MonoBehaviour stateScript;
}

public enum StateType
{
    PreGameState,
    PlayGameState,
    PauseGameState
}