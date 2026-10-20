using UnityEditor;
using UnityEngine;

//below attribute for this class ensures this editor script affects NPCInteract class
[CustomEditor(typeof(_NPCInteract))]
public class _CustomNPCInteract : Editor
{
    public override void OnInspectorGUI()
    {
        //built in method "draws" inspector as it normally would be viewed
        //"draw" means to visually render in editor
        //DrawDefaultInspector();

        //this built-in method on this object will continue to "draw" the editor code within this OnInspectorGUI method
        serializedObject.Update();
        
        SerializedProperty typeProp = serializedObject.FindProperty("type");
        EditorGUILayout.PropertyField(typeProp);

        iType currentType = (iType)typeProp.enumValueIndex;

        if (currentType == iType.npc)
        {
            _NPCInteract npcI = (_NPCInteract)target;
            //"draw" properties/variables associated with this interaction type
            EditorGUILayout.PropertyField(serializedObject.FindProperty("textData"));
            EditorGUILayout.HelpBox("Warning: Make sure to write the Prompt Text String before entering play mode.", MessageType.Warning);
            //for editor script if using an IF statement with GUILayout as condition
            //then the editor will generate that button instead of checking to see if condition is true
            if (GUILayout.Button("Load Prompt Text"))
            {
                npcI.LoadText();
            }
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogueData"));
            if (GUILayout.Button("Load Dialogue Runner"))
            {
                npcI.LoadYS();
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("camData"));
            EditorGUILayout.HelpBox("Manually load the NPC Cam Prefab before instantiating in scene", MessageType.Info);
            if (GUILayout.Button("Instantiate NPC Cam Prefab"))
            {
                npcI.LoadNpcCamPrefab();
            }
            //to affect and add to the inspector, we need to gain access to methods in _NPCInteract class
            //below line grants access to those methods using temp variable "npcI"
            
        }
        else if (currentType == iType.item)
        {
            
        }
        //---ESSENTIAL LINE---//
        //without this line, class will perpetually update and override edited properties
        serializedObject.ApplyModifiedProperties();
        
    }
}
