using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 判断动作 : 动作
{
    public List<G.数值条件info> list = new List<G.数值条件info>();

    public override void 渲染()
    {
        do渲染();
    }

    private void do渲染()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("if", GUILayout.Width(25));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {
                    重复窗口 window_ = 重复窗口.Popup(new Vector3(Screen.width / 2, Screen.height / 2));
                    window_.设置页面参数(this, "条件", "条件类型", "条件", G.条件判断1级参数Array);
                }
            }
            EditorGUILayout.EndHorizontal();
            渲染数值条件();
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("then", GUILayout.Width(50));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {

                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("else", GUILayout.Width(50));
                if (GUILayout.Button("+", GUILayout.Width(20)))
                {

                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    private void 渲染数值条件()
    {
        for (int i = 0; i < list.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("行为类型"));
                list[i].数值1选项 = EditorGUILayout.Popup("", list[i].数值1选项, G.条件判断1级参数Array, GUILayout.MaxWidth(150));
                list[i].手动数值1 = EditorGUILayout.FloatField("", list[i].手动数值1, GUILayout.MaxWidth(150));
                list[i].数值2选项 = EditorGUILayout.Popup("", list[i].数值2选项, G.条件判断1级参数Array, GUILayout.MaxWidth(150));
                list[i].手动数值2 = EditorGUILayout.FloatField("", list[i].手动数值2, GUILayout.MaxWidth(150));
            }
            EditorGUILayout.EndHorizontal();
        }
    }
    public override void 数据项目新增(string type)
    {
        list.Add(new G.数值条件info());
    }

}