using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public enum Comparer
    {
        Equal,
        LessThan,
        LessOrEqual,
        GreaterThan,
        GreaterOrEqual
    }

    public abstract class SkillCondition : ISkillObject
    {
        protected Dictionary<string, object> m_attributes = new Dictionary<string, object>();
        public bool established { get; private set; }
        public Comparer comparer { get; set; }

        public string name
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public Transform owner
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public SkillCondition() { }

        public void OnEnter()
        {
        }

        public void Update()
        {
        }

        public void OnExit()
        {
        }

        public abstract bool OnValidate();

        public virtual void SetEstablish(bool established)
        {
            this.established = established;
        }

        public void Update(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public virtual object this[string key]
        {
            set
            {
                m_attributes[key] = value;
            }
            get
            {
                if (m_attributes.ContainsKey(key))
                {
                    return m_attributes[key];
                }
                return null;
            }
        }
    }
}