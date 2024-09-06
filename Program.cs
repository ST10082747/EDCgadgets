using System;
using System.Collections.Generic;

namespace EDCLinkedList
{
    // Define Gadget class
    class Gadget
    {
        public string Name { get; set; }
        public HashSet<DayOfWeek> CarryDays { get; set; }

        public Gadget(string name, HashSet<DayOfWeek> carryDays)
        {
            Name = name;
            CarryDays = carryDays;
        }

        public override string ToString()
        {
            return $"{Name} - Carried on: {string.Join(", ", CarryDays)}";
        }
    }

    // Define Node class for LinkedList
    class Node
    {
        public Gadget Gadget { get; set; }
        public Node Next { get; set; }

        public Node(Gadget gadget)
        {
            Gadget = gadget;
            Next = null;
        }
    }

    // Define Custom LinkedList class
    class CustomLinkedList
    {
        private Node head;
        private int count;

        public CustomLinkedList()
        {
            head = null;
            count = 0;
        }

        // Add gadget to the end of the list
        public void AddGadget(Gadget gadget)
        {
            Node newNode = new Node(gadget);
            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            count++;
        }

        // Remove gadget by name
        public bool RemoveGadget(string name)
        {
            if (head == null)
                return false;

            if (head.Gadget.Name == name)
            {
                head = head.Next;
                count--;
                return true;
            }

            Node current = head;
            while (current.Next != null && current.Next.Gadget.Name != name)
            {
                current = current.Next;
            }

            if (current.Next != null)
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            return false;
        }

