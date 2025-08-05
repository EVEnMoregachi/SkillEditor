using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class 触发器Window : EditorWindowBase ,I动作
{
    [MenuItem("工具箱/触发器编辑器", false, 2)]
    static void ShowWindow()
    {
        触发器Window window = EditorWindow.GetWindow<触发器Window>();
        window.Show();
    }


    bool m_Foldout1;
    GUIContent m_Content1 = new GUIContent("触发器项目");
    private Vector2 scrollView = Vector2.zero;

    public static List<动作> 动作list = new List<动作>();

    private void OnGUI()
    {
        SceneView.RepaintAll();
        if (GUILayout.Button("新建触发器", GUILayout.Width(200)))
        {

        }

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                scrollView = EditorGUILayout.BeginScrollView(scrollView, true, true, GUILayout.Width(150), GUILayout.Height(400));
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                m_Foldout1 = EditorGUILayout.Foldout(m_Foldout1, m_Content1);
                if (m_Foldout1)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("新建行为", GUILayout.Width(200)))
                        {
                            重复窗口.Popup(this.position.position).设置页面参数(this, "行为", "行为类别", "行为二级类别", G.行为类型Array);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    渲染全部行为项目();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void 渲染全部行为项目()
    {
        for(int i = 0; i < 动作list.Count; i++)
        {
            动作list[i]?.渲染();
        }
    }

    public void 渲染()
    {
        
    }

    public void 数据项目新增(string type)
    {
        Type type1 = Type.GetType(type);
        System.Object obj = type1.Assembly.CreateInstance(type);
        动作list.Add(obj as 动作);
    }
}
