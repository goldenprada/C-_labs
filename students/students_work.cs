namespace students_work;

class Student
{
    public int Id{get; set;}
    public string Name{get; set;}
    public string Second_name{get; set;}
    public string Third_name{get; set;}
    public double Rating{get; set;}
    public override string ToString()
    {
        return $"[{Id}] {Second_name} {Name} {Third_name} {Rating}";
    }
}