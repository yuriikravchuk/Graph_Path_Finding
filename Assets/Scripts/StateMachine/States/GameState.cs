using UnityEngine;

public abstract class GameState : MonoBehaviour
{
    public abstract void Enter();

    protected abstract void Exit();

    public abstract bool CanTransit(GameState state);

    public abstract void Transit(GameState state);
}