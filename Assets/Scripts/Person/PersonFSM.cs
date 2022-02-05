using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFSM : MonoBehaviour
{



    public float moveDir { get; private set; }

    StateMachine stateMachine = new StateMachine();
   
    void Start()
    {
        stateMachine.ChangeState(new IdelState(this));
    }
 
    void Update()
    {
        
        stateMachine.Update();
    }
}



public interface IState
{
    public void Enter();
    public void Execute();
    public void Exit();
}
 
public class StateMachine
{
    IState currentState;
 
    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState.Exit();
 
        currentState = newState;
        currentState.Enter();
    }
 
    public void Update()
    {
        if (currentState != null) currentState.Execute();
    }
}
 
public class IdelState : IState
{
    PersonFSM owner;
 
    public IdelState(PersonFSM owner) { this.owner = owner; }
 
    public void Enter()
    {
        Debug.Log("entering idle state");
    }
 
    public void Execute()
    {
        Debug.Log("updating idle state");
    }
 
    public void Exit()
    {
        Debug.Log("exiting idle state");
    }
}
 