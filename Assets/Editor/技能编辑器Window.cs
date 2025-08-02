using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Jobs;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using 攻击判定;

public class 技能编辑器Window : EditorWindow
{
    private Vector2 ScrollPos;

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
    private int 攻击判定形状Idx;
    private string[] 攻击判定形状Array = new string[]
    {
        "圆形",
        "矩形",
        "扇形",
    };
    private float 范围参数1;
    private float 范围参数2;

    public float 偏移量X, 偏移量Y, 偏移量Z;

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
    // 动画
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
    // 攻击
    List<G.攻击判定> 攻击判定list = new List<G.攻击判定>();
    bool 绘制攻击范围 = false;  
    bool 是否重写攻击判定信息= false;

    攻击判定.I判定范围 I判定范围_ = null;
    判定信息 判定信息_ = new 判定信息();
    BoxBoundsHandle boxHandle = new BoxBoundsHandle();
    SphereBoundsHandle sphereHandle = new SphereBoundsHandle();
    攻击判定.BoxItem boxItem = new 攻击判定.BoxItem();
    攻击判定.SphereItem sphereItem = new 攻击判定.SphereItem();



    [MenuItem("工具箱/技能编辑器")]
    static void ShowWindow()
    {
        技能编辑器Window window = EditorWindow.GetWindow<技能编辑器Window>();
        window.Show();
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView view)
    {
        OnSceneGUI();
        view.Repaint();
        Repaint();
    }

    void OnSceneGUI()
    {
        if (绘制攻击范围)
        {
            do绘制攻击范围();
        }
    }

    void do绘制攻击范围()
    {
        if (!是否重写攻击判定信息)
        {
            switch (攻击判定形状Idx)
            {
                case 1:
                    I判定范围_ = boxItem;
                    I判定范围_.SetValue(范围参数1, 1, 范围参数2, 偏移量X, 偏移量Y, 偏移量Z);
                    break;
                case 0:
                    I判定范围_ = sphereItem;
                    I判定范围_.SetValue(范围参数1, 0, 0, 偏移量X, 偏移量Y, 偏移量Z);
                    break;
            }
        }
        else
        {
            int 索引 = 找到当前帧号在攻击判定list中的索引(frameSelectIdx);
            if (索引 >= 0)
            {
                switch (攻击判定list[索引].判定形状)
                {
                    case 1:
                        I判定范围_ = boxItem;
                        I判定范围_.SetValue(攻击判定list[索引].参数1, 1, 攻击判定list[索引].参数2, 攻击判定list[索引].offset.x, 攻击判定list[索引].offset.y, 攻击判定list[索引].offset.z);
                        break;
                    case 0:
                        I判定范围_ = sphereItem;
                        I判定范围_.SetValue(攻击判定list[索引].参数1, 0, 0, 攻击判定list[索引].offset.x, 攻击判定list[索引].offset.y, 攻击判定list[索引].offset.z);
                        break;
                }
            }
        }
        判定信息_.value = I判定范围_;
        Matrix4x4 localToWorld = 模型.transform.localToWorldMatrix;
        Draw判定范围(判定信息_, localToWorld, new Color(1.0f, 0.5f, 0.1f, 0.3f));

    }
    void Draw判定范围(判定信息 判定信息_, Matrix4x4 localToWorld, Color color)
    {
        Matrix4x4 temp = Matrix4x4.TRS(localToWorld.MultiplyPoint3x4(Vector3.zero), localToWorld.rotation, Vector3.one);// 获取模型矩阵并且去掉缩放
        Handles.matrix = temp;
        do绘制Range(判定信息_, color);
        do绘制Handler(判定信息_.value);
    }
    void do绘制Range(判定信息 config, Color color)
    {
        Handles绘制器.H.PushColor(color);
        Handles绘制器.H.填充绘制 = true;
        switch (config.value)
        {
            case 攻击判定.BoxItem v:
                Handles绘制器.H.DrawBox(v.size, Matrix4x4.Translate(v.offset)); ;
                break;
            case 攻击判定.SphereItem v:
                Handles绘制器.H.DrawSphere(v.radius, Matrix4x4.Translate(v.offset));
                break;
        }
        Handles绘制器.H.填充绘制 = false;
        Handles绘制器.H.PopColor();
    }
    void do绘制Handler(攻击判定.I判定范围 config)
    {
        Vector3 offset = Vector3.zero;
        Vector3 size = Vector3.one;
        switch (config)
        {
            case 攻击判定.BoxItem v:
                offset = v.offset;
                size = v.size;
                break;
            case 攻击判定.SphereItem v:
                offset = v.offset;
                size = new Vector2(v.radius, 0);
                break;
        }
        float handlerSize = HandleUtility.GetHandleSize(offset);
        switch (Tools.current)
        {
            case Tool.View:
                break;
            case Tool.Move:
                offset = Handles.DoPositionHandle(offset, Quaternion.identity);
                break;
            case Tool.Scale:
                size = Handles.DoScaleHandle(size, offset, Quaternion.identity, handlerSize);
                break;
            case Tool.Transform:
                Vector3 _offset = offset;
                Vector3 _size = size;
                Handles.TransformHandle(ref _offset, Quaternion.identity, ref _size);
                offset = _offset;
                size = _size;
                break;
            case Tool.Rect:
                switch (config)
                {
                    case 攻击判定.BoxItem v:
                        boxHandle.axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Y | PrimitiveBoundsHandle.Axes.Z;
                        boxHandle.center = offset;
                        boxHandle.size = size;
                        boxHandle.DrawHandle();
                        offset = boxHandle.center;
                        size = boxHandle.size;
                        break;
                    case 攻击判定.SphereItem v:
                        sphereHandle.axes = PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Y | PrimitiveBoundsHandle.Axes.Z;
                        sphereHandle.center = offset;
                        sphereHandle.radius = size.x;
                        sphereHandle.DrawHandle();
                        offset = sphereHandle.center;
                        size.x = sphereHandle.radius;
                        break;
                }
                break;
        
        }
        Func<Vector3> getOffset = () => new Vector3((offset.x), offset.y, offset.z);
        Func<Vector3> getSize = () => new Vector3((size.x), size.y, size.z);
        Func<float> getRadius = () => size.magnitude;
        switch (config)
        {
            case 攻击判定.BoxItem v:
                v.offset = getOffset();
                v.size = getSize();
                if (!是否重写攻击判定信息)
                {
                    偏移量X = v.offset.x;
                    偏移量Y = v.offset.y;
                    偏移量Z = v.offset.z;
                    范围参数1 = v.size.x;
                    范围参数2 = v.size.z;
                }
                else
                {
                    int 索引 = 找到当前帧号在攻击判定list中的索引(frameSelectIdx);
                    if (索引 > 0)
                    {
                        攻击判定list[索引].offset.x = v.offset.x;
                        攻击判定list[索引].offset.y = v.offset.y;
                        攻击判定list[索引].offset.z = v.offset.z;
                        攻击判定list[索引].参数1 = v.size.x;
                        攻击判定list[索引].参数2 = v.size.z;
                    }
                }
                break;
            case 攻击判定.SphereItem v:
                v.offset = getOffset();
                v.radius = getRadius();
                if (!是否重写攻击判定信息)
                {
                    偏移量X = v.offset.x;
                    偏移量Y = v.offset.y;
                    偏移量Z = v.offset.z;
                    范围参数1 = v.radius;
                }
                else
                {
                    int 索引 = 找到当前帧号在攻击判定list中的索引(frameSelectIdx);
                    if (索引 > 0)
                    {
                        攻击判定list[索引].offset.x = v.offset.x;
                        攻击判定list[索引].offset.y = v.offset.y;
                        攻击判定list[索引].offset.z = v.offset.z;
                        攻击判定list[索引].参数1 = v.radius;
                    }
                }
                break;
        }
    }

    private void OnGUI()
    {
        ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, true, true);
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
            this.攻击判定形状Idx = EditorGUILayout.Popup("攻击判定形状", 攻击判定形状Idx, 攻击判定形状Array);
            EditorGUI.indentLevel++;// 缩进
            this.范围参数1 = EditorGUILayout.FloatField("范围参数1", 范围参数1);
            this.范围参数2 = EditorGUILayout.FloatField("范围参数2", 范围参数2);
            this.偏移量X = EditorGUILayout.FloatField("偏移量x", 偏移量X);
            this.偏移量Y = EditorGUILayout.FloatField("偏移量y", 偏移量Y);
            this.偏移量Z = EditorGUILayout.FloatField("偏移量z", 偏移量Z);
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
                // 攻击判定
                if (GUILayout.Button("添加攻击判定", GUILayout.Width(200)))
                {
                    G.攻击判定 攻击判定 = new G.攻击判定();
                    攻击判定.帧号 = frameSelectIdx;
                    攻击判定list.Add(攻击判定);
                }
                for (int i = 0; i < 攻击判定list.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("删除", GUILayout.Width(40)))
                    {
                        攻击判定list.RemoveAt(i);
                        EditorGUILayout.EndHorizontal();
                        continue;
                    }
                    EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("帧号"));
                    攻击判定list[i].帧号 = EditorGUILayout.IntField("帧号", 攻击判定list[i].帧号, GUILayout.Width(70));
                    攻击判定list[i].帧号 = Mathf.Clamp(攻击判定list[i].帧号, 0, (int)(clips[动画Idx].length * clips[动画Idx].frameRate - 1));
                    EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  是否重写范围"));
                    攻击判定list[i].是否重写范围  = EditorGUILayout.Toggle("是否重写范围", 攻击判定list[i].是否重写范围, GUILayout.Width(100));
                    if (攻击判定list[i].是否重写范围)
                    {
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  判定形状"));
                        攻击判定list[i].判定形状 = EditorGUILayout.Popup("判定形状", 攻击判定list[i].判定形状, 攻击判定形状Array, GUILayout.Width(120));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  参数1"));
                        攻击判定list[i].参数1 = EditorGUILayout.FloatField("  参数1", 攻击判定list[i].参数1, GUILayout.Width(100));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  参数2"));
                        攻击判定list[i].参数2 = EditorGUILayout.FloatField("  参数2", 攻击判定list[i].参数2, GUILayout.Width(100));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  offset"));
                        EditorGUILayout.LabelField("  offset", GUILayout.Width(50));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  x"));
                        攻击判定list[i].offset.x = EditorGUILayout.FloatField("  x", 攻击判定list[i].offset.x, GUILayout.Width(50));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  y"));
                        攻击判定list[i].offset.y = EditorGUILayout.FloatField("  y", 攻击判定list[i].offset.y, GUILayout.Width(50));
                        EditorGUIUtility.labelWidth = calcLabelWidth(new GUIContent("  z"));
                        攻击判定list[i].offset.z = EditorGUILayout.FloatField("  z", 攻击判定list[i].offset.z, GUILayout.Width(50));
                    }
                    EditorGUILayout.EndHorizontal();
                }
                // Scene角色动画播放与动画片段时间轴绘制
                AnimationClip clip = clips[动画Idx];
                clip.SampleAnimation(ani.gameObject, 当前帧);
                frameSelectIdx = EditorGUILayout.IntSlider(frameSelectIdx, 0, (int)(clip.length * clip.frameRate - 1));
                EditorGUILayout.LabelField("动画时长：" + clip.length + "s");
                deawFrames(clip);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();
    }

    int 找到当前帧号在攻击判定list中的索引(int num)
    {
        int res = 0;
        foreach(var t in 攻击判定list)
        {
            if (t.帧号 == num)
                return res;
            res++;
        }
        return -1;
    }

    bool 该帧是否有攻击判定(int num)
    {
        if (攻击判定list.Count - 1 >= num && num >= 0) return true;
        return false;
    }

    bool 该帧是否重写攻击判定信息(int num)
    {
        if (该帧是否有攻击判定(num))
        {
            return 攻击判定list[num].是否重写范围;
        }
        return false;
    }


    /// <summary>
    /// 绘制帧信息
    /// </summary>
    private void deawFrames(AnimationClip clip)
    {
        int frameCount = (int)(clip.length * clip.frameRate);
        float 帧绘制width = 40f;
        float 帧信息区域height = 50f;

        float 窗口可视宽度 = position.width - 20f; // 留点边距，防止挤满

        scrollView = EditorGUILayout.BeginScrollView(
            scrollView,
            true, false,
            GUILayout.Height(帧信息区域height)
        );

        EditorGUILayout.BeginHorizontal();

        for (int i = 0; i < frameCount; i++)
        {
            bool selected = i == frameSelectIdx;
            int 索引_ = 找到当前帧号在攻击判定list中的索引(i);
            string title = string.Format("{0}\n{1}", i, 该帧是否有攻击判定(索引_) ? "口" : "");

            if (GUILayout.Button(title, selected ? GUIStyles.item_select : GUIStyles.item_normal, GUILayout.Width(帧绘制width)))
            {
                frameSelectIdx = selected ? -1 : i;
            }

            当前帧 = frameSelectIdx * (1f / clip.frameRate);
        }

        int 索引__ = 找到当前帧号在攻击判定list中的索引(frameSelectIdx);
        绘制攻击范围 = 该帧是否有攻击判定(索引__);
        是否重写攻击判定信息 = 该帧是否重写攻击判定信息(索引__);

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
        this.攻击判定形状Idx = this.配置文件.攻击判定形状Idx;
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
        this.配置文件.攻击判定形状Idx = 攻击判定形状Idx;
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
    //public static GUIStyle item_select = new GUIStyle(EditorStyles.helpBox) { normal = { textColor = Color.green } };
    //public static GUIStyle item_normal = new GUIStyle(EditorStyles.helpBox);
    public static GUIStyle box = "HelpBox";
    public static GUIStyle textfield = "TestFilde";

}

