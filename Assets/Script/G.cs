using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G
{
    public class ��ת����
    {
        public int Ŀ�궯��;
        public int ��ʼ֡;
        public int ����֡;
        public List<int> ���� = new List<int>();

    }

    public class �����ж�
    {
        public int ֡��;
        public bool �Ƿ���д��Χ;
        public int �ж���״;
        public float ����1;
        public float ����2;
        public Vector3 offset;

    }

    public class ��Ч��Ч��Ϣ
    {
        public int ֡��;
        public ParticleSystem ������Ч;
        public string ��ЧID;
        public Vector3 offset;
        public AudioClip ��Ч;
    }

    public static string[] ����Array = new string[]
    {
        "�����ӳ�",
        "���ټӳ�",
        "���ټӳ�",
        "�����ָ�",
        "������",
        "�����˺�",
        "��ȴ����",
    };

    public class buff����Ӱ��
    {
        public int Ӱ������_;
        public float ��ֵ1;
        public float ��ֵ2;
        public int ��������_;
    }

    public static string[] ��Ч�ҽӵ�Array = new string[]
    {
        "�ŵ�",
        "����",
        "�ز�",
        "����",
        "����",
        "ͷ��",
    };
    public class buff��Ч��Ϣ
    {
        public int ��ЧID;
        public int ��Ч�ҽӵ�;
        public float ����ϵ��;

    }

    public static string[] �˺����Array = new string[]
    {
        "�����˺�",
        "ħ���˺�",
        "��ʵ�˺�",
        "���ķ���",
        "�ָ�����",
        "�ָ�����",
    };

    public static string[] buff��ֵ�������Array = new string[]
    {
        "�﹥",
        "����",
        "����",
        "�������ֵ",
        "��ǰ����ֵ",
        "�ȼ�",
    };

    public class buff�˺���Ϣ
    {
        public int �˺����_;
        public float ��ֵa1;
        public float ��ֵa2;
        public int buff��ֵ�������a;

        public float ��ֵb1;
        public float ��ֵb2;
        public int buff��ֵ�������b;

        public float ��ֵc1;
        public float ��ֵc2;
        public int buff��ֵ�������c;

        public bool �Ƿ�ɱ���;
    }

}
