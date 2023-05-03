using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI.GOAP.Agents 
{
    public abstract class AgentsContainerAbstract<TAgentInfo>
    {
        public List<TAgentInfo> AgentInfosList { get; private set; }

        public AgentsContainerAbstract() 
        {
            AgentInfosList = new();
        }
    }
}