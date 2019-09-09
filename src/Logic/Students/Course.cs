namespace Logic.Students
{
    public class Course : Entity
    {
        public Course()
        {

        }
        public Course(string name, int credits)
        {
            Name = name;
            Credits = credits;
        }

        public virtual string Name { get; protected set; }
        public virtual int Credits { get; protected set; }
    }
}
