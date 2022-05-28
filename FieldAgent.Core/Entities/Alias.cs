using System;

namespace FieldAgent.Core.Entities
{
    public class Alias
    {
        public int AliasId { get; set; }
        public string AliasName { get; set; }
        public Guid? InterpolId { get; set; } 
        public string Persona { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
