using System.Collections.Generic;
namespace Skill
{
    public sealed class SkillTransition
    {
        public string name { get; private set; }
        public Skill skill { get; set; }
        public string fromName { get; private set; }
        public string toName { get; private set; }
        public Dictionary<string, SkillCondition> conditions { get; private set; }

        public SkillTransition(string name,string from,string to)
        {
            this.name = name;
            fromName = from;
            toName = to;
            conditions = new Dictionary<string, SkillCondition>();
        }


        public void OnEnter()
        {
            foreach (KeyValuePair<string, SkillCondition> condition in conditions)
            {
                condition.Value.OnEnter();
            }
        }

        public void OnExit()
        {
            foreach (KeyValuePair<string, SkillCondition> condition in conditions)
            {
                condition.Value.OnExit();
            }
        }

        public void AddCondition(SkillCondition condition)
        {
            if (conditions.ContainsKey(condition.name))
            {
                UnityEngine.Debug.LogError("状态" + name + "已经包含名为：" + condition.name + "的动作！");
            }
            else
            {
                conditions.Add(condition.name, condition);
            }
        }

        public void RmCondition(string conditionName)
        {
            conditions.Remove(conditionName);
        }

        public bool Validate()
        {
            bool established = false;
            foreach (KeyValuePair<string, SkillCondition> condition in conditions)
            {
                established = condition.Value.OnValidate();
                //如果有一个条件没满足，则返回
                if (!established)
                {
                    break;
                }
            }
            return established;
        }
    }
}
