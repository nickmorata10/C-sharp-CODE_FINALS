using System;
using System.Collections.Generic;

public class Program {
    public static void Main(string[] args) {
        EmployeeManager manager = new EmployeeManager();
        bool isProgramRunning = true;

        while (isProgramRunning) {
            DisplayOptions();
            Console.Write("\nEnter Option Number [1-5]: ");
            string userOption = Console.ReadLine().Trim();

            switch (userOption) {
                case "1": AddEmployeeRecord(manager); break;
                case "2": EditEmployeeRecord(manager); break;
                case "3": DeleteEmployeeRecord(manager); break;
                case "4": SearchEmployee(manager); break;
                case "5":
                    Console.WriteLine("\nExiting Program...");
                    isProgramRunning = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid Option! Please choose between 1-5 only.");
                    break;
            }
        }
    }

    public static void DisplayOptions() {
        Console.WriteLine("\n***** Cybersecurity Payroll System *****");
        Console.WriteLine("\t1. Add Employee Record");
        Console.WriteLine("\t2. Edit Employee Information");
        Console.WriteLine("\t3. Delete Employee Record");
        Console.WriteLine("\t4. Search Employee");
        Console.WriteLine("\t5. Exit Program");
    }

    public static void AddEmployeeRecord(EmployeeManager manager) {
        Console.WriteLine("\nOption #1: Adding Employee Record");
        
        int ID;
        while (true) {
            Console.Write("Enter Employee ID: ");
            if (!int.TryParse(Console.ReadLine().Trim(), out ID) || ID <= 0) {
                Console.WriteLine("Invalid input! Please enter a valid ID number.");
                continue;
            }
            if (manager.FindEmployee(ID) != null) {
                Console.WriteLine("This Employee ID already exists! Please enter a different ID.");
            } else {
                break;
            }
        }

        Console.Write("Enter Employee Name: ");
        string name = Console.ReadLine().Trim();
        Console.Write("Enter Employee Department: ");
        string department = Console.ReadLine().Trim();
        
        string status;
        while (true) {
            Console.Write("Enter Employee Status (Probational / Permanent / Contractual): ");
            status = Console.ReadLine().Trim();
            if (status.Equals("Probational", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Permanent", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Contractual", StringComparison.OrdinalIgnoreCase)) {
                break;
            }
            Console.WriteLine("Invalid status! Choose from Probational, Permanent, or Contractual.");
        }
        
        double rate;
        while (true) {
            Console.Write("Enter Employee Rate/Hr (PHP): ");
            if (!double.TryParse(Console.ReadLine().Trim(), out rate) || rate <= 0) {
                Console.WriteLine("Invalid input! Please enter a positive number.");
                continue;
            }
            break;
        }
        
        double hoursWorked;
        while (true) {
            Console.Write("Enter Hours Worked: ");
            if (!double.TryParse(Console.ReadLine().Trim(), out hoursWorked) || hoursWorked <= 0) {
                Console.WriteLine("Invalid input! Please enter a positive number.");
                continue;
            }
            break;
        }
        
        Console.Write("Enter Date Hired (MM/DD/YYYY): ");
        string dateHired = Console.ReadLine().Trim();

        Employee newEmployee = new Employee(ID, name, department, status, rate, hoursWorked, dateHired);
        manager.AddEmployee(newEmployee);
        Console.WriteLine("\nEmployee added successfully!\n");
    }

    public static void EditEmployeeRecord(EmployeeManager manager) {
        Console.Write("\nEnter Employee ID to Edit: ");
        int ID = int.Parse(Console.ReadLine().Trim());
        Employee emp = manager.FindEmployee(ID);
        if (emp == null) {
            Console.WriteLine("\nEmployee Not Found!\n");
            return;
        }
        
        Console.WriteLine("\nEditing Employee Record");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Department");
        Console.WriteLine("3. Status");
        Console.WriteLine("4. Rate per Hour");
        Console.WriteLine("5. Hours Worked");
        
        Console.Write("\nEnter field number to edit: ");
        string option = Console.ReadLine().Trim();

        switch (option) {
            case "1":
                Console.Write("Enter New Name: ");
                emp.EmployeeName = Console.ReadLine().Trim();
                break;
            case "2":
                Console.Write("Enter New Department: ");
                emp.Department = Console.ReadLine().Trim();
                break;
            case "3":
                while (true) {
                    Console.Write("Enter New Status (Probational / Permanent / Contractual): ");
                    string status = Console.ReadLine().Trim();
                    if (status.Equals("Probational", StringComparison.OrdinalIgnoreCase) ||
                        status.Equals("Permanent", StringComparison.OrdinalIgnoreCase) ||
                        status.Equals("Contractual", StringComparison.OrdinalIgnoreCase)) {
                        emp.EmployeeStatus = status;
                        emp.RecalculateSalary();
                        break;
                    }
                    Console.WriteLine("Invalid status! Choose from Probational, Permanent, or Contractual.");
                }
                break;
            case "4":
                Console.Write("Enter New Rate Per Hour (PHP): ");
                emp.RatePerHr = double.Parse(Console.ReadLine().Trim());
                emp.RecalculateSalary();
                break;
            case "5":
                Console.Write("Enter New Hours Worked: ");
                emp.HoursWorked = double.Parse(Console.ReadLine().Trim());
                emp.RecalculateSalary();
                break;
            default:
                Console.WriteLine("Invalid Option!");
                break;
        }
        Console.WriteLine("\nEmployee Record Updated!\n");
    }
}

public class EmployeeManager {
    private List<Employee> employees = new List<Employee>();

    public void AddEmployee(Employee emp) {
        employees.Add(emp);
    }

    public Employee FindEmployee(int id) {
        return employees.Find(emp => emp.EmployeeID == id);
    }

    public bool DeleteEmployee(int id) {
        Employee emp = FindEmployee(id);
        if (emp != null) {
            employees.Remove(emp);
            return true;
        }
        return false;
    }
}

public class Employee {
    // Properties and Constructor are the same as before
}
