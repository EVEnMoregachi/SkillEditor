using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorWindowBase : EditorWindow
{
    public int ���ȼ� { get; set;}

    private void OnFocus()
    {

        EditorWindowManager.focusWindow();

    }




}
