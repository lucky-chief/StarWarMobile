using System.Collections.Generic;
namespace Skill
{
    public sealed class State
    {
        public Dictionary<string, SkillAction> actions { get; private set; }
        public Dictionary<string, SkillTransition> transitions { get; private set; }
        public bool isDefault { get; set; }
        public string name { get; private set; }

        public State(string name, bool isDefault = false)
        {
            this.name = name;
            this.isDefault = isDefault;
            actions = new Dictionary<string, SkillAction>();
            transitions = new Dictionary<string, SkillTransition>();
        }

        public void OnEnter()
        {
            foreach (KeyValuePair<string, SkillAction> action in actions)
            {
                action.Value.OnEnter();
            }
            foreach (KeyValuePair<string, SkillTransition> transition in transitions)
            {
                transition.Value.OnEnter();
            }
        }

        public void Update(float deltaTime)
        {

            foreach (KeyValuePair<string, SkillAction> action in actions)
            {
                action.Value.Update(deltaTime);
            }
        }

        public bool Validate(out SkillTransition outTransition)
        {
            outTransition = null;
            foreach (KeyValuePair<string, SkillTransition> transition in transitions)
            {
                if (transition.Value.Validate())
                {
                    OnExit();
                    outTransition = transition.Value;
                    return true;
                }
            }
            return false;
        }

        public void OnExit()
        {
            foreach (KeyValuePair<string, SkillAction> action in actions)
            {
                action.Value.OnExit();
            }
            foreach (KeyValuePair<string, SkillTransition> transition in transitions)
            {
                transition.Value.OnExit();
            }
        }

        public void AddAction(SkillAction action)
        {
            if (actions.ContainsKey(action.name))
            {
                UnityEngine.Debug.LogError("状态" + name + "已经包含名为：" + action.name + "的动作！");
            }
            else
            {
                actions.Add(action.name, action);
            }
        }

        public void AddTransition(SkillTransition transition)
        {
            if (transitions.ContainsKey(transition.name))
            {
                UnityEngine.Debug.LogError("状态" + name + "已经包含名为：" + transition.name + "的过渡条件！");
                return;
            }
            if (transition.fromName != name)
            {
                UnityEngine.Debug.LogError("过渡条件的起始状态不是名为 " + name + "的状态！");
                return;
            }

            transitions.Add(transition.name, transition);
        }

    }
}