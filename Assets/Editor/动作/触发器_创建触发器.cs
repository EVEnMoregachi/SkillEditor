using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 触发器_创建触发器 : 动作
{
    public string 动作描述 = "触发器_创建触发器";
    public string 触发器变量名;
    public List<动作> 动作list = new List<动作>();

    public override void 数据项目新增(string type)
    {
        Type type1 = Type.GetType(type);
        System.Object obj = type1.Assembly.CreateInstance(type);
        动作list.Add(obj as 动作);
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(动作描述, GUILayout.Width(150));
                EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("触发器变量名"));
                触发器变量名 = EditorGUILayout.TextField("触发器变量名", 触发器变量名, GUILayout.Width(150));

                EditorGUILayout.LabelField("动作", GUILayout.Width(50));
                if (GUILayout.Button("+", GUILayout.Width(25)))
                {
                    重复窗口.Popup(new Vector3(Screen.width / 2, Screen.height / 2)).设置页面参数(this, "行为", "行为类别", "行为二级参数", G.行为类型Array);
                }
            }
            EditorGUILayout.EndHorizontal();
            渲染动作liet();
        }
        EditorGUILayout.EndVertical();
    }

    private void 渲染动作liet()
    {
        EditorGUILayout.BeginVertical();
        {
            for (int i = 0; i < 动作list.Count; i++)
            {
                EditorGUILayout.LabelField("动作 " + i + " :");
                EditorGUI.indentLevel++;
                动作list[i]?.渲染();
                EditorGUI.indentLevel--;
            }
        }
        EditorGUILayout.EndVertical();
    }
}