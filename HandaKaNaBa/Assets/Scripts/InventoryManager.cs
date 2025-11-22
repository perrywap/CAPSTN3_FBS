using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject inventoryUI;
    [SerializeField] private Image inventoryIcon;
    [SerializeField] private GameObject toolIconPrefab;
    [SerializeField] private Transform iconsParent;

    public List<Tool> tools = new List<Tool>();

    public float maxToolCount = 12;
    public float toolCount = 0;

    private UnityEngine.Color color;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        toolCount = tools.Count;
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = !inventoryUI.activeSelf;
            inventoryUI.SetActive(isActive);

            if (isActive)
            {
                inventoryIcon.transform.parent.gameObject.SetActive(false);
                OnOpenInventory();
            }
            else
            {
                inventoryIcon.transform.parent.gameObject.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        inventoryIcon.fillAmount = toolCount / maxToolCount;

        if (toolCount < maxToolCount)
        {
            ColorUtility.TryParseHtmlString("#94BE90", out color); // If inventory not full, color green
        }
        else if (toolCount >= maxToolCount)
        {
            ColorUtility.TryParseHtmlString("#BE9093", out color); // if inventory full, color red
        }
        

        inventoryIcon.color = color;

    }

    // ----------- ADD TOOL -------------
    public void AddTool(Tool tool)
    {
        if (tools.Count >= maxToolCount)
            return;

        tools.Add(tool);

        // Refresh UI instantly if open
        if (inventoryUI.activeSelf)
        {
            OnOpenInventory();
        }
    }

    // ----------- REMOVE TOOL -------------
    public void RemoveTool(Tool tool)
    {
        if (tools.Contains(tool))
        {
            tools.Remove(tool);

            // Refresh UI instantly if open
            if (inventoryUI.activeSelf)
            {
                OnOpenInventory();
            }
        }
    }

    public void OnOpenInventory()
    {
        // Clear existing icons
        foreach (Transform child in iconsParent)
        {
            Destroy(child.gameObject);
        }

        // Create icon for each tool
        foreach (Tool tool in tools)
        {
            GameObject iconObj = Instantiate(toolIconPrefab, iconsParent);

            UnityEngine.UI.Image iconImage = iconObj.GetComponent<UnityEngine.UI.Image>();
            if (iconImage != null)
                iconImage.sprite = tool.icon;
        }
    }
}
