using OxyPlot;
using OxyPlot.Series;
using OxyPlot.ImageSharp;
using OxyPlot.Axes;
using OxyPlot.Legends;

var rand = new Random();
double[] arr = new double[1000000];
for(var i=0;i<arr.Length;i++)
{
    double u1 = 1 - rand.NextDouble();
    double u2 = 1 - rand.NextDouble();
    arr[i] = Math.Sqrt(-2.0 * Math.Log(u1)) *
             Math.Sin(2.0 * Math.PI * u2);
}
double max = arr[0];
for(var i=0;i<arr.Length;i++)
{
    if(arr[i]>max) max = arr[i];
}
double min = arr[0];
for(var j=0;j<arr.Length;j++)
{
    if(arr[j]<min) min=arr[j];
}
var mean = find_mean(arr);
var variance = find_variance(arr, mean);
var hist_width = (max-min)/11;
double[] arr_bars = new double[12];
arr_bars[0] = min;
var last = min;
for(var i=1;i<12;i++) 
{   
    arr_bars[i] = last+hist_width;
    last=arr_bars[i];
}
for(var i=0;i<12;i++) 
{   
    Console.WriteLine(arr_bars[i]);
}
int[] arr_count = new int[11];

for(var i=0;i<11;i++)
{
    int count = 0;
    for(var j=0;j<arr.Length;j++)
    {
        if(arr[j]>=arr_bars[i] & arr[j]<arr_bars[i+1])
        {
            count+=1;
        }
    }
    arr_count[i] = count;
}
for(var i=0; i<arr_count.Length;i++) Console.WriteLine("Count of {0} bar is: {1}",i+1, arr_count[i]);

Console.WriteLine("Min of the array is: {0}", min);
Console.WriteLine("Max of the array is: {0}", max);
Console.WriteLine("Dx is: {0}", hist_width);
Console.WriteLine("Mean of array is {0}, variance is {1}", mean, variance);



static double find_mean(double []a)
{
    double sum=0;
    for(var i=0;i<a.Length;i++)
        sum+=a[i];
    double mean = (double)sum / (double)a.Length;
    return mean;
}
static double find_variance(double []a, double Mean)
{
    double SqDiff = 0;
    for(var i=0;i<a.Length;i++)
    {
        SqDiff += (a[i] - Mean) *(a[i] - Mean);
    }
    return SqDiff / a.Length;
}
