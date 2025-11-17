using System;
using System.IO;
using System.Runtime.Remoting.Lifetime;
using System.Text;

class Test
{
    public static void Main()
    {
        string path = @"RPGMap.txt"; //Getting the file as a string
        string[] map = File.ReadAllLines(path, Encoding.UTF8); //Reading all lines of the file into an array

        foreach (string line in map)
        {
            Console.WriteLine(line);
        }

    }
}
