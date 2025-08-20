public class StateBase<T> where T : class
{
    public T Agent { get; set; }
    public StateBase(T agent)
    {
        Agent = agent;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}