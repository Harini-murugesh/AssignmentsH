// File: entity/Movie.cs
// ==========================

namespace task11.entity
{
    public class Movie : Event
    {
        public override string GetEventType()
        {
            return "Movie";
        }
    }
}