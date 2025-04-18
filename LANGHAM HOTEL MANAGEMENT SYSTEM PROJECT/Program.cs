/* 
 * Project Name:LANGHAM Hotel Management System
 * Author Name:Manpreet Kaur
 * Date:19/04/2025
 * Application Purpose:To manage hotel room allocation, customer records,file storage,and backups with exception handling
 * 
 */
using System;
using System.Collections.Generic;
using System.IO;

namespace LanghamHotelManagementSystem
{
    // Room class
    public class Room
    {
        public int RoomNo { get; set; }
        public bool IsAllocated { get; set; }
    }

    // Customer class
    public class Customer
    {
        public int CustomerNo { get; set; }
        public string CustomerName { get; set; }
    }

    // Room Allocation class
    public class RoomAllocation
    {
        public int AllocatedRoomNo { get; set; }
        public Customer AllocatedCustomer { get; set; }
    }

    class Program
    {
        public static List<Room> listOfRooms = new List<Room>();
        public static List<RoomAllocation> roomAllocations = new List<RoomAllocation>();
        public static string filePath;
        // Main function
        static void Main(string[] args)
        {
            string folderPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");
            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("****************************************************************
                * ******************");
                Console.WriteLine(" LANGHAM HOTEL MANAGEMENT SYSTEM
                ");
                Console.WriteLine(" MENU
                ");
                Console.WriteLine("****************************************************************
                * ******************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                // Add new option 0 for Backup
                Console.WriteLine("****************************************************************
                * ******************");
                Console.Write("Enter Your Choice Number Here:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddRooms();
                        break;

                    case 2:
                        DisplayRooms();
                        break;

                    case 3:
                        // allocate Room To Customer function
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        break;
                    case 5:
                        // display Room Alocations function;
                        break;
                    case 6:
                        // Display "Billing Feature is Under Construction and will
                        be added soon...!!!"
                break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        break;
                    case 8:
                        //Show Room Allocations From File
                        break;
                    case 9:
                        // Exit Application
                        break;
                    default:
                        break;
                }
                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
        public static void AddRooms()
        {
            try
            {
                Console.Write("Please Enter the Total Number of Rooms in the Hotel: ");
                int totalRooms = Convert.ToInt32(Console.ReadLine());

                listofRooms = new Room[totalRooms];

                for (int i = 0; i < totalRooms; i++)
                {
                    Console.Write("Please enter the Room Number: ");
                    int roomNo = Convert.ToInt32(Console.ReadLine());

                    listofRooms[i] = new Room(roomNo);
                }

                Console.WriteLine("\nRooms added successfully!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter numeric values only.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public static void DisplayRooms()
        {
            try
            {
                if (listofRooms == null || listofRooms.Length == 0)
                {
                    Console.WriteLine("No rooms have been added yet.");
                    return;
                }

                Console.WriteLine("\nRoom No\t\tStatus");
                Console.WriteLine("--------------------------------");

                foreach (Room room in listofRooms)
                {
                    string status = room.IsAllocated ? "Allocated" : "Available";
                    Console.WriteLine($"{room.RoomNo}\t\t{status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying rooms: {ex.Message}");
            }
        }


    }
}
