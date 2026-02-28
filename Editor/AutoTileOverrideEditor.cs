using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace PurCell
{
    /// <summary>
    /// Editor for AutoTile.
    /// </summary>
    [CustomEditor(typeof(AutoTileOverride))]
    public class AutoTileOverrideEditor : Editor
    {
        private AutoTileOverride autoTile => target as AutoTileOverride;

        /// <summary>
        /// Creates a VisualElement for AutoTile Editor.
        /// </summary>
        /// <returns>A VisualElement for AutoTile Editor.</returns>
        public override VisualElement CreateInspectorGUI()
        {
            var autoTileEditorElement = new AutoTileOverrideEditorElement();
            autoTileEditorElement.Bind(serializedObject);
            autoTileEditorElement.autoTile = autoTile;
            return autoTileEditorElement;
        }
    }

}