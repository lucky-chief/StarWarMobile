using UnityEngine;

public class Transform2D
{
    private Transform m_owner;
    private Vector2 m_position;
    private Vector2 m_localPosition;
    private Vector2 m_localScale;
    private Vector2 m_forword;
    private Vector2 m_right;

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

    public Transform2D(Transform owner)
    {
        m_owner = owner;
    }

    ~Transform2D()
    {
        m_owner = null;
    }
}