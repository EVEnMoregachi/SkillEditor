using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 变量_设置全局变量_整数 : 动作
{
    public string 动作描述 = "变量_设置全局变量_整数";
    public string 变量名;
    public int value;
    public override void 数据项目新增(string type)
    {
        
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("变量名"));
            变量名 = EditorGUILayout.TextField("变量名", 变量名, GUILayout.MaxWidth(100));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("值"));
            value = EditorGUILayout.IntField("值", value, GUILayout.MaxWidth(100));
        }
        EditorGUILayout.EndHorizontal();
    }
}