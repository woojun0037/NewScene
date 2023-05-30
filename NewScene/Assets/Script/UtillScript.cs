using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtillScript
{
    //UtillScript.onv3 //사용하는 방법

    public static Vector2 onev2 = Vector2.one;
    public static Vector3 onev3 = Vector3.one;

    public static Vector3 zeronv3 = Vector3.zero;
    public static Vector2 zeronv2 = Vector2.zero;

    public static Vector3 Forward = Vector3.forward;
    public static Vector3 Back = Vector3.back;
    public static Vector3 Right = Vector3.right;
    public static Vector3 Left = Vector3.left;

    public static Vector3 Up = Vector3.up;
    public static Vector3 Down = Vector3.down;

    public static Vector3 Scale(Vector3 a, Vector3 b)
    {
        Vector3 temp = Vector3.Scale(a, b);

        return temp;
    }


}
