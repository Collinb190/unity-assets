using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class ItemDatabase<T> : EditorWindow where T : class
{
    protected Vector2 scrollPosition;
    protected T selectedItem;
    protected float propertiesSectionWidth = 400f;
    
    public ItemDatabase()
    {
        this.minSize = new Vector2(600, 400); 
    }

    protected abstract void DrawItemList();
    protected abstract void DrawPropertiesSection();

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        
        DrawItemList();
        
        GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(propertiesSectionWidth));
        DrawPropertiesSection();
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    protected virtual void DeleteSelectedItem()
    {
        if (selectedItem == null)
        {
            EditorUtility.DisplayDialog("Error", "No item selected to delete.", "OK");
            return;
        }

        bool confirmDelete = EditorUtility.DisplayDialog("Confirm Delete", "Are you sure you want to delete this item?", "Delete", "Cancel");
        if (confirmDelete)
        {
            string assetPath = AssetDatabase.GetAssetPath(selectedItem as UnityEngine.Object);
            AssetDatabase.DeleteAsset(assetPath);
            AssetDatabase.Refresh();
            selectedItem = null; // Clear selected item after deletion
        }
    }

    protected void DuplicateSelectedItem()
    {
        //Duplicate item
    }

    protected abstract void ExportItemsToCSV();

    protected abstract void ImportItemsFromCSV();

    protected void DrawTopLeftOptions()
    {
        GUILayout.Label("Database Admin Functions:", EditorStyles.boldLabel);
        if (GUILayout.Button("Export to CSV")) ExportItemsToCSV();
        if (GUILayout.Button("Import from CSV")) ImportItemsFromCSV();
        if (GUILayout.Button("Delete Selected Item")) DeleteSelectedItem();
        if (GUILayout.Button("Duplicate Selected Item")) DuplicateSelectedItem();
        
        // ... additional top-left options, like 'Create New Item' or 'Search' ...
    }
}