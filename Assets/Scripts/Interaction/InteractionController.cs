using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private InteractionData _interactionData;
    [SerializeField] private InteractionInputData _interactionInputData;
    [SerializeField] private float _interactRadius;
    [SerializeField] private float _interactDistance;
    [SerializeField] private LayerMask _interactLayer;
    private Camera _interactCamera;
    void Start()
    {
        _interactCamera = Camera.main;
    }
    void Update()
    {
        CheckInteractionRaycast();
    }

    private void CheckInteractionRaycast()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(_interactCamera.transform.position, _interactRadius, _interactCamera.transform.forward, out hitInfo, _interactDistance, _interactLayer))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<InteractableBase>(out var interactable))
            {
                if (interactable.CanInteract && Input.GetKeyDown(KeyCode.F))
                {
                    interactable.OnInteract();
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        if (_interactCamera == null)
        {
            return;
        }
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_interactCamera.transform.position, _interactCamera.transform.forward * _interactDistance);
    }
}