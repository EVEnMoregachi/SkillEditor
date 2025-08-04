using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class 触发器Window : EditorWindowBase
{
    bool m_Foldout1;
    GUIContent m_Content1 = new GUIContent("触发器项目");
    private Vector2 scrollView = Vector2.zero;
    private void OnGUI()
    {
        SceneView.RepaintAll();
        if (GUILayout.Button("新建触发器", GUILayout.Width(200)))
        {

        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(GUI.skin.box);
        scrollView = EditorGUILayout.BeginScrollView(scrollView, true, true, GUILayout.Width(150), GUILayout.Height(400));
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        m_Foldout1 = EditorGUILayout.Foldout(m_Foldout1, m_Content1);
        if (m_Foldout1)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("新建行为", GUILayout.Width(200)))
            {
                重复窗口.Popup(this.position.position);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndHorizontal();
    }

    [MenuItem("工具箱/触发器编辑器", false, 2)]
    static void ShowWindow()
    {
        触发器Window window = EditorWindow.GetWindow<触发器Window>();
        window.Show();
    }

}
