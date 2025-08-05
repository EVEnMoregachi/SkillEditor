using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class 变量动作 : 动作
{
    public List<G.设置局部变量info> list = new List<G.设置局部变量info>();

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
                EditorGUILayout.LabelField("设置变量类型", GUILayout.Width(50));
                list[0].变量类型 = EditorGUILayout.Popup("变量类型", list[0].变量类型, G.变量类型Array, GUILayout.MaxWidth(150));
                EditorGUILayout.LabelField("变量名", GUILayout.Width(25));
                list[0].手动变量数值 = EditorGUILayout.FloatField("", list[0].手动变量数值, GUILayout.MaxWidth(150));
                list[0].变量函数 = EditorGUILayout.Popup("", list[0].变量函数, G.变量Array, GUILayout.MaxWidth(150));
            }
            EditorGUILayout.EndHorizontal();
            渲染数值();
        }
        EditorGUILayout.EndVertical();
    }

    private void 渲染数值()
    {

    }

    public override void 数据项目新增(string type)
    {
        list.Add(new G.设置局部变量info());
    }
}