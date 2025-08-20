public class StateMachine<T> where T : class
{
    private StateBase<T> _currentState;
    public StateBase<T> CurrentState
    {
        get => _currentState;
        set { _currentState = value; }
    }
    public void ChangeState(StateBase<T> state)
    {
        if (_currentState == state)
        {
            return;
        }
        _currentState?.Exit();
        _currentState = state;
        _currentState?.Enter();
    }
}