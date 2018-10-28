//------------------------------------------------------------------------------------------------------------
//-----------------------------------generate file 2018-10-28 15:40:08----------------------------------------
//------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttributeTableSerializeData : ScriptableObject
{
    public List<BaseAttributeTable> DataList = new List<BaseAttributeTable>();
}

[System.Serializable]
public class BaseAttributeTable
{
    public int ID;
    public string name;
    public List<int> chgAttrIntVals = new List<int>();
    public List<float> chgAttrFloatVals = new List<float>();
}
