using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighSurface : InteractableObject
{
    [Header("Items allowed on this surface")]
    [SerializeField] private List<Tool> itemsToPlace;

    [Header("Placement settings")]
    [SerializeField] private float spacing = 0.05f;   // gap between items
    [SerializeField] private int maxSearchSteps = 200; // safety
    [SerializeField] private Transform surfacePoint;   // origin point for placement

    private List<GameObject> placedItems = new List<GameObject>();
    private float currentXOffset = 0f;

    private Bounds surfaceBounds;

    private void Awake()
    {
        // Calculate surface bounds from its collider
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            surfaceBounds = col.bounds;
        }
        else
        {
            Debug.LogError("HighSurface needs a collider to calculate bounds!");
            surfaceBounds = new Bounds(surfacePoint.position, Vector3.one);
        }
    }

    public override void Interact()
    {
        PlaceItemsFromInventory();
    }

    private void PlaceItemsFromInventory()
    {
        List<Tool> playerItems = InventoryManager.Instance.tools;
        bool placedSomething = false;

        foreach (Tool required in itemsToPlace.ToList())
        {
            Tool toolInInventory = playerItems.FirstOrDefault(t => t.toolName == required.toolName);
            if (toolInInventory == null) continue;

            PlaceItemOnSurface(toolInInventory);

            playerItems.Remove(toolInInventory);
            itemsToPlace.Remove(required);
            placedSomething = true;
        }

        if (!placedSomething)
        {
            Debug.Log("No valid items to place.");
            return;
        }

        if (itemsToPlace.Count == 0 && !isCompleted)
        {
            CompleteTask();
            Debug.Log("All items moved to higher surface!");
        }
    }

    private void PlaceItemOnSurface(Tool tool)
    {
        if (tool.prefab == null)
        {
            Debug.LogError($"Tool '{tool.toolName}' has no prefab assigned!");
            return;
        }

        GameObject item = Instantiate(tool.prefab, surfacePoint.position, surfacePoint.rotation);
        item.SetActive(true);

        Renderer[] renderers = item.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning($"Placed item '{tool.toolName}' has no renderer!");
        }

        // Compute the item bounds in world space
        Bounds itemBounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
            itemBounds.Encapsulate(renderers[i].bounds);

        // Align item bottom with surface top
        float surfaceTopY = surfaceBounds.max.y;
        Vector3 pos = item.transform.position;
        pos.y = surfaceTopY + (itemBounds.extents.y); // center at half-height above surface
        item.transform.position = pos;

        // Find first free horizontal spot along X-axis
        float xOffset = currentXOffset;
        bool foundSpot = false;
        for (int step = 0; step < maxSearchSteps; step++)
        {
            Vector3 candidatePos = new Vector3(surfaceBounds.min.x + xOffset + itemBounds.extents.x,
                                               pos.y,
                                               pos.z);

            item.transform.position = candidatePos;

            // Recalculate bounds at new position
            Bounds newBounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++) newBounds.Encapsulate(renderers[i].bounds);

            // Check if it fits within surface bounds
            if (newBounds.max.x > surfaceBounds.max.x)
            {
                Debug.LogWarning($"Item '{tool.toolName}' exceeds surface width, placing at last valid position.");
                break;
            }

            // Check against all already placed items
            bool intersects = false;
            foreach (GameObject placed in placedItems)
            {
                if (placed == null) continue;
                Renderer[] placedRends = placed.GetComponentsInChildren<Renderer>();
                Bounds placedBounds = placedRends[0].bounds;
                for (int i = 1; i < placedRends.Length; i++) placedBounds.Encapsulate(placedRends[i].bounds);

                if (newBounds.Intersects(placedBounds))
                {
                    intersects = true;
                    break;
                }
            }

            if (!intersects)
            {
                foundSpot = true;
                currentXOffset = candidatePos.x - surfaceBounds.min.x + itemBounds.extents.x + spacing;
                break;
            }

            xOffset += 0.01f; // small incremental move
        }

        if (!foundSpot)
        {
            Debug.LogWarning($"Could not find free spot for '{tool.toolName}', placing at last tried location.");
        }

        // Disable physics so items stay in place
        foreach (Collider col in item.GetComponentsInChildren<Collider>())
            col.enabled = false;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        placedItems.Add(item);
    }
}
