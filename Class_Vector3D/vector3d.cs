Vector3D vector1 = new(5,2,4);
Vector3D vector3 = new(0,0,1);
var vector2 = vector1 * 2;
double l = vector2.len;
Console.WriteLine("The length of the vector {0} is {1}", vector2, l);
var sum = vector2 + vector1;
var phi = vector3^vector1;
double dot_product = vector1.dot(vector1, vector3);
var cross_product = vector1.cross(vector1, vector3);
var projectile = vector1.vector_proj(vector1, vector3);
string plane = "OX";
var proj_plane = vector1.vector_plane(vector1, plane);
Console.WriteLine("The dot product of the vectors {0} and {1} is {2} ", vector1, vector3, dot_product);
Console.WriteLine("The angle between the vectors {0} and {1} is {2}", vector1, vector3, phi * 180 / Math.PI);
Console.WriteLine("The projection lenght of the vector {0} on the {1} is {2} ", vector1, vector3, projectile);
Console.WriteLine("The projection of the vector {0} on the plane {1} is {2} ", vector1, plane, proj_plane);

class Vector3D
{
    const double eps = 1e-6; //константа, нужная для понимания, являются ли прямые параллельными
    private double _x;
    private double _y;
    private double _z;
    public double len => Math.Sqrt(x*x + y*y + z*z); //длина вектора
    
    public double x
    {
        get => _x;
        set => _x = value;
    }
    public double y
    {
       get => _y;
       set => _y = value; 
    }
    public double z
    {
        get => _z;
        set => _z = value;
    }
    public Vector3D(double X, double Y, double Z)
    {
        Console.WriteLine("Vector ({0}, {1}, {2}) created", X, Y, Z);
        x = X;
        y = Y;
        z = Z;
    }
    public override string ToString()
    {
        return $"({x}, {y}, {z})"; //строковая интерполяция, доллар подставляет переменные сюдам-с
    }
    public static Vector3D operator *(Vector3D a, double b) //умножение вектора на число
    {
        return new(a.x * b, a.y * b, a.z * b);
    }
    public static Vector3D operator +(Vector3D a, Vector3D b) //сумма векторов
    {
        return new(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static Vector3D operator -(Vector3D a, Vector3D b) //разность векторов
    {
        return new(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static double operator ^(Vector3D a, Vector3D b) //угол между векторами
    { 
        if (Math.Acos((a.dot(a,b)) / (a.len * b.len)) < eps)
        {
            return 0;
        }
        else
        {
            return (Math.Acos((a.dot(a,b)) / (a.len * b.len)));
        }
        
    }
    public double dot(Vector3D a, Vector3D b) //скалярное произведение векторов
    {
        return (a.x * b.x + a.y * b.y + a.z * b.z);
    }
    public Vector3D cross(Vector3D a, Vector3D b) //векторное произведение векторов
    {
        return new(a.y * b.z - a.z * b.y, -(a.x * b.z - a.z * b.x), a.x * b.y - a.y * b.x);
    }
    public double vector_proj(Vector3D a, Vector3D b) //числовая проекция вектора а на вектор b
    {
        return((a.dot(a,b)) / b.len);
    }
    public Vector3D vector_plane(Vector3D a, string b) //проекция вектора на координатную плоскость
    {
        int len = b.Length;
        if (len != 2)
        {
            throw new ArgumentException("Plane must contain only 2 characters, please check your input");
        } 
        else
        {
            if (b=="XY")
            {
                return new(a.x, a.y, 0);
            }
            else if (b=="XZ")
            {
                return new(a.x, 0, a.z);
            }
            else
            {
                return new(0, a.y, a.z);
            }
        }
    }
}