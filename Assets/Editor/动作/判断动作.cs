using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 判断动作 : 动作
{
    public List<G.数值条件info> 数值条件list = new List<G.数值条件info>();
    public List<动作> then动作list = new List<动作>();
    public List<动作> else动作list = new List<动作>();
    int 添加类型;

    public override void 渲染()
    {
        EditorGUILayout.BeginVertical();
        {
            
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("if", GUILayout.Width(100));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    添加类型 = 0;
                    重复窗口 window_ = 重复窗口.Popup(new Vector3(Screen.width / 2, Screen.height / 2));
                    window_.设置页面参数(this, "条件", "条件类型", "条件", G.条件判断1级参数Array);
                }
            }
            EditorGUILayout.EndHorizontal();
            渲染数值条件();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("then", GUILayout.Width(100));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    添加类型 = 1;
                    重复窗口 window_ = 重复窗口.Popup(new Vector3(Screen.width / 2, Screen.height / 2));
                    window_.设置页面参数(this, "行为", "行为类型", "行为二级类别", G.行为类型Array);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
                渲染then动作();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("else", GUILayout.Width(100));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    添加类型 = 2;
                    重复窗口 window_ = 重复窗口.Popup(new Vector3(Screen.width / 2, Screen.height / 2));
                    window_.设置页面参数(this, "行为", "行为类型", "行为二级类别", G.行为类型Array);
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginVertical();
                渲染else动作();
            EditorGUILayout.EndVertical();

        }
        EditorGUILayout.EndVertical();
    }


    private void 渲染数值条件()
    {
        EditorGUI.indentLevel++;
        for (int i = 0; i < 数值条件list.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("行为类型"));
                数值条件list[i].数值1选项 = EditorGUILayout.Popup("", 数值条件list[i].数值1选项, G.数值选项参数Array, GUILayout.MaxWidth(150));
                数值条件list[i].手动数值1 = EditorGUILayout.FloatField("", 数值条件list[i].手动数值1, GUILayout.MaxWidth(150));
                数值条件list[i].数值关系 = EditorGUILayout.Popup("", 数值条件list[i].数值关系, G.数值关系Array, GUILayout.MaxWidth(150));
                数值条件list[i].数值2选项 = EditorGUILayout.Popup("", 数值条件list[i].数值2选项, G.数值选项参数Array, GUILayout.MaxWidth(150));
                数值条件list[i].手动数值2 = EditorGUILayout.FloatField("", 数值条件list[i].手动数值2, GUILayout.MaxWidth(150));
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel--;
    }

    private void 渲染then动作()
    {
        EditorGUI.indentLevel++;
        for (int i = 0; i < then动作list.Count; i++)
        {
            EditorGUILayout.LabelField("行为 " + i.ToString() + " :");
            then动作list[i]?.渲染();
        }
        EditorGUI.indentLevel--;
    }

    private void 渲染else动作()
    {
        EditorGUI.indentLevel++;
        for (int i = 0; i < else动作list.Count; i++)
        {
            EditorGUILayout.LabelField("行为 " + i.ToString() + " :");
            else动作list[i]?.渲染();
        }
        EditorGUI.indentLevel--;
    }

    public override void 数据项目新增(string type)
    {
        if (添加类型 == 0)
        {
            数值条件list.Add(new G.数值条件info());
        }
        else if (添加类型 == 1)
        {
            Type type1 = Type.GetType(type);
            System.Object obj = type1.Assembly.CreateInstance(type);
            then动作list.Add(obj as 动作);
        }
        else if (添加类型 == 2)
        {
            Type type1 = Type.GetType(type);
            System.Object obj = type1.Assembly.CreateInstance(type);
            else动作list.Add(obj as 动作);
        }
    }
}