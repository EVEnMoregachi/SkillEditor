using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class 技能编辑器Window : EditorWindow
{
    private int 技能ID;
    private string 技能名称;
    private string 技能描述;
    private int 目标检测类型Idx;
    private string[] 目标检测类型Array = new string[]
    {
        "敌方",
        "友方",
        "友方除自己",
        "全体",
        "全体除自己",
    };
    private int 技能目标类型Idx;
    private string[] 技能目标类型Array = new string[]
    {
        "对面向",
        "区域",
        "单体锁定",
        "对自己",
        "全体除自己",
    };
    private Texture 技能Icon;
    private string icon;
    private int 技能类型Idx;
    private string[] 技能类型Array = new string[]
    {
        "普攻",
        "主动技能",
        "被动技能",
    };
    private float CD时间;
    private int 技能指示器形状Idx;
    private string[] 指示器形状Array = new string[]
    {
        "圆形",
        "矩形",
        "扇形",
    };
    private float 范围参数1;
    private float 范围参数2;
    private int 资源消耗类型Idx;
    private string[]  资源消耗类型Array = new string[]
    {
        "法力",
        "精力",
        "耐力",
    };
    private float 资源消耗数量;
    private int 升级所需等级;
    private bool 面向施法对象;
    private bool 释放时可转向;
    private bool 释放时可移动;

    bool m_Foldout1;
    GUIContent m_Content1 = new GUIContent("基本信息");
    bool m_Foldout2;
    GUIContent m_Content2 = new GUIContent("技能详情");
    bool m_Foldout3;
    GUIContent m_Content3 = new GUIContent("动画属性");

    string path = "Assets/Resources/";
    string configname = "skillConfig";
    string ext = ".asset";
    public SkillConfigsSto 配置文件;
    public SkillConfigsSto last配置文件;

    public GameObject 模型;
    Animator ani;
    private int 动画Idx = 0;
    float 当前帧;
    private Vector2 scrollView = new Vector2(0, 0);
    int frameSelectIdx;// 当前帧idx
                       //float frameTimeFloat;// 动画播放时长


    string 使用动画名;
    string[] 操作Array = new string[]
    {
        "攻击",
        "移动",
    };

    List<G.跳转条件> 跳转条件list = new List<G.跳转条件>();









    [MenuItem("工具箱/技能编辑器")]
    static void ShowWindow()
    {
        技能编辑器Window window = EditorWindow.GetWindow<技能编辑器Window>();
        window.Show();
    }
    private void OnGUI()
    {
        // 新建配置
        if (GUILayout.Button("新建配置", GUILayout.Width(100)))
        {
            ScriptableObject scriptable = ScriptableObject.CreateInstance<SkillConfigsSto>();
            int idx = 0;
            string url = "";
            while(true)
            {
                url = path + configname + idx + ext;
                if (!File.Exists(url))
                    break;
                idx++;
            }
            AssetDatabase.CreateAsset(scriptable, url);
            this.配置文件 = Resources.Load(configname + idx) as SkillConfigsSto;
            loadConfig();
        }
        // 读取配置
        配置文件 = EditorGUILayout.ObjectField("配置文件", 配置文件, typeof(SkillConfigsSto), true) as SkillConfigsSto;
        if (last配置文件 != 配置文件)
        {
            last配置文件 = 配置文件;
            loadConfig();
        }
        // 保存配置
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveConfig();
        }
        // 基本信息
        EditorGUILayout.BeginVertical(GUI.skin.box); //垂直布局样式
        this.m_Foldout1 = EditorGUILayout.Foldout(m_Foldout1, m_Content1);
        if (m_Foldout1)
        {
            this.技能Icon = EditorGUILayout.ObjectField("添加贴图", 技能Icon, typeof(Texture), true) as Texture;
            this.技能ID = EditorGUILayout.IntField("技能ID", 技能ID);
            this.技能名称 = EditorGUILayout.TextField("技能名称", 技能名称);
            EditorGUILayout.LabelField("技能描述");
            this.技能描述 = EditorGUILayout.TextArea(this.技能描述, GUILayout.Height(35));
        }
        EditorGUILayout.EndVertical();
        // 技能详情
        EditorGUILayout.BeginVertical(GUI.skin.box);
        this.m_Foldout2 = EditorGUILayout.Foldout(m_Foldout2, m_Content2);
        if (m_Foldout2)
        {
            this.目标检测类型Idx = EditorGUILayout.Popup("目标检测类型", 目标检测类型Idx, 目标检测类型Array);
            this.技能目标类型Idx = EditorGUILayout.Popup("技能目标类型", 技能目标类型Idx, 技能目标类型Array);
            this.技能类型Idx = EditorGUILayout.Popup("技能类型", 技能类型Idx, 技能类型Array);
            this.CD时间 = EditorGUILayout.FloatField("CD时间", CD时间);
            this.技能指示器形状Idx = EditorGUILayout.Popup("技能指示器形状", 技能指示器形状Idx, 指示器形状Array);
            EditorGUI.indentLevel++;// 缩进
            this.范围参数1 = EditorGUILayout.FloatField("范围参数1", 范围参数1);
            this.范围参数2 = EditorGUILayout.FloatField("范围参数2", 范围参数2);
            EditorGUI.indentLevel--;// 缩进
            this.资源消耗类型Idx = EditorGUILayout.Popup("资源消耗类型", 资源消耗类型Idx, 资源消耗类型Array);
            EditorGUI.indentLevel++;// 缩进
            this.资源消耗数量 = EditorGUILayout.FloatField("资源消耗数量", 资源消耗数量);
            this.升级所需等级 = EditorGUILayout.IntField("升级所需等级", 升级所需等级);
            EditorGUI.indentLevel--;// 缩进
            this.面向施法对象 = EditorGUILayout.Toggle("面向施法对象", 面向施法对象);
            this.释放时可转向 = EditorGUILayout.Toggle("释放时可转向", 释放时可转向);
            this.释放时可移动 = EditorGUILayout.Toggle("释放时可移动", 释放时可移动);
        }
        EditorGUILayout.EndVertical();

        // 动画属性
        EditorGUILayout.BeginVertical(GUI.skin.box);
        m_Foldout3 = EditorGUILayout.Foldout(m_Foldout3, m_Content3);
        if (m_Foldout3)
        {
            模型 = EditorGUILayout.ObjectField("添加角色", 模型, typeof(GameObject), true) as GameObject;
            if (GUILayout.Button("应用角色", GUILayout.Width(100)))
            {
                if (模型 == null)
                {
                    Debug.LogError("请添加角色");
                    return;
                }
                ani = 模型.GetComponent<Animator>();
                if (ani == null)
                {
                    Debug.LogError("请在模型上绑定Animator组件");
                    return;
                }
            }

            if (ani != null)
            {
                var clips = ani.runtimeAnimatorController.animationClips;
                动画Idx = Mathf.Clamp(动画Idx, 0, clips.Length);
                string[] clipNamesArray = clips.Select(t => t.name).ToArray();

                EditorGUILayout.BeginHorizontal();
                动画Idx = EditorGUILayout.Popup("动画片段", 动画Idx, clipNamesArray);
                if (GUILayout.Button("添加动画", GUILayout.Width(80)))
                {
                    if (使用动画名 == "" || 使用动画名 == null)
                    {
                        使用动画名 = clipNamesArray[动画Idx];
                    }
                    else
                    {
                        使用动画名 += "|" + clipNamesArray[动画Idx];
                    }
                }
                if (GUILayout.Button("撤销动画", GUILayout.Width(80)))
                {
                    if (使用动画名.Length > 0 && 使用动画名 != null)
                    {
                        int pos = 使用动画名.LastIndexOf("|");
                        if (pos > -1)
                        {
                            使用动画名 = 使用动画名.Substring(0, pos);
                        }
                        else
                        {
                            使用动画名 = "";
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                AnimationClip clip = clips[动画Idx];
                clip.SampleAnimation(ani.gameObject, 当前帧);
                frameSelectIdx = EditorGUILayout.IntSlider(frameSelectIdx, 0, (int)(clip.length * clip.frameRate - 1));
                EditorGUILayout.LabelField("动画时长：" + clip.length + "s");
                deawFrames(clip);

                EditorGUILayout.LabelField("使用动画片段：", 使用动画名);
                if (GUILayout.Button("添加跳转条件", GUILayout.Width(200)))
                {
                    G.跳转条件 跳转条件 = new G.跳转条件();
                    跳转条件list.Add(跳转条件);
                }
                for (int i = 0; i < 跳转条件list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("删除", GUILayout.Width(40)))
                    {
                        跳转条件list.RemoveAt(i);
                        EditorGUILayout.EndHorizontal();
                        continue;
                    }

                    EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("跳转目标动画"));
                    跳转条件list[i].目标动画 = EditorGUILayout.Popup("跳转目标动画", 跳转条件list[i].目标动画, clipNamesArray, GUILayout.Width(150));
                    AnimationClip clip_ = clips[跳转条件list[i].目标动画];
                    int frameCount = (int)(clip_.length * clip_.frameRate);
                    EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  开始帧"));
                    跳转条件list[i].开始帧 = EditorGUILayout.IntField("  开始帧", 跳转条件list[i].开始帧, GUILayout.Width(100));
                    EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  结束帧"));
                    跳转条件list[i].结束帧 = EditorGUILayout.IntField("  结束帧", 跳转条件list[i].结束帧, GUILayout.Width(100));

                    跳转条件list[i].开始帧 = Mathf.Clamp(跳转条件list[i].开始帧, 0, frameCount - 1);
                    跳转条件list[i].结束帧 = Mathf.Clamp(跳转条件list[i].结束帧, 0, frameCount - 1);
                    if (跳转条件list[i].结束帧 < 跳转条件list[i].开始帧)
                    {
                        跳转条件list[i].结束帧 = 跳转条件list[i].开始帧;
                    }
                    if (GUILayout.Button("添加输入", GUILayout.Width(60)))
                    {
                        跳转条件list[i].操作.Add(0);
                    }
                    for (int j = 0; j < 跳转条件list[i].操作.Count; j++)
                    {
                        跳转条件list[i].操作[j] = EditorGUILayout.Popup("选择操作", 跳转条件list[i].操作[j], 操作Array, GUILayout.Width(120));
                        if (GUILayout.Button("X", GUILayout.Width(20)))
                        {
                            跳转条件list[i].操作.RemoveAt(j);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                }

                

            }
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 绘制帧信息
    /// </summary>
    private void deawFrames(AnimationClip clip)
    {
        int frameCount = (int)(clip.length * clip.frameRate);
        float 帧绘制width = 40;
        float 帧信息区域width = 600;
        float 帧信息区域height = 50;
        scrollView = EditorGUILayout.BeginScrollView(scrollView, true, true, GUILayout.Width(帧信息区域width), GUILayout.Height(帧信息区域height));
        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < frameCount; i++)
        {
            bool selected = i == frameSelectIdx;
            string title = "" + i;
            if (GUILayout.Button(title, selected ? GUIStyles.item_select : GUIStyles.item_normal, GUILayout.Width(帧绘制width)))
            {
                frameSelectIdx = selected ? -1 : i;
            }
            当前帧 = frameSelectIdx * (1 / clip.frameRate);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// 加载配置
    /// </summary>
    void loadConfig()
    {
        if (this.配置文件 == null) return;
        this.技能ID = this.配置文件.技能ID;
        this.技能名称 = this.配置文件.技能名称;
        this.技能描述 = this.配置文件.技能描述;
        this.目标检测类型Idx = this.配置文件.目标检测类型;
        this.技能目标类型Idx = this.配置文件.技能目标类型;
        this.技能Icon = Resources.Load<Texture>("SkillIcon/" + this.配置文件.icon);
        this.技能类型Idx = this.配置文件.技能类型;
        this.CD时间 = this.配置文件.CD时间;
        this.技能指示器形状Idx = this.配置文件.技能指示器形状;
        this.范围参数1 = this.配置文件.范围参数1;
        this.范围参数2 = this.配置文件.范围参数2;
        this.资源消耗类型Idx = this.配置文件.资源消耗类型;
        this.资源消耗数量 = this.配置文件.资源消耗数量;
        this.升级所需等级 = this.配置文件.升级所需等级;
        this.面向施法对象 = this.配置文件.面向施法对象;
        this.释放时可转向 = this.配置文件.释放时可转向;
        this.释放时可移动 = this.配置文件.释放时可移动;
        // 动画
        this.使用动画名 = this.配置文件.使用动画片段;
        this.跳转条件list = this.配置文件.跳转条件list;
        GameObject 模型_ = GameObject.Find(配置文件.模型name);
        if (模型_ == null)
        {
            //多几个路基获取模型
        }
        else
        {
            模型 = 模型_;
            ani = 模型.GetComponent<Animator>();
            if (ani == null)
            {
                Debug.Log("模型没有绑定animator");
                return;
            }
        }
    }
    /// <summary>
    /// 保存配置
    /// </summary>
    void SaveConfig()
    {
        if (this.配置文件 == null) return;
        this.配置文件.技能ID = this.技能ID;
        this.配置文件.技能名称 = this.技能名称;
        this.配置文件.技能描述 = this.技能描述;
        this.配置文件.目标检测类型 = this.目标检测类型Idx;
        this.配置文件.技能目标类型 = this.技能目标类型Idx;
        if (技能Icon != null) this.配置文件.icon = this.技能Icon.name;
        this.配置文件.技能类型 = this.技能类型Idx;
        this.配置文件.CD时间 = this.CD时间;
        this.配置文件.技能指示器形状 = 技能指示器形状Idx;
        this.配置文件.范围参数1 = this.范围参数1;
        this.配置文件.范围参数2 = this.范围参数2;
        this.配置文件.资源消耗类型 = 资源消耗类型Idx;
        this.配置文件.资源消耗数量 = this.资源消耗数量;
        this.配置文件.升级所需等级 = this.升级所需等级;
        this.配置文件.面向施法对象 = this.面向施法对象;
        this.配置文件.释放时可转向 = this.释放时可转向;
        this.配置文件.释放时可移动 = this.释放时可移动;
        // 动画
        this.配置文件.模型name = this.模型.name;
        this.配置文件.使用动画片段 = this.使用动画名;
        this.配置文件.跳转条件list = this.跳转条件list;
    }
    public static float calcLabelWidth(GUIContent label)
    {
        return GUI.skin.label.CalcSize(label).x + EditorGUI.indentLevel * GUI.skin.label.fontSize * 2;
    }
}




public static class GUIStyles
{
    public static GUIStyle item_select = "MetransitionSelectHend";
    public static GUIStyle item_normal = "MetransitionSelect";
    public static GUIStyle box = "HelpBox";
    public static GUIStyle textfield = "TestFilde";

}