using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Reflection;
using Object = UnityEngine.Object;

namespace PurCell
{
    /// <summary>
    /// Editor for AutoTileOverride.
    /// </summary>
    [CustomEditor(typeof(AutoTileOverride))]
    public class AutoTileOverrideEditor : Editor
    {
        private AutoTileOverride autoTile => target as AutoTileOverride;

        /// <summary>
        /// Creates a VisualElement for AutoTileOverride Editor.
        /// </summary>
        /// <returns>A VisualElement for AutoTile Editor.</returns>
        public override VisualElement CreateInspectorGUI()
        {
            var autoTileEditorElement = new AutoTileOverrideEditorElement();
            autoTileEditorElement.Bind(serializedObject);
            autoTileEditorElement.autoTile = autoTile;

            return autoTileEditorElement;
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            var t = GetType("UnityEditor.SpriteUtility");
            if (t != null)
            {
                var method = t.GetMethod("RenderStaticPreview",
                    new[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
                if (method != null)
                {
                    var ret = method.Invoke("RenderStaticPreview",
                        new object[] { autoTile.GetSoloOrDefaultSprite(), Color.white, width, height });
                    if (ret is Texture2D)
                        return ret as Texture2D;
                }
            }

            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }


        private static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null)
                return type;

            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    type = assembly.GetType(typeName);
                    if (type != null)
                        return type;
                }
            }

            return null;
        }
    }

}