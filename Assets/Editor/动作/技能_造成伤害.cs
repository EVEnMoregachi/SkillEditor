using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 技能_造成伤害 : 动作
{
    public string 动作描述 = "技能_造成伤害";
    public int 单位选择;
    public int 技能选择;
    public int 被伤害单位选择 = -1;
    public int 被伤害单位组选择 = -1;
    public int 伤害数值序列;
    public int 受击特效序列;
    public int 选择的中心单位;
    public float 范围半径 = 5;
    public EG.单位组Bass 被伤害单位组data;


    public override void 数据项目新增(string type)
    {
    }

    public override void 渲染()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField(动作描述, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("单位选择"));
            单位选择 = EditorGUILayout.Popup("单位选择", 单位选择, G.变量函数_单位Array, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("技能选择"));
            技能选择 = EditorGUILayout.Popup("技能选择", 技能选择, G.技能选择_Array, GUILayout.Width(150));
            渲染单位选择();
            渲染单位组选择();
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("伤害数字序列"));
            伤害数值序列 = EditorGUILayout.IntField("伤害数值序列", 伤害数值序列, GUILayout.Width(150));
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("受击特效序列"));
            受击特效序列 = EditorGUILayout.IntField("受击特效序列", 受击特效序列, GUILayout.Width(150));
        }
        EditorGUILayout.EndHorizontal();
    }

    void 渲染单位选择()
    {
        EditorGUI.BeginChangeCheck();
        {
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("被伤害单位"));
            被伤害单位选择 = EditorGUILayout.Popup("被伤害单位", 被伤害单位选择, G.单体单位选择_Array, GUILayout.Width(400));
        }
    }

    void 渲染单位组选择()
    {
        EditorGUI.BeginChangeCheck();
        {
            EditorGUIUtility.labelWidth = EG.calcLabelWidth(new GUIContent("被伤害单位组"));
            被伤害单位组选择 = EditorGUILayout.Popup("被伤害单位组", 被伤害单位组选择, G.单位组选择_Array, GUILayout.Width(400));
        }
        if (EditorGUI.EndChangeCheck())
        {
            if (被伤害单位组选择 == 0)
            {
                被伤害单位组data = new EG.单位组_范围内的所有单位();
            }
            else if (被伤害单位组选择 == 1)
            {
                被伤害单位组data = new EG.单位组_随机单位组();
            }
        }
    }
}