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
var csv_students = SaveStudents("students.csv", students); //сделали сиэсвишку
var avg_rating = students.Select(s => s.Rating).Average(); //называется конвеер
var best_students = students.OrderByDescending(s => s.Rating)
                            .Take(5); //как лимит в mysql
var best_stud_rating_avg = best_students.Average(s => s.Rating);
Console.WriteLine(avg_rating);
Console.WriteLine(best_stud_rating_avg);
foreach(var s in best_students) Console.WriteLine(s);
foreach(var s in students.Where(s => s.Second_name[0]=='К')) Console.WriteLine(s); //ну вот так вот where делаем
Readcsv(name_csv_file, out var ID,
                    out var first_,
                    out var last_,
                    out var patr_,
                    out var Rate);
// foreach(var s in students) Console.WriteLine(s); вывели всех
/* foreach(var name in first)
{
    Console.WriteLine(name);
} */
/* var reader = names_file.OpenText();

while(!reader.EndOfStream) //так читаются файлы, записали в переменную по разделителю и вывели нулевой(фамилии)
{
    var line = reader.ReadLine();
    var components = line.Split(' ');
    if(components.Length < 3) continue;
    Console.WriteLine(components[0]);
} */
static void ReadNames(FileInfo file, 
        out List<string> Last, //out - передача параметра по ссылке, запрет модификации из вне, 
        out List<string> First, //используем, когда хотим вернуть из функции больше одного значения
        out List<string> Patr)  //ref - другой модификатор, работают одинаково, используются по разному
{
    Last = new();
    First = new();
    Patr = new();
    var reader = file.OpenText();

    while(!reader.EndOfStream) //так читаются файлы, записали в переменную по разделителю и вывели нулевой(фамилии)
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
    var rnd = new Random(5);          //rnd.Next() - генерирует случайное челое число(оч больше), если не ограничить
    for (var i = 0; i<Count; i++)    //rnd.Next(1,101) или .Next(101) - ограничили от 1 до 100)))
    {                                //rnd.NextDouble() - генерирует от нуля до 1, а параметр Next это сид
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
    var writer = data_file.CreateText(); //внутри writer будет StreamWriter
    writer.WriteLine("Id;Last Name;First Name;Patronymic;Rating");
    foreach(var s in students) //дальше можно делать по одной записи .Write(s.Id), .Write(;) и так далее
    {                           //можно построчно Write($"{s.Id};{s.LastName}....")
        writer.WriteLine(String.Join(';',   //а это нам надо, String.Concat сливает строки, а join делает разделитель и сливает
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

    while (!reader.EndOfStream) //так читаются файлы, записали в переменную по разделителю и вывели нулевой(фамилии)
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
//сделать чтение csv файла по аналогии и запринтить всех студентов