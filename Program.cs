using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace linqExercise3
{
    class Program
    {

        public class Course
        {
            public string name;
            public int mark, units;
            public Course(string name, int mark, int units)
            {
                this.name = name;
                this.mark = mark;
                this.units = units;
            }
        }
        public class Student
        {
            public string name, family, ID, father;
            public DateTime birthYear;
            public List<Course> courses;
            public Student(string name = "no name", string family = "no family", string ID = "no id",
                string father = "no father", DateTime birthYear = default, List<Course> courses = default)
            {
                this.name = name;
                this.family = family;
                this.ID = ID;
                this.father = father;
                this.birthYear = birthYear;
                this.courses = courses;
            }

            public List<Course> GetCourse()
            {
                courses = new List<Course>();
                Console.WriteLine("How many course does the student have?");
                int courseCount = int.Parse(Console.ReadLine());
                for (int i = 0; i < courseCount; i++)
                {
                    Course c;
                    Console.WriteLine("Enter the next course information:\n" +
                        "course name, mark, units");
                    c = new Course(Console.ReadLine(), int.Parse(Console.ReadLine()), int.Parse(Console.ReadLine()));
                    courses.Add(c);
                }
                return courses;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("If you want to enter students data by yourself, Enter 1.\n" +
                "else if you prefer to use provided data, press another number(recommended).");
            int flag = int.Parse(Console.ReadLine());
            List<Student> students = new List<Student>();
            if (flag == 1)
            {
                Console.WriteLine("flag detected");
                for (int i = 0; i < 1; i++)
                {
                    Student student = new Student();
                    List<Course> courses = new List<Course>();
                    courses = student.GetCourse();
                    Console.WriteLine("Enter the student information\n" +
                        "name, family, id, father's name, birth day(as mm/dd/yyyy)");
                    student = new Student(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine()
                       , DateTime.Parse(Console.ReadLine()), courses);
                    students.Add(student);
                }
                Console.WriteLine("We are good to go! Enter any key to continue:");
                Console.ReadLine();
                Console.Clear();
                goto Modification;
            }

            students = new List<Student>{
                    new Student{name = "mehrab", family = "mehraban", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/11/1999"), courses = new List<Course>{new
                    Course("fizik", 20, 3), new Course("shimi", 10, 3), new Course("zist", 7, 3),  new Course("riazi", 20, 3)
                    }},
                    new Student{name = "sara", family = "sarane", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/13/1999"), courses = new List<Course>{new
                    Course("fizik", 20, 3), new Course("shimi", 10, 3), new Course("zist", 5, 3),  new Course("riazi", 20, 3)
                    }},
                    new Student{name = "javid", family = "javidi", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/11/1999"), courses = new List<Course>{new
                    Course("fizik", 10, 3), new Course("shimi", 10, 3), new Course("zist", 5, 3),  new Course("riazi", 20, 3)
                    }},
                    new Student{name = "ali", family = "alifar", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/11/2000"), courses = new List<Course>{new
                    Course("fizik", 20, 3), new Course("shimi", 10, 3), new Course("zist", 5, 3),  new Course("riazi", 20, 3)
                    }},
                    new Student{name = "tiran", family = "tirandaz", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/11/1999"), courses = new List<Course>{new
                    Course("fizik", 20, 3), new Course("shimi", 20, 3), new Course("zist", 5, 3),  new Course("riazi", 20, 3)
                    }},
                    new Student{name = "taha", family = "tahami", ID = "92213021", father = "mohammad",
                    birthYear =  DateTime.Parse("11/11/1999"), courses = new List<Course>{new
                    Course("fizik", 20, 3), new Course("shimi", 10, 3), new Course("zist", 7, 3),  new Course("riazi", 20, 3)
                    }} }

        ;
        Modification:
            var modifiedStudents = students.Where(x => CheckMarks(x.courses)
            && x.family.Contains(x.name)
            && x.birthYear.Month % 2 == 1)
                .GroupBy(x => x.courses.Min(x => x.mark))
                .Select(x => new
                {
                    key = x.Key,
                    values = x.GroupBy(y => y.birthYear.Year.ToString().ToList().Sum(y => y - '0'))
                    .Select(y => new
                    {
                        key = y.Key,
                        values = y.OrderByDescending(t => t.family)
                    })
                    .ToList()
                });

            modifiedStudents.ToList().ForEach(x =>
            {
                Console.WriteLine($"key1(grouped by minimum mark): {x.key}");
                x.values.ToList().ForEach(y =>
                {
                    Console.WriteLine($"\tkey2(grouped by birth year sum): {y.key}");
                    y.values.ToList().ForEach(z =>
                    {
                        Console.Write($"\t\tName: {z.name}, Family: {z.family}, Birth month: {z.birthYear.Month}, Birth Year: {z.birthYear.Year}, Marks:");
                        foreach (var course in z.courses)
                        {
                            Console.Write($" {course.mark}-");
                        }
                        Console.WriteLine();
                    });
                });
            });

        }
        public static Boolean CheckMarks(List<Course> list)
        {
            int count = 0;
            foreach (var item in list)
            {
                if (item.mark < 12)
                {
                    count++;
                    if (count == 2) return true;
                }
            }
            return false;
        }
    }
}