public class 判定信息
{
    public 攻击判定.I判定范围 value;
}

namespace 攻击判定
{
    public interface I判定范围
    {
        public void SetValue(float a1, float a2, float a3, float x, float y, float z);
    }
    public class BoxItem : I判定范围
    {
        public Vector3 offset = Vector3.zero;
        public Vector3 size = Vector3.one;
        public void SetValue(float a1, float a2, float a3, float x, float y, float z)
        {
            this.size.x = a1;
            this.size.y = a2;
            this.size.z = a3;
            this.offset.x = x;
            this.offset.y = y;
            this.offset.z = z;
        }
    }

    public class SphereItem : I判定范围
    {
        public Vector3 offset = Vector3.zero;
        public float radius;
        public void SetValue(float a1, float a2, float a3, float x, float y, float z)
        {
            this.radius = a1;
            this.offset.x = x;
            this.offset.y = y;
            this.offset.z = z;
        }
    }
}
public class Handles绘制器 : DeawTools
{
    public static Handles绘制器 H = new Handles绘制器();
    public override Color color { get => Handles.color; set => Handles.color = value; }
    public override void DrawLine(Vector3 start, Vector3 end)
    {
        Handles.DrawLine(start, end);
    }
    protected override void 填充多边形(Vector3[] vertices)
    {
        Handles.DrawAAConvexPolygon(vertices);
    }
}

public abstract class DeawTools
{
    public static Color 默认Color = Color.white;
    public abstract void DrawLine(Vector3 strat, Vector3 end);

    public virtual Color color {get; set;}
    public Color 线框Color => new Color(1, 1, 1, color.a);
    public int 圆切割精度 = 30;
    public bool 填充绘制 = false;
    public bool 是否绘制线框 = false;
    Stack<Color> _colorStack = new Stack<Color>();

    public void PushColor(Color color)
    {
        _colorStack.Push(this.color);
        this.color = color;
    }
    public void PopColor()
    {
        this.color = _colorStack.Count > 0 ? _colorStack.Pop() : 默认Color;
    }

    public void 绘制多边形(Vector3[] vertices)
    {
        if (填充绘制)
        {
            填充多边形(vertices);
            if (是否绘制线框)
            {
                PushColor(线框Color);
                for (int i = vertices.Length - 1, j = 0; j < vertices.Length; i = j, j++)
                {
                    DrawLine(vertices[i], vertices[j]);
                }
                PopColor();
            }
        }
        else
        {
            for (int i = vertices.Length - 1, j = 0; j < vertices.Length; i = j, j++)
            {
                DrawLine(vertices[i], vertices[j]);
            }
        }
    }

    protected virtual void 填充多边形(Vector3[] vertices)
    {
        for (int i = vertices.Length - 1, j = 0; j < vertices.Length; i = j, j++)
        {
            DrawLine(vertices[i], vertices[j]);
        }
    }
    // 绘制盒子
    public void DrawBox(Vector3 size, Matrix4x4 matrix)
    {
        Vector3[] points = MathTools.CalcBoxVertex(size, matrix);
        int[] indexs = MathTools.GetBoxSurfaceIndexs();
        for (int i = 0; i < 6; i++)
        {
            Vector3[] polygon = new Vector3[]
            {
                points[indexs[i * 4]],
                points[indexs[i * 4 + 1]],
                points[indexs[i * 4 + 2]],
                points[indexs[i * 4 + 3]]
            };
            绘制多边形(polygon);
        }
    }
    // 绘制球体
    public void DrawSphere(float radius, Matrix4x4 matrix)
    {
        Matrix4x4 lookMatrix = Matrix4x4.identity;
        SceneView sceneView = SceneView.currentDrawingSceneView;
        if (sceneView != null)
        {
            Camera cam = sceneView.camera;// scene场景的相机
            var cameraTrans = cam.transform;
            var rotation = Quaternion.LookRotation(cameraTrans.position - matrix.MultiplyPoint(Vector3.zero));
            lookMatrix = Matrix4x4.TRS(matrix.MultiplyPoint(Vector3.zero), rotation, matrix.lossyScale);
            DrawCircle(radius, lookMatrix);
        }
        bool oldColorFill = 填充绘制;
        填充绘制 = false;
        PushColor(线框Color);
        DrawCircle(radius, matrix);
        DrawCircle(radius, matrix * Matrix4x4.Rotate(Quaternion.Euler(0, 90, 0)));
        DrawCircle(radius, matrix * Matrix4x4.Rotate(Quaternion.Euler(90, 0, 0)));
        PopColor();
        填充绘制 = oldColorFill;
    }
    //绘制圆形
    public void DrawCircle(float radius, Matrix4x4 lookMatrix)
    {
        Vector3[] vertices = MathTools.CalcCircleVertex(radius, lookMatrix, 圆切割精度);
        绘制多边形(vertices);
    }
}

public static class MathTools
{
    public const float PI = Mathf.PI;

    public static int[] GetBoxSurfaceIndexs()
    {
        return new int[]
        {
            0,1,2,3,// 上
            4,5,6,7,// 下
            2,6,5,3,// 左
            0,4,7,1,// 右
            1,7,6,2,// 前
            0,3,5,4,// 后
        };
    }
    /// <summary>
    /// 返回长方体的8个顶点
    /// </summary>
    public static Vector3[] CalcBoxVertex(Vector3 size)
    {
        Vector3 halfSize = size * 0.5f;
        Vector3[] points = new Vector3[8];
        points[0] = new Vector3(halfSize.x, halfSize.y, halfSize.z);
        points[1] = new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        points[2] = new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        points[3] = new Vector3(-halfSize.x, halfSize.y, halfSize.z);

        points[4] = new Vector3(halfSize.x, -halfSize.y, halfSize.z);
        points[5] = new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
        points[6] = new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
        points[7] = new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        return points;
    }

    public static Vector3[] CalcBoxVertex(Vector3 size, Matrix4x4 matrix)
    {
        Vector3[] points = CalcBoxVertex(size);
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = matrix.MultiplyPoint(points[i]);
        }
        return points;
    }
    /// <summary>
    /// 返回球体顶点
    /// </summary>
    /// <param name="radius">半径</param>
    /// <param name="matrix"></param>
    /// <param name="count">圆形切割边数</param>
    /// <returns></returns>
    public static Vector3[] CalcCircleVertex(float radius,Matrix4x4 matrix, int count = 30)
    {
        float deg = 2 * Mathf.PI;
        float delatDeg = deg / count;
        Vector3[] vertices = new Vector3[count];
        for (int i = 0; i < count; i++)
        {
            Vector2 pos;
            float d = deg - i * delatDeg;
            pos.x = Mathf.Cos(d) * radius;
            pos.y = Mathf.Sin(d) * radius;
            vertices[i] = matrix.MultiplyPoint(pos);
        }
        return vertices;
    }
}
