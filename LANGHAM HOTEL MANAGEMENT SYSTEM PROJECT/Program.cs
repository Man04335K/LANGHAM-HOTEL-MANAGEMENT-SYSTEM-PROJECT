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
using System.Linq;

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
        public static List<Room> listofRooms = new List<Room>();
        public static List<RoomAllocation> roomAllocations = new List<RoomAllocation>();
        public static string mainFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");

        // Main function
        static void Main(string[] args)
        {
           

            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("**********************************************************************************");
                Console.WriteLine(" LANGHAM HOTEL MANAGEMENT SYSTEM");
                Console.WriteLine(" MENU ");
                Console.WriteLine("**********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("0. Backup Room Allocation File");
                Console.WriteLine("**********************************************************************************");
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
                        AllocateRoom();
                        break;

                    case 4:
                        DeallocateRoom();
                        break;

                    case 5:
                        DisplayRoomAllocations();
                        break;

                    case 6:
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
                        break;


                    case 7:
                        SaveRoomAllocationsToFile();
                        break;

                    case 8:
                        ShowRoomAllocationsFromFile();
                        break;

                    case 9:
                        Console.WriteLine("Thank you for using the LANGHAM Hotel Management System. Goodbye!");
                        Environment.Exit(0);
                        break;
                    case 0:
                        BackupRoomAllocations();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
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

                listofRooms = new List<Room>();

                for (int i = 0; i < totalRooms; i++)
                {
                    Console.Write("Please enter the Room Number: ");
                    int roomNo = Convert.ToInt32(Console.ReadLine());

                    listofRooms.Add(new Room { RoomNo = roomNo, IsAllocated = false });
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
                if (listofRooms == null || listofRooms.Count == 0)
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
        public static void AllocateRoom()
        {
            try
            {
                // Corrected the condition to use Count instead of Length
                if (listofRooms == null || listofRooms.Count == 0)
                {
                    Console.WriteLine("No rooms available to allocate. Please add rooms first.");
                    return;
                }

                Console.Write("Enter Room Number to Allocate: ");
                int roomNo = Convert.ToInt32(Console.ReadLine());

                Room selectedRoom = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);

                if (selectedRoom == null)
                {
                    throw new InvalidOperationException("Room number does not exist.");
                }

                if (selectedRoom.IsAllocated)
                {
                    Console.WriteLine("Room is already allocated.");
                    return;
                }

                Console.Write("Enter Customer Number: ");
                int custNo = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Customer Name: ");
                string custName = Console.ReadLine();

                Customer newCustomer = new Customer
                {
                    CustomerNo = custNo,
                    CustomerName = custName
                };

                RoomAllocation allocation = new RoomAllocation
                {
                    AllocatedRoomNo = roomNo,
                    AllocatedCustomer = newCustomer
                };

                selectedRoom.IsAllocated = true;
                roomAllocations.Add(allocation);

                Console.WriteLine($"Room {roomNo} allocated successfully to {custName}.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter numeric values where required.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public static void DeallocateRoom()
        {
            try
            {
                if (roomAllocations.Count == 0)
                {
                    Console.WriteLine("No rooms have been allocated yet.");
                    return;
                }

                Console.Write("Enter Room Number to De-Allocate: ");
                int roomNo = Convert.ToInt32(Console.ReadLine());

                // Check if the room is actually allocated
                RoomAllocation allocation = roomAllocations.FirstOrDefault(ra => ra.AllocatedRoomNo == roomNo);

                if (allocation == null)
                {
                    throw new InvalidOperationException("The room is not allocated or does not exist.");
                }

                Room roomToDeallocate = listofRooms.FirstOrDefault(r => r.RoomNo == roomNo);

                if (roomToDeallocate != null)
                {
                    roomToDeallocate.IsAllocated = false;
                }

                roomAllocations.Remove(allocation);

                Console.WriteLine($"Room {roomNo} has been successfully de-allocated.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid room number.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        public static void DisplayRoomAllocations()
        {
            try
            {
                if (roomAllocations.Count == 0)
                {
                    Console.WriteLine("No room allocations to display.");
                    return;
                }

                Console.WriteLine("\n--- Room Allocation Details ---");
                foreach (var allocation in roomAllocations)
                {
                    Console.WriteLine($"Room No: {allocation.AllocatedRoomNo}, " +
                                      $"Customer No: {allocation.AllocatedCustomer.CustomerNo}, " +
                                      $"Customer Name: {allocation.AllocatedCustomer.CustomerName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying room allocations: {ex.Message}");
            }
        }
        public static void SaveRoomAllocationsToFile()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(mainFilePath, true)) // true to append
                {
                    sw.WriteLine("========== Room Allocations ==========");
                    sw.WriteLine("Timestamp: " + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));

                    bool dataWritten = false;

                    foreach (Room room in listofRooms)
                    {
                        if (room.IsAllocated)
                        {
                            sw.WriteLine($"Room No: {room.RoomNo} => Allocated");
                            dataWritten = true;
                        }
                    }

                    if (!dataWritten)
                    {
                        sw.WriteLine("No room allocations found.");
                    }

                    sw.WriteLine("======================================\n");
                    Console.WriteLine("Room allocation details saved to file successfully.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("ERROR: You don't have permission to write to the file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("File I/O Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Save to file operation completed.");
            }
        }
        public static void ShowRoomAllocationsFromFile()
        {
            try
            {
                if (!File.Exists(mainFilePath))
                {
                    throw new FileNotFoundException("The file does not exist.");
                }

                string[] lines = File.ReadAllLines(mainFilePath);

                Console.WriteLine("\n========== Contents of Room Allocations File ==========");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine("========================================================");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("ERROR: You don’t have permission to read the file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("ERROR reading the file: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("File read operation completed.");
            }
        }

        public static void BackupRoomAllocations()
        {
            string mainFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581.txt");
            string backupFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lhms_850002581_backup.txt");

            try
            {
                if (File.Exists(mainFile))
                {
                    // Read content from main file
                    string content = File.ReadAllText(mainFile);

                    // Append content to backup file with timestamp
                    using (StreamWriter sw = new StreamWriter(backupFile, true))
                    {
                        sw.WriteLine($"Backup on {DateTime.Now}");
                        sw.WriteLine(content);
                        sw.WriteLine("---------------------------------------------");
                    }

                    // Clear the content of the main file
                    File.WriteAllText(mainFile, string.Empty);

                    Console.WriteLine("Backup completed successfully. Main file has been cleared.");
                }
                else
                {
                    Console.WriteLine("Main file not found. Nothing to backup.");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to write to the backup file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred during backup: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Backup process completed.\nPress any key to continue...");
                Console.ReadKey();
            }
        }


    }
}
// End of Program

