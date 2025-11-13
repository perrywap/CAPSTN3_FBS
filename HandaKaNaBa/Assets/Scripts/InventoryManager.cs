using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public Sprite emptyIcon;
    public List<Tool> tools = new List<Tool>();
    public List<Image> toolIcons = new List<Image>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateHUDIcons();
    }

    private void UpdateHUDIcons()
    {
        for (int i = 0; i < toolIcons.Count; i++)
        {
            toolIcons[i].sprite = (i < tools.Count && tools[i] != null)
                ? tools[i].icon
                : emptyIcon;
        }
    }
}
