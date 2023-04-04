using System.IO;
using students_work;

const string names_file_path = "Names.txt";
const string name_csv_path = "students.csv";
FileInfo name_csv_file = new(name_csv_path);
FileInfo names_file = new(names_file_path);

if(!names_file.Exists)
{
    Console.WriteLine("Файл с именами не найден!");
    return;
}
ReadNames(names_file, out var last,
                    out var first,
                    out var patr);
var students = CreateStudents(1000, last, first, patr);
var csv_students = SaveStudents("students.csv", students); 
var avg_rating = students.Select(s => s.Rating).Average(); 
var best_students = students.OrderByDescending(s => s.Rating)
                            .Take(5); 
var best_stud_rating_avg = best_students.Average(s => s.Rating);
Console.WriteLine(avg_rating);
Console.WriteLine(best_stud_rating_avg);
foreach(var s in best_students) Console.WriteLine(s);
foreach(var s in students.Where(s => s.Second_name[0]=='К')) Console.WriteLine(s); 
Readcsv(name_csv_file, out var ID,
                    out var first_,
                    out var last_,
                    out var patr_,
                    out var Rate);

static void ReadNames(FileInfo file, 
        out List<string> Last, 
        out List<string> First, 
        out List<string> Patr) 
{
    Last = new();
    First = new();
    Patr = new();
    var reader = file.OpenText();

    while(!reader.EndOfStream) 
    {
        var line = reader.ReadLine();
        var components = line.Split(' ');
        if(components.Length < 3) continue;
        var last = components[0];
        var first = components[1];
        var patr = components[2];
        Last.Add(last);
        First.Add(first);
        Patr.Add(patr);
    } 
}

static Student[] CreateStudents(int Count,
                                List<string> Last,
                                List<string> First,
                                List<string> Patr)
{
    var students = new Student[Count];
    var rnd = new Random(5);          
    for (var i = 0; i<Count; i++)    
    {                                
        var student = new Student();
        students[i] = student;
        student.Id = i+1;
        student.Second_name = Last[rnd.Next(Last.Count)];
        student.Name = First[rnd.Next(First.Count)];
        student.Third_name = Patr[rnd.Next(Patr.Count)];
        student.Rating = rnd.NextDouble() * 100;
    }
    return students;
}                                
static FileInfo SaveStudents(string FilePath, Student[] students)
{
    var data_file = new FileInfo(FilePath);
    var writer = data_file.CreateText(); 
    writer.WriteLine("Id;Last Name;First Name;Patronymic;Rating");
    foreach(var s in students) 
    {                           
        writer.WriteLine(String.Join(';',   
                        s.Id,
                        s.Name,
                        s.Second_name,
                        s.Third_name,
                        s.Rating));          
    }
    return data_file;
}
static void Readcsv(FileInfo file,
    out List<int> ID,
    out List<string> Last,
    out List<string> First,
    out List<string> Patr,
    out List<double> Rate)
{
    ID = new List<int>();
    Last = new();
    First = new();
    Patr = new();
    Rate = new();
    var reader = file.OpenText();
    int com1;
    double com2;

    while (!reader.EndOfStream) 
    {
        var line = reader.ReadLine();
        var components = line.Split(';');
        if (int.TryParse(components[0], out com1) & double.TryParse(components[4], out com2)) continue;
        var id = com1;
        var last = components[1];
        var first = components[2];
        var patr = components[3];
        var rate = com2;
        ID.Add(id);
        Last.Add(last);
        First.Add(first);
        Patr.Add(patr);
        Rate.Add(rate);
    }
}
