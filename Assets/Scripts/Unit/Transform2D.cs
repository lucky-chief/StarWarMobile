using UnityEngine;
using System.Collections.Generic;

public class Transform2D
{
    public const float zOffset = 0.1f;
    private Transform m_owner;
    private Vector2 m_position;
    private Vector2 m_localPosition;
    private Vector2 m_localScale;
    private Vector2 m_forword;
    private Vector2 m_right;
    private Vector2 m_angle;
    private int m_childIndex;
    private int m_childCount;

    private List<Transform2D> children = new List<Transform2D>();

    public Vector2 Position
    {
        get
        {
            return new Vector2 (m_owner.position.x,m_owner.position.y);
        }

        set
        {  
            m_owner.position = new Vector3(value.x,value.y,m_owner.position.z);
            m_position = value;
        }
    }

    public Vector2 LocalPosition
    {
        get
        {
            return new Vector2 (m_owner.localPosition.x,m_owner.localPosition.y);
        }

        set
        {
            m_owner.localPosition = new Vector3(value.x,value.y,m_owner.localPosition.z);
            m_localPosition = value;
        }
    }

    public Vector2 LocalScale
    {
        get {
            return new Vector2(m_owner.localScale.x,m_owner.localScale.y);
        }
        set{
            m_owner.localScale = new Vector3(value.x,value.y,1);
            m_localScale = value;
        }
    }

    public Transform owner
    {
        get{
            return m_owner;
        }
    }

    public Vector2 Forword
    {
        get
        {
            m_forword = (new Vector2(m_owner.up.x,m_owner.up.y)).normalized;
            return m_forword;
        }
    }

    public Vector2 Right
    {
        get
        {
            m_right = (new Vector2(m_owner.right.x,m_owner.right.y)).normalized;
            return m_right;
        }
    }

    public Vector2 Angle
    {
        get{
            return m_angle;
        }
        set
        {
            m_owner.Rotate(value.x,value.y,0);
            m_angle = value;
        }
    }

    public int ChildIndex
    {
        get
        {
            return m_childIndex;
        }

        set
        {
            value = value < 0 ? 0 : value;
            m_childIndex = value;
            m_owner.transform.localPosition += new Vector3(0,0,zOffset * value);
        }
    }

    public int ChildCount
    {
        get
        {
            return m_childCount;
        }
    }

    public Transform2D(Transform owner)
    {
        m_owner = owner;
    }

    ~Transform2D()
    {
        m_owner = null;
    }

    public void AddChild(Transform2D child)
    {
        AddChild(child,Vector2.zero,Vector2.zero);
    }

    public void AddChild(Transform2D child,Vector2 localPositoin,Vector2 angle)
    {
        child.owner.SetParent(m_owner);
        child.LocalPosition = localPositoin;
        child.Angle = angle;
        child.m_childCount++;
        child.m_childIndex = child.owner.GetSiblingIndex();
        child.owner.localPosition += new Vector3(0,0,zOffset * m_childIndex);
        children.Add(child);
    }

    public Transform2D GetChild(int childIndex)
    {
        if(m_childCount > childIndex)
        {
            return children[childIndex];
        }
        return null;
    }

    public void DestroyChild(Transform2D child)
    {
        for(int i = 0; i < m_childCount; i++)
        {
            if(children[i].owner == child.owner)
            {
                DestroyChild(i);
                break;
            }
        }
    }

    public void DestroyChild(int childIndex)
    {
        if(m_childCount > childIndex)
        {
            Transform trans = m_owner.GetChild(childIndex);
            GameObject.Destroy(trans.gameObject);
            children.RemoveAt(childIndex);
            m_childCount--;
        }
    }

    public void LookAt(Vector2 target)
    {
        
    }
}