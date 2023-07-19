using UnityEngine;
using UnityEditor;

public class AttackProjectileDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty typeOfProjectileProperty = property.FindPropertyRelative("TypeOfProjectile");
        if (typeOfProjectileProperty != null)
        {
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            typeOfProjectileProperty.enumValueIndex = EditorGUI.Popup(position, typeOfProjectileProperty.enumValueIndex, typeOfProjectileProperty.enumDisplayNames);
        }

        EditorGUI.EndProperty();
    }


}
