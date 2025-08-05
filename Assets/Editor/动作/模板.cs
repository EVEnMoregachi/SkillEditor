using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 模板 : 动作
{
    public string 动作描述 = "";
    public string 变量名;
    public override void 数据项目新增(string type)
    {
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent(""));

        }
        EditorGUILayout.EndHorizontal();
    }
}