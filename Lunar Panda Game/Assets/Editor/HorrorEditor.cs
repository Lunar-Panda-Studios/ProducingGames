using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Horror)), CanEditMultipleObjects]
public class HorrorEditor : Editor
{
    public SerializedProperty
    //General
        list_prop,
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
        list_prop = serializedObject.FindProperty("list");
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
        damping_Prop = serializedObject.FindProperty("damping");
        lookPos_Prop = serializedObject.FindProperty("lookPos");
        startLook_Prop = serializedObject.FindProperty("startLook");
        clipName_Prop = serializedObject.FindProperty("clipName");
        dropObject_Prop = serializedObject.FindProperty("dropObject");
        LevitateObjects_Prop = serializedObject.FindProperty("LevitateObjects");
        forceUp_Prop = serializedObject.FindProperty("forceUp");
        forceDown_Prop = serializedObject.FindProperty("forceDown");
        delay_Prop = serializedObject.FindProperty("delay");
        throwObject_Prop = serializedObject.FindProperty("throwObject");
        force_Prop = serializedObject.FindProperty("force");
        otherTrigger_Prop = serializedObject.FindProperty("otherTrigger");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(state_Prop);


        Horror.HorrorEvent st = (Horror.HorrorEvent)state_Prop.enumValueIndex;
        

        switch (st)
        {
            case Horror.HorrorEvent.DisableAtStart:
                EditorGUILayout.PropertyField(disableAtStart_Prop, new GUIContent("disableAtStart"));
                break;

            case Horror.HorrorEvent.DisableMovement:
                EditorGUILayout.Slider(delayBeforeMovingAgain_Prop, 0, 20, new GUIContent("delayBeforeMovingAgain"));
                break;

            case Horror.HorrorEvent.MoveObject:
                EditorGUILayout.ObjectField(movableObject_Prop, new GUIContent("moveableObject"));
                EditorGUILayout.Slider(delayMoveObject_Prop, 0, 20, new GUIContent("delayMoveObject"));
                break;

            case Horror.HorrorEvent.Teleport:
                EditorGUILayout.ObjectField(teleportObject_Prop, new GUIContent("teleportObject"));
                break;

            case Horror.HorrorEvent.LightOnOff:
                EditorGUILayout.PropertyField(lightsOnOff_Prop, new GUIContent("lightsOnOff"));
                EditorGUILayout.PropertyField(Lights_Prop, new GUIContent("Lights"));
                break;

            case Horror.HorrorEvent.Jumpscare:
                EditorGUILayout.ObjectField(jumpSImage_Prop, new GUIContent("jumpSImage"));
                EditorGUILayout.Slider(stayOnScreenFor_Prop, 0, 5, new GUIContent("stayOnScreenFor"));
                break;

            case Horror.HorrorEvent.LookAt:
                EditorGUILayout.ObjectField(camera_Prop, new GUIContent("camera"));
                EditorGUILayout.ObjectField(lookAt_Prop, new GUIContent("lookAt"));
                EditorGUILayout.Slider(lookAtDelay_Prop, 0, 10, new GUIContent("lookAtDelay"));
                EditorGUILayout.Slider(damping_Prop, 0, 100, new GUIContent("damping"));
                //Vector3
                EditorGUILayout.PropertyField(startLook_Prop, new GUIContent("startLook"));
                break;

            case Horror.HorrorEvent.PlaySound:
                EditorGUILayout.PropertyField(clipName_Prop, new GUIContent("clipName"));
                break;

            case Horror.HorrorEvent.DropObject:
                EditorGUILayout.ObjectField(dropObject_Prop, new GUIContent("dropObject"));
                break;

            case Horror.HorrorEvent.Levitate:
                EditorGUILayout.PropertyField(LevitateObjects_Prop, new GUIContent("LevitateObjects"));
                EditorGUILayout.Slider(forceUp_Prop, 0, 100, new GUIContent("forceUp"));
                EditorGUILayout.Slider(forceDown_Prop, 0, 100, new GUIContent("forceDown"));
                EditorGUILayout.Slider(delay_Prop, 0, 100, new GUIContent("delay"));
                break;

            case Horror.HorrorEvent.ThrowObject:
                EditorGUILayout.ObjectField(throwObject_Prop, new GUIContent("throwObject"));
                EditorGUILayout.Slider(force_Prop, 0, 100, new GUIContent("force"));
                break;

            case Horror.HorrorEvent.EnableOtherTrigger:
                EditorGUILayout.ObjectField(otherTrigger_Prop, new GUIContent("otherTrigger"));
                break;

        }
        EditorGUILayout.PropertyField(list_prop, new GUIContent("list"));

        serializedObject.ApplyModifiedProperties();
    }
}
