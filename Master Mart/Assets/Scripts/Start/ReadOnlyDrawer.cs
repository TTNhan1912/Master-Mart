#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Ngăn chặn việc chỉnh sửa giá trị từ Inspector
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
#endif
