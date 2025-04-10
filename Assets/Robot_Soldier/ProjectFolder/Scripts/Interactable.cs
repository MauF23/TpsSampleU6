using StarterAssets;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform objectPivot;
    public LayerMask layerMask;
    public Collider collider;
    public Vector3 rayOffset;
    public float interactDistance = 1;
    private float interactRayRadius = 3;
    public bool canInteract = true;
    private RaycastHit hit;
    private ThirdPersonController playerInBounds;
    private ThirdPersonController playerInRange;
    private Color debugColor;

    protected virtual void Start()
    {
        if (collider)
        {
            collider.isTrigger = true;
        }

        if (objectPivot == null)
        {
            objectPivot = transform;
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<ThirdPersonController>(out playerInBounds);
    }

    protected void OnTriggerExit(Collider other)
    {
        ThirdPersonController player = other.GetComponent<ThirdPersonController>();
        if (player != null && player == playerInBounds)
        {
            playerInBounds = null;
        }

    }

    protected void Update()
    {
        if (playerInBounds == null || !canInteract)
        {
            return;
        }

        Vector3 direction = playerInBounds.transform.position + rayOffset;
        Ray ray = new Ray(objectPivot.position, direction - objectPivot.position);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, interactDistance, layerMask))
        {
            playerInRange = hit.collider.GetComponent<ThirdPersonController>();
            debugColor = Color.green;
        }
        else
        {
            debugColor = Color.red;
        }

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, debugColor);
        if (playerInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }


    }

    protected virtual void Interact()
    {
        canInteract = false;
    }
}
