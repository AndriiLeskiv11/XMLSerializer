using System.Xml;
using System.Xml.Serialization;

namespace Lesson14Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees=new List<Employee>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Employee>));

            int i = 0;
            while (i<100)
            {
                Console.WriteLine("You wont \"show\" information about employers or \"add\" new employer or \"exit\" to end  program");
                string enter=(Console.ReadLine()).ToLower();
                if (enter == "add")
                {
                    Console.Clear();
                    Employee newEmployee = new Employee();
                    Console.WriteLine("Enter Name:");
                    newEmployee.Name = Console.ReadLine();
                    Console.WriteLine("Enter Surname:");
                    newEmployee.Surname = Console.ReadLine();
                    Console.WriteLine("Enter Position:");
                    newEmployee.Position = Console.ReadLine();
                    Console.WriteLine("Enter Salary:");

                    if (double.TryParse(Console.ReadLine(), out double salary))
                    {
                        newEmployee.Salary = salary;
                    }
                    else
                    {
                        Console.WriteLine("Invalid salary value. Please try again.");
                        continue;
                    }
                    employees.Add(newEmployee);

                    using (var streamWriter = new StreamWriter("Employees.xml",append:false))
                    {
                        serializer.Serialize(streamWriter, employees);
                    }
                    Console.WriteLine("Your class is serialize");
                    i++;

                }
                else if (enter == "show")
                {
                    Console.Clear ();
                    if (File.Exists("Employees.xml") && new FileInfo("Employees.xml").Length > 0)
                    {
                        using (var streamReader = new StreamReader("Employees.xml"))
                        {
                            employees = (List<Employee>)serializer.Deserialize(streamReader);
                        }

                        foreach (var employee in employees)
                        {
                            Console.WriteLine("Name: " + employee.Name);
                            Console.WriteLine("Surname: " + employee.Surname);
                            Console.WriteLine("Position: " + employee.Position);
                            Console.WriteLine("Salary: " + employee.Salary);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employee data found.Try to add");
                    }
                }
                else if(enter == "exit")
                {
                    Environment.Exit(0);
                }
                else { Console.WriteLine("Wrong input try again"); }

                
            }
        }
    }
}   