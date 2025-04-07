// File: entity/Concert.cs
// ==========================

namespace task11.entity
{
    public class Concert : Event
    {
        public override string GetEventType()
        {
            return "Concert";
        }
    }
}

