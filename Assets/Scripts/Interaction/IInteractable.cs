public interface IInteractable
{
    bool HoldInteract { get; }
    bool CanInteract { get; }
    void OnInteract();
}