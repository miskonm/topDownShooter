using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(AnimatedButton), true)]
[CanEditMultipleObjects]
public class AnimatedButtonEditor : ButtonEditor
{
    #region Variables

    private SerializedProperty needScaleAnimationProperty;
    private SerializedProperty touchDownScaleAnimationTimeProperty;
    private SerializedProperty touchDownScaleAnimationScaleProperty;
    private SerializedProperty touchDownScaleEaseProperty;
    private SerializedProperty touchUpScaleAnimationTimeProperty;
    private SerializedProperty touchUpScaleAnimationScaleProperty;
    private SerializedProperty touchUpScaleEaseProperty;

    private SerializedProperty needColorChangeProperty;
    private SerializedProperty graphicsToChangeProperty;
    private SerializedProperty normalColorProperty;
    private SerializedProperty highlightedColorProperty;
    private SerializedProperty pressedColorProperty;
    private SerializedProperty selectedColorProperty;
    private SerializedProperty disabledColorProperty;
    private SerializedProperty colorAnimationTimeProperty;

    #endregion


    #region Unity lifecycle

    protected override void OnEnable()
    {
        base.OnEnable();

        needScaleAnimationProperty = serializedObject.FindProperty("needScaleAnimation");
        touchDownScaleAnimationTimeProperty = serializedObject.FindProperty("touchDownScaleAnimationTime");
        touchDownScaleAnimationScaleProperty = serializedObject.FindProperty("touchDownScaleAnimationScale");
        touchDownScaleEaseProperty = serializedObject.FindProperty("touchDownScaleEase");
        touchUpScaleAnimationTimeProperty = serializedObject.FindProperty("touchUpScaleAnimationTime");
        touchUpScaleAnimationScaleProperty = serializedObject.FindProperty("touchUpScaleAnimationScale");
        touchUpScaleEaseProperty = serializedObject.FindProperty("touchUpScaleEase");

        needColorChangeProperty = serializedObject.FindProperty("needColorChange");
        graphicsToChangeProperty = serializedObject.FindProperty("graphicsToChange");
        normalColorProperty = serializedObject.FindProperty("normalColor");
        highlightedColorProperty = serializedObject.FindProperty("highlightedColor");
        pressedColorProperty = serializedObject.FindProperty("pressedColor");
        selectedColorProperty = serializedObject.FindProperty("selectedColor");
        disabledColorProperty = serializedObject.FindProperty("disabledColor");
        colorAnimationTimeProperty = serializedObject.FindProperty("colorAnimationTime");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(needScaleAnimationProperty);
        EditorGUILayout.PropertyField(touchDownScaleAnimationTimeProperty);
        EditorGUILayout.PropertyField(touchDownScaleAnimationScaleProperty);
        EditorGUILayout.PropertyField(touchDownScaleEaseProperty);
        EditorGUILayout.PropertyField(touchUpScaleAnimationTimeProperty);
        EditorGUILayout.PropertyField(touchUpScaleAnimationScaleProperty);
        EditorGUILayout.PropertyField(touchUpScaleEaseProperty);

        EditorGUILayout.PropertyField(needColorChangeProperty);
        EditorGUILayout.PropertyField(graphicsToChangeProperty);
        EditorGUILayout.PropertyField(normalColorProperty);
        EditorGUILayout.PropertyField(highlightedColorProperty);
        EditorGUILayout.PropertyField(pressedColorProperty);
        EditorGUILayout.PropertyField(selectedColorProperty);
        EditorGUILayout.PropertyField(disabledColorProperty);
        EditorGUILayout.PropertyField(colorAnimationTimeProperty);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Button", EditorStyles.boldLabel);

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }

    #endregion
}
