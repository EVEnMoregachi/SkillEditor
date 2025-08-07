using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 计时器_运行计时器 : 动作
{
    public string 动作描述 = "运行计时器";
    public string 计时器name;

    public override void 数据项目新增(string type)
    {
        
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(100));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("运行计时器"));
            计时器name = EditorGUILayout.TextField("运行计时器", 计时器name, GUILayout.MaxWidth(200));
        }
        EditorGUILayout.EndHorizontal();
    }
}