using StudentsDBMS.Data;
using StudentsDBMS.Models;

namespace StudentsDBMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            static void DisplayMenu()
            {
                while (true)
                {
                    Console.WriteLine("\nStudents DBMS");
                    Console.WriteLine("1. Show Students Table");
                    Console.WriteLine("2. Add record");
                    Console.WriteLine("3. Update record");
                    Console.WriteLine("4. Delete record");
                    Console.WriteLine("5. Exit");
                    Console.Write("Press one number to perform an action: ");
                    string? input = Console.ReadLine();

                    if(string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Invalid input, please try again.");
                        continue;
                    }

                    switch(input)
                    {
                        case "1":
                            ShowStudentsTable();
                            break;
                        case "2":
                            AddRecord();
                            break;
                        case "3":
                            EditRecord();
                            break;
                        case "4":
                            DeleteRecord();
                            break;
                        case "5":
                            Console.WriteLine("Exiting the application...");
                            return;
                        default:
                            Console.WriteLine("Invalid option, please enter a valid number");
                            break;
                    }    
                }
            }

            static void ShowStudentsTable()
            {
                using var context = new StudentDbContext();
                var students = context.Students.ToList();
                Console.WriteLine("\nStudents Table: ");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade {student.Grade}");
                }
            }

            static void AddRecord()
            {
                using var context = new StudentDbContext();
                var student = new Student();

                Console.Write("Enter name: ");
                student.Name = Console.ReadLine() ??  "Unknown";

                Console.Write("Enter age: ");
                student.Age = int.TryParse(Console.ReadLine(), out int age) ? age : 0 ;

                Console.Write("Enter grade: ");
                student.Grade = Console.ReadLine() ?? "Not assigned";

                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Record added successfully.");
            }

            static void EditRecord()
            {
                using var context = new StudentDbContext();
                Console.Write("Enter Student ID to edit: ");
                if(int.TryParse(Console.ReadLine(), out int id) )
                {
                    var student = context.Students.Find(id);
                    if(student != null)
                    {
                        Console.Write("Enter new Name:");
                        student.Name = Console.ReadLine() ?? student.Name;

                        Console.Write("Enter new Age:");
                        student.Age = int.TryParse(Console.ReadLine(), out int age) ? age : student.Age;

                        Console.Write("Enter new Grade:");
                        student.Grade = Console.ReadLine() ?? student.Grade;

                        context.SaveChanges();
                        Console.WriteLine("Record edited successfully");
                    }
                    else
                    {
                        Console.WriteLine("Student not found");
                    }
                }
            }

            static void DeleteRecord()
            {
                using var context = new StudentDbContext();
                Console.Write("Enter Student ID to delete: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    var student = context.Students.Find(id);
                    if(student != null)
                    {
                        context.Students.Remove(student);
                        context.SaveChanges();
                        Console.WriteLine("Student deleted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Student not found");
                    }
                }
            }
        }
    }
}
