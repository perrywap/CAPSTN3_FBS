using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<Tool> tools = new List<Tool>();
    public List<Image> toolIcons = new List<Image>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (tools.Count <= 0) return;

        for (int i = 0; i < tools.ToList().Count; i++)
        {
            toolIcons[i].sprite = tools[i].icon;
        }
    }
}
