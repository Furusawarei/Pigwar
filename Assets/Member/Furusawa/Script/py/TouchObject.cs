using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private Transform CollPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float heightOffset = 0.5f;

    private List<GameObject> grabObjects = new List<GameObject>();
    private List<GameObject> objectsInTrigger = new List<GameObject>();
    public bool IsHoldingObject { get; private set; }

    private int maxPearlCount = 3;
    private int maxBoxCount = 1;

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>();

    private bool canInteract = true;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (!canInteract) return;

        if (_playerInput.actions["Have"].triggered)
        {
            GameObject closestObject = null;
            float closestDistance = float.MaxValue;

            foreach (var obj in objectsInTrigger)
            {
                float distance = Vector3.Distance(CollPoint.position, obj.transform.position);

                if (obj.CompareTag("Pearl") &&
                    grabObjects.Count(grabObj => grabObj.CompareTag("Pearl")) < maxPearlCount &&
                    !grabObjects.Exists(grabObj => grabObj.CompareTag("boxPrefab")))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
                else if (obj.CompareTag("boxPrefab") &&
                         grabObjects.Count(grabObj => grabObj.CompareTag("boxPrefab")) < maxBoxCount &&
                         !grabObjects.Exists(grabObj => grabObj.CompareTag("Pearl")))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
            }

            if (closestObject != null)
            {
                GrabObject(closestObject);
                RearrangePearls(); // Ensure pearls are correctly positioned
            }
        }

        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            GameObject throwObj = grabObjects[0];
            grabObjects.RemoveAt(0);
            Rigidbody rb = throwObj.GetComponent<Rigidbody>();
            throwObj.transform.SetParent(null);
            throwObj.transform.rotation = Quaternion.identity;
            rb.isKinematic = false;

            if (originalLayers.ContainsKey(throwObj))
            {
                throwObj.layer = originalLayers[throwObj];
                originalLayers.Remove(throwObj);
            }

            if (throwObj.CompareTag("Pearl"))
            {
                rb.AddForce(CollPoint.forward * 10.0f, ForceMode.Impulse);
                var throwableObject = throwObj.GetComponent<ThrowableObject>();
                if (throwableObject != null)
                {
                    throwableObject.isThrown = true;
                }
                StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f));
            }

            RearrangePearls();
        }

        IsHoldingObject = grabObjects.Count > 0;
    }

    private void GrabObject(GameObject obj)
    {
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer;
        }

        obj.layer = LayerMask.NameToLayer("HeldObjectLayer");

        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.position = grabPoint.position + Vector3.up * heightOffset * grabObjects.Count;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.SetParent(transform);
        grabObjects.Add(obj);

        Debug.Log($"Object grabbed: {obj.name}, Position: {obj.transform.position}");
    }

    private void RearrangePearls()
    {
        for (int i = 0; i < grabObjects.Count; i++)
        {
            if (grabObjects[i].CompareTag("Pearl"))
            {
                grabObjects[i].transform.position = grabPoint.position + Vector3.up * heightOffset * i;
            }
        }
    }

    public void DropAllPearls()
    {
        for (int i = grabObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = grabObjects[i];
            if (obj.CompareTag("Pearl"))
            {
                grabObjects.RemoveAt(i);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                obj.transform.SetParent(null);
                obj.transform.rotation = Quaternion.identity;
                rb.isKinematic = false;

                if (originalLayers.ContainsKey(obj))
                {
                    obj.layer = originalLayers[obj];
                    originalLayers.Remove(obj);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            if (!objectsInTrigger.Contains(other.gameObject))
            {
                objectsInTrigger.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
        }
    }

    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }
}
