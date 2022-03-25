using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Horror)), CanEditMultipleObjects]
public class HorrorEditor : Editor
{
    public SerializedProperty
    //General
        hor_Prop,
        state_Prop,
        player_Prop,

    //Disable At Start
        disableAtStart_Prop,

    //Disable Movement Settings
        delayBeforeMovingAgain_Prop,

    //Move Object Settings
        movableObject_Prop,
        delayMoveObject_Prop,

    //Teleport Object Settings
        teleportObject_Prop,

    //Lights ON/OFF Settings
        lightsOnOff_Prop,
        Lights_Prop,

    //Jumpscare Settings
        jumpSImage_Prop,
        stayOnScreenFor_Prop,

    //Look At Settings
        camera_Prop,
        lookAt_Prop,
        lookAtDelay_Prop,
        damping_Prop,
        lookPos_Prop,
        startLook_Prop,

    //Play Sound Settings
        clipName_Prop,

    //Drop Object Settings
        dropObject_Prop,

    //Levitate Objects Settings
        LevitateObjects_Prop,
        forceUp_Prop,
        forceDown_Prop,
        delay_Prop,

    //Throw Object Settings
        throwObject_Prop,
        force_Prop,

    //Enable Other Trigger Settings
        otherTrigger_Prop;


    void OnEnable()
    {
        // Setup the SerializedProperties
        hor_Prop = serializedObject.FindProperty("hor");
        state_Prop = serializedObject.FindProperty("state");
        player_Prop = serializedObject.FindProperty("player");
        disableAtStart_Prop = serializedObject.FindProperty("disableAtStart");
        delayBeforeMovingAgain_Prop = serializedObject.FindProperty("delayBeforeMovingAgain");
        movableObject_Prop = serializedObject.FindProperty("moveableObject");
        delayMoveObject_Prop = serializedObject.FindProperty("delayMoveObject");
        teleportObject_Prop = serializedObject.FindProperty("teleportObject");
        lightsOnOff_Prop = serializedObject.FindProperty("lightsOnOff");
        Lights_Prop = serializedObject.FindProperty("Lights");
        jumpSImage_Prop = serializedObject.FindProperty("jumpSImage");
        stayOnScreenFor_Prop = serializedObject.FindProperty("stayOnScreenFor");
        camera_Prop = serializedObject.FindProperty("camera");
        lookAt_Prop = serializedObject.FindProperty("lookAt");
        lookAtDelay_Prop = serializedObject.FindProperty("lookAtDelay");
        dropObject_Prop = serializedObject.FindProperty("dropObject");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(state_Prop);


        Horror.HorrorEvent st = (Horror.HorrorEvent)state_Prop.enumValueIndex;

        switch (st)
        {
            case Horror.HorrorEvent.DisableMovement:
                EditorGUILayout.ObjectField(dropObject_Prop, new GUIContent("dropObject"));
                break;

            case Horror.HorrorEvent.MoveObject:
                EditorGUILayout.PropertyField(disableAtStart_Prop, new GUIContent("disableAtStart"));
                EditorGUILayout.Slider(delayBeforeMovingAgain_Prop, 0, 100, new GUIContent("delayBeforeMovingAgain"));
                break;

            case Horror.HorrorEvent.Teleport:
                EditorGUILayout.PropertyField(disableAtStart_Prop, new GUIContent("disableAtStart"));
                EditorGUILayout.Slider(delayBeforeMovingAgain_Prop, 0, 100, new GUIContent("delayBeforeMovingAgain"));
                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}