        // Find a gadget by name
        public Gadget FindGadget(string name)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Gadget.Name == name)
                {
                    return current.Gadget;
                }
                current = current.Next;
            }
            return null;
        }

        // Count gadgets
        public int CountGadgets()
        {
            return count;
        }

        // Insert gadget at a specific position
        public bool InsertGadgetAt(int position, Gadget gadget)
        {
            if (position < 0 || position > count)
                return false;

            Node newNode = new Node(gadget);

            if (position == 0)
            {
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                Node current = head;
                for (int i = 0; i < position - 1; i++)
                {
                    current = current.Next;
                }
                newNode.Next = current.Next;
                current.Next = newNode;
            }
            count++;
            return true;
        }

        // Reverse the list
        public void ReverseList()
        {
            Node prev = null;
            Node current = head;
            Node next = null;
            while (current != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            head = prev;
        }

        // Clear the list
        public void ClearList()
        {
            head = null;
            count = 0;
        }

        // Display the list
        public void DisplayList()
        {
            Node current = head;
            while (current != null)
            {
                Console.WriteLine(current.Gadget);
                current = current.Next;
            }
        }

        // Display gadgets based on the day of the week
        public void DisplayByDay(DayOfWeek day)
        {
            Node current = head;
            Console.WriteLine($"Gadgets for {day}:");
            while (current != null)
            {
                if (current.Gadget.CarryDays.Contains(day))
                {
                    Console.WriteLine(current.Gadget);
                }
                current = current.Next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList gadgetList = new CustomLinkedList();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a Gadget");
                Console.WriteLine("2. Remove a Gadget");
                Console.WriteLine("3. Find a Gadget");
                Console.WriteLine("4. Display All Gadgets");
                Console.WriteLine("5. Display Gadgets for a Specific Day");
                Console.WriteLine("6. Insert a Gadget at a Specific Position");
                Console.WriteLine("7. Reverse the List");
                Console.WriteLine("8. Clear the List");
                Console.WriteLine("9. Count Gadgets");
                Console.WriteLine("0. Exit");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddGadget(gadgetList);
                        break;
                    case "2":
                        RemoveGadget(gadgetList);
                        break;
                    case "3":
                        FindGadget(gadgetList);
                        break;
                    case "4":
                        gadgetList.DisplayList();
                        break;
                    case "5":
                        DisplayByDay(gadgetList);
                        break;
                    case "6":
                        InsertGadget(gadgetList);
                        break;
                    case "7":
                        gadgetList.ReverseList();
                        Console.WriteLine("The gadget list has been reversed.");
                        break;
                    case "8":
                        gadgetList.ClearList();
                        Console.WriteLine("The list has been cleared.");
                        break;
                    case "9":
                        Console.WriteLine($"Total gadgets: {gadgetList.CountGadgets()}");
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // Method to add a gadget
        static void AddGadget(CustomLinkedList gadgetList)
        {
            Console.Write("Enter the name of the gadget: ");
            string name = Console.ReadLine();

            HashSet<DayOfWeek> carryDays = new HashSet<DayOfWeek>();
            Console.WriteLine("Enter the days you carry this gadget (e.g., Monday, Tuesday). Type 'done' to finish:");
            while (true)
            {
                string dayInput = Console.ReadLine();
                if (dayInput.Equals("done", StringComparison.OrdinalIgnoreCase))
                    break;

                if (Enum.TryParse(dayInput, out DayOfWeek day))
                {
                    carryDays.Add(day);
                }
                else
                {
                    Console.WriteLine("Invalid day. Please try again.");
                }
            }

            Gadget newGadget = new Gadget(name, carryDays);
            gadgetList.AddGadget(newGadget);
            Console.WriteLine($"{name} has been added to the gadget list.");
        }

        // Method to remove a gadget
        static void RemoveGadget(CustomLinkedList gadgetList)
        {
            Console.Write("Enter the name of the gadget to remove: ");
            string name = Console.ReadLine();

            bool isRemoved = gadgetList.RemoveGadget(name);
            if (isRemoved)
            {
                Console.WriteLine($"{name} has been removed from the list.");
            }
            else
            {
                Console.WriteLine("Gadget not found.");
            }
        }

        // Method to find a gadget
        static void FindGadget(CustomLinkedList gadgetList)
        {
            Console.Write("Enter the name of the gadget to find: ");
            string name = Console.ReadLine();

            Gadget foundGadget = gadgetList.FindGadget(name);
            if (foundGadget != null)
            {
                Console.WriteLine($"Found gadget: {foundGadget}");
            }
            else
            {
                Console.WriteLine("Gadget not found.");
            }
        }

        // Method to display gadgets for a specific day
        static void DisplayByDay(CustomLinkedList gadgetList)
        {
            Console.Write("Enter the day to display gadgets for (e.g., Monday): ");
            string dayInput = Console.ReadLine();

            if (Enum.TryParse(dayInput, out DayOfWeek day))
            {
                gadgetList.DisplayByDay(day);
            }
            else
            {
                Console.WriteLine("Invalid day.");
            }
        }

        // Method to insert a gadget at a specific position
        static void InsertGadget(CustomLinkedList gadgetList)
        {
            Console.Write("Enter the name of the gadget: ");
            string name = Console.ReadLine();

            HashSet<DayOfWeek> carryDays = new HashSet<DayOfWeek>();
            Console.WriteLine("Enter the days you carry this gadget (e.g., Monday, Tuesday). Type 'done' to finish:");
            while (true)
            {
                string dayInput = Console.ReadLine();
                if (dayInput.Equals("done", StringComparison.OrdinalIgnoreCase))
                    break;

                if (Enum.TryParse(dayInput, out DayOfWeek day))
                {
                    carryDays.Add(day);
                }
                else
                {
                    Console.WriteLine("Invalid day. Please try again.");
                }
            }

            Console.Write("Enter the position where you want to insert the gadget (starting from 0): ");
            if (int.TryParse(Console.ReadLine(), out int position))
            {
                Gadget newGadget = new Gadget(name, carryDays);
                bool success = gadgetList.InsertGadgetAt(position, newGadget);
                if (success)
                {
                    Console.WriteLine($"{name} has been inserted at position {position}.");
                }
                else
                {
                    Console.WriteLine("Invalid position.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input for position.");
            }
        }
    }
}

