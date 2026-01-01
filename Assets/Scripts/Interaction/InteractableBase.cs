using System;
using UnityEngine;

[Serializable]
public class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _holdInteract;
    [SerializeField] private bool _canInteract;
    public bool HoldInteract => _holdInteract;
    public bool CanInteract => _canInteract;

    public void OnInteract()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }
}
