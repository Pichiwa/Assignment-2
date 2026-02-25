using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

class Student
{
    public int StudentID { get; set; }
    public string Name { get; set; }
    public string Course { get; set; }
    public int Grade { get; set; }

    // Constructor
    public Student(int studentID, string name, string course, int grade)
    {
        StudentID = studentID;
        Name = name;
        Course = course;
        Grade = grade;
    }
}

class Program
{
    static void Main()
    {
        string filePath = "students.txt";

        // -------------------------
        // TASK 1: Create and Save File
        // -------------------------
        List<Student> students = new List<Student>
        {
            new Student(101, "Van", "BSIT", 79),
            new Student(102, "Halen", "BSCS", 82),
            new Student(103, "Jardiolen", "BSIT", 95),
            new Student(104, "Janao", "BSCS", 85),
            new Student(105, "Assignment2", "BSIT", 91)
        };

        // Write to file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.StudentID},{student.Name},{student.Course},{student.Grade}");
            }
        }

        Console.WriteLine("Students saved to file.\n");

        // -------------------------
        // TASK 2: Read File and Use LINQ
        // -------------------------

        var lines = File.ReadAllLines(filePath);

        List<Student> studentList = lines
            .Select(line =>
            {
                var parts = line.Split(',');
                return new Student(
                    int.Parse(parts[0]),
                    parts[1],
                    parts[2],
                    int.Parse(parts[3])
                );
            })
            .ToList();

        // Students with Grade > 85
        Console.WriteLine("Students with Grade > 85");
        var highGrades = studentList
            .Where(s => s.Grade > 85);

        foreach (var s in highGrades)
        {
            Console.WriteLine($"{s.Name} - {s.Grade}");
        }

        // Sorted by Grade Descending
        Console.WriteLine("\nSorted by Grade (Descending)");
        var sorted = studentList
            .OrderByDescending(s => s.Grade);

        foreach (var s in sorted)
        {
            Console.WriteLine($"{s.Name} - {s.Grade}");
        }

        // Projection (Only Names)
        Console.WriteLine("\nStudent Names Only");
        var names = studentList
            .Select(s => s.Name);

        foreach (var name in names)
        {
            Console.WriteLine(name);
        }

        // Average Grade
        double average = studentList.Average(s => s.Grade);
        Console.WriteLine($"\nAverage Grade: {average:F2}");
    }
}