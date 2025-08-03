
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class Buff编辑器Window : EditorWindow
{
    bool m_Foldout1, m_Foldout2, m_Foldout3, m_Foldout4;
    GUIContent m_Content1 = new GUIContent("基本属性");
    GUIContent m_Content2 = new GUIContent("属性影响");
    GUIContent m_Content3 = new GUIContent("特效信息");
    GUIContent m_Content4 = new GUIContent("伤害信息");

    int buffId; float 持续时间;
    public int SelectIdx1;
    public int SelectIdx2;
    public int 可叠加层数;
    private Vector2 scrollView1 = Vector2.zero;
    private Vector2 scrollView2 = Vector2.zero;
    List<G.buff属性影响> buff属性影响list = new List<G.buff属性影响>();
    List<G.buff特效信息> buff特效信息list = new List<G.buff特效信息>();
    List<G.buff伤害信息> buff伤害信息list = new List<G.buff伤害信息>();

    [MenuItem("工具箱/Buff编辑器",false, 1)]
    static void ShowWindow()
    {
        Buff编辑器Window window = EditorWindow.GetWindow<Buff编辑器Window>();
        window.Show();
    }


    private void OnGUI()
    {
        scrollView1 = EditorGUILayout.BeginScrollView(scrollView1, true, true);
        if (GUILayout.Button("新建buff", GUILayout.Width(200)))
        {

        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(GUI.skin.box);
        scrollView2 = EditorGUILayout.BeginScrollView(scrollView2, true, true, GUILayout.Width(200), GUILayout.Height(position.height));
        EditorGUILayout.EndScrollView();


        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        m_Foldout1 = EditorGUILayout.Foldout(m_Foldout1, m_Content1);
        if (m_Foldout1)
        {
            buffId = EditorGUILayout.IntField("buffId", buffId);
            可叠加层数 = EditorGUILayout.IntField("可叠加层数", 可叠加层数);
            持续时间 = EditorGUILayout.FloatField("持续时间", 持续时间);
        }

        m_Foldout2 = EditorGUILayout.Foldout(m_Foldout2, m_Content2);
        if (m_Foldout2)
        {
            if (GUILayout.Button("新增属性", GUILayout.Width(200)))
            {
                G.buff属性影响 temp = new G.buff属性影响();
                buff属性影响list.Add(temp);
            }
            for (int i = 0; i < buff属性影响list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("删除", GUILayout.Width(50)))
                {
                    buff属性影响list.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                    continue;
                }
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("属性影响"));
                buff属性影响list[i].影响属性_ = EditorGUILayout.Popup("属性影响", buff属性影响list[i].影响属性_,G.属性Array, GUILayout.MaxWidth(150));
                buff属性影响list[i].数值1 = EditorGUILayout.FloatField("数值1", buff属性影响list[i].数值1, GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField("+", GUILayout.Width(10));
                buff属性影响list[i].数值2 = EditorGUILayout.FloatField("数值2", buff属性影响list[i].数值2, GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField("*", GUILayout.Width(10));
                buff属性影响list[i].计算属性_ = EditorGUILayout.Popup("计算属性", buff属性影响list[i].计算属性_, G.属性Array, GUILayout.MaxWidth(150));
                EditorGUILayout.EndHorizontal();
            }
        }

        m_Foldout3 = EditorGUILayout.Foldout(m_Foldout3, m_Content3);
        if (m_Foldout3)
        {
            if (GUILayout.Button("新增特效", GUILayout.Width(200)))
            {
                G.buff特效信息 temp = new G.buff特效信息();
                temp.缩放系数 = 1;
                buff特效信息list.Add(temp);
            }
            for (int i = 0; i < buff特效信息list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("删除", GUILayout.Width(50)))
                {
                    buff特效信息list.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                    continue;
                }
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("影响属性"));
                buff特效信息list[i].特效ID = EditorGUILayout.IntField("特效ID", buff特效信息list[i].特效ID, GUILayout.MaxWidth(100));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("特效挂接点"));
                buff特效信息list[i].特效挂接点 = EditorGUILayout.Popup("特效挂接点", buff特效信息list[i].特效挂接点, G.特效挂接点Array, GUILayout.MaxWidth(150));
                buff特效信息list[i].缩放系数 = EditorGUILayout.FloatField("缩放系数", buff特效信息list[i].缩放系数, GUILayout.MaxWidth(100));
                EditorGUILayout.EndHorizontal();
            }
        }

        m_Foldout4 = EditorGUILayout.Foldout(m_Foldout4, m_Content4);
        if (m_Foldout4)
        {
            if (GUILayout.Button("新增伤害信息", GUILayout.Width(200)))
            {
                G.buff伤害信息 temp = new G.buff伤害信息();
                buff伤害信息list.Add(temp);
            }
            for (int i = 0; i < buff伤害信息list.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("删除", GUILayout.Width(50)))
                {
                    buff伤害信息list.RemoveAt(i);
                    EditorGUILayout.EndHorizontal();
                    continue;
                }
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("影响属性"));
                buff伤害信息list[i].伤害类别_ = EditorGUILayout.Popup("伤害类别", buff伤害信息list[i].伤害类别_, G.伤害类别Array, GUILayout.Width(150));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  伤害值为"));
                buff伤害信息list[i].数值a1 = EditorGUILayout.FloatField("  伤害值为(", buff伤害信息list[i].数值a1, GUILayout.MaxWidth(100));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("a"));
                buff伤害信息list[i].数值a2 = EditorGUILayout.FloatField("+", buff伤害信息list[i].数值a2, GUILayout.MaxWidth(50));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent(" "));
                buff伤害信息list[i].buff数值计算类别a = EditorGUILayout.Popup("*", buff伤害信息list[i].buff数值计算类别a, G.buff数值计算类别Array, GUILayout.MaxWidth(100));

                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent(")+("));
                buff伤害信息list[i].数值b1 = EditorGUILayout.FloatField(")+(", buff伤害信息list[i].数值b1, GUILayout.MaxWidth(50));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("+"));
                buff伤害信息list[i].数值b2 = EditorGUILayout.FloatField("+", buff伤害信息list[i].数值b2, GUILayout.MaxWidth(50));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("*"));
                buff伤害信息list[i].buff数值计算类别b = EditorGUILayout.Popup("*", buff伤害信息list[i].buff数值计算类别b, G.buff数值计算类别Array, GUILayout.MaxWidth(100));

                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent(")²+("));
                buff伤害信息list[i].数值c1 = EditorGUILayout.FloatField(")²+(", buff伤害信息list[i].数值c1, GUILayout.MaxWidth(50));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("+"));
                buff伤害信息list[i].数值c2 = EditorGUILayout.FloatField("+", buff伤害信息list[i].数值c2, GUILayout.MaxWidth(50));
                EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("*"));
                buff伤害信息list[i].buff数值计算类别c = EditorGUILayout.Popup("*", buff伤害信息list[i].buff数值计算类别c, G.buff数值计算类别Array, GUILayout.MaxWidth(100));
                EditorGUILayout.LabelField(")³");
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }

    public static float calcLabelWidth(GUIContent label)
    {
        return GUI.skin.label.CalcSize(label).x + EditorGUI.indentLevel * GUI.skin.label.fontSize * 2;
    }
}

