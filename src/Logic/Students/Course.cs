using System;

namespace Logic.Students
{
    public class Course : Entity
    {
        protected Course() { }

        public Course(string name, int credits)
        {
            Id = Guid.NewGuid();
            Name = name;
            Credits = credits;
        }

        public virtual string Name { get; protected set; }
        public virtual int Credits { get; protected set; }
    }
}
