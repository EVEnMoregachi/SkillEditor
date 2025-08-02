using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G
{
    public class 跳转条件
    {
        public int 目标动画;
        public int 开始帧;
        public int 结束帧;
        public List<int> 操作 = new List<int>();

    }

    public class 攻击判定
    {
        public int 帧号;
        public bool 是否重写范围;
        public int 判定形状;
        public float 参数1;
        public float 参数2;
        public Vector3 offset;

    }

}
