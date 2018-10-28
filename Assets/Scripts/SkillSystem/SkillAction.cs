using System.Collections.Generic;
using UnityEngine;

namespace Skill
{
    public abstract class SkillAction : ISkillObject
    {
        protected Dictionary<string, object> m_attributes = new Dictionary<string, object>();

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

        public virtual void OnEnter()
        {
        }

        public virtual  void OnExit()
        {
        }

        public virtual  void Update(float deltaTime)
        {
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