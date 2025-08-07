using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 技能_施加buff : 动作
{
    public string 动作描述 = "技能_施加buff";
    public int 技能选择, 单位选择, buff序号;
    public override void 数据项目新增(string type)
    {
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("技能选择"));
            技能选择 = EditorGUILayout.Popup("技能选择", 技能选择, G.技能选择_Array, GUILayout.MaxWidth(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("buff序号"));
            buff序号 = EditorGUILayout.IntField("buff序号", buff序号, GUILayout.MaxWidth(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("buff接受单位"));
            单位选择 = EditorGUILayout.Popup("buff接受单位", 单位选择, G.变量函数_单位Array, GUILayout.MaxWidth(150));
        }
        EditorGUILayout.EndHorizontal();
    }
}