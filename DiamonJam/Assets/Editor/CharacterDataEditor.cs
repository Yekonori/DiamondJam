using UnityEditor;
using UnityEngine;









[CustomPropertyDrawer(typeof(QuestionData))]
public class QuestionDataDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, position.width * 0.32f, position.height);
        var unitRect = new Rect(position.x + position.width * 0.33f, position.y, position.width * 0.32f, position.height);
        var nameRect = new Rect(position.x + position.width * 0.66f, position.y, position.width * 0.32f, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("questionDifficulty"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("questionID"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("questionTag"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}




/*[CustomPropertyDrawer(typeof(DiscussionData))]
public class DiscussionDataDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x + position.width * 0.4f, position.y, position.width * 0.58f, position.height);
        //var unitRect = new Rect(position.x + position.width * 0.5f, position.y, position.width * 0.48f, position.height);
        //var nameRect = new Rect(position.x + position.width * 0.66f, position.y, position.width * 0.32f, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("discussionID"), GUIContent.none);
        //EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("discussionTag"), GUIContent.none);
        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("discussionTag"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}*/



[CustomPropertyDrawer(typeof(DiscussionCharacterData))]
public class DiscussionCharacterDataDrawer : PropertyDrawer
{
    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var amountRect = new Rect(position.x, position.y, position.width * 0.32f, position.height);
        var unitRect = new Rect(position.x + position.width * 0.33f, position.y, position.width * 0.32f, position.height);
        var nameRect = new Rect(position.x + position.width * 0.66f, position.y, position.width * 0.32f, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("characterData"), GUIContent.none);
        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("discussionID"), GUIContent.none);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("discussionTag"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}