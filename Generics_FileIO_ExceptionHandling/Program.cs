using Assignment_2;
using System;
using System.Collections.Generic;
using System.IO;

namespace GenericStudentManagement
{
   
    class Program
    {
        static string studentFile = "students.txt";

        static void Main()
        {
            GenericFileHandler<string> handler = new GenericFileHandler<string>();
            handler.LoadFromFile(studentFile);

            int choice = 0;

            do
            {
                Console.WriteLine("\n--- Student Management System ---");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Search Student by ID");
                Console.WriteLine("4. Update Student Marks");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddStudent(handler);
                            break;
                        case 2:
                            ViewStudents(handler);
                            break;
                        case 3:
                            SearchStudent(handler);
                            break;
                        case 4:
                            UpdateMarks(handler);
                            break;
                        case 5:
                            handler.WriteToFile(studentFile);
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                    Console.WriteLine("Invalid input.");
                }

            } while (choice != 5);
        }

        // 3️ Add student in file
        static void AddStudent(GenericFileHandler<string> handler)
        {
            try
            {


                Console.Write("Enter StudentId: ");
                string id = Console.ReadLine();

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Age: ");
                string age = Console.ReadLine();

                Console.Write("Enter Marks: ");
                string marks = Console.ReadLine();

                string record = $"{id},{name},{age},{marks}";
                handler.Records.Add(record);

                Console.WriteLine("Student added successfully.");
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        // view all students
        static void ViewStudents(GenericFileHandler<string> handler)
        {
            foreach (var student in handler.Records)
            {
                Console.WriteLine(student);
            }
        }

        // search student by id
        static void SearchStudent(GenericFileHandler<string> handler)
        {
            Console.Write("Enter Student ID to search: ");
            string id = Console.ReadLine();

            foreach (var student in handler.Records)
            {
                if (student.StartsWith(id + ","))
                {
                    Console.WriteLine("Student Found: " + student);
                    return;
                }
            }
            Console.WriteLine("Student not found.");
        }

        // update marks by student id
        static void UpdateMarks(GenericFileHandler<string> handler)
        {
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine();

            for (int i = 0; i < handler.Records.Count; i++)
            {
                string[] data = handler.Records[i].Split(',');

                if (data[0] == id)
                {
                    Console.Write("Enter new marks: ");
                    data[3] = Console.ReadLine();

                    handler.Records[i] = string.Join(",", data);
                    Console.WriteLine("Marks updated.");
                    return;
                }
            }
            Console.WriteLine("Student not found.");
        }

        // EXCEPTION LOGGING
        static void LogError(Exception ex)
        {
            File.AppendAllText("errorlog.txt",
                $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}");
        }
    }
}


