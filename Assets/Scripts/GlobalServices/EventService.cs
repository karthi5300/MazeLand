using System;
using GlobalServices;

public class EventService : MonoSingletonGeneric<EventService>
{
    // Observer pattern implementation. Handles all event actions.

    public event Action<int> OnPlayerMove;
    public event Action<int> OnPlayerCollectCoin;
    public event Action OnPlayerCompleteLevel;
    public event Action OnPlayerRunOutOfMoves;
    public event Action OnPauseButtonPressed;

    public void InvokeOnPlayerMove(int moves)
    {
        OnPlayerMove?.Invoke(moves);
    }

    public void InvokeOnPlayerCollectCoin(int coins)
    {
        OnPlayerCollectCoin?.Invoke(coins);
    }


    public void InvokeOnPlayerCompleteLevel()
    {
        OnPlayerCompleteLevel?.Invoke();
    }

    public void InvokeOnPlayerRunOutOfMoves()
    {
        OnPlayerRunOutOfMoves?.Invoke();
    }

}
