using System;
using System.Collections.Generic;
namespace OBP200_PetshopExercise;

class Program
{
    
    // Purchase history: [date, animal, price]
    public static List<string[]> PurchaseHistory = new List<string[]>();
    

    // ======= Main =======

    static void Main(string[] args)
    {
    List<Animals> animals = new List<Animals>();
    
        Animals.animals.Add(new Animals("mammal", "Cat", 25, "Meow"));
        Animals.animals.Add(new Animals("mammal", "Dog", 40, "Woof"));
        Animals.animals.Add(new Animals("bird", "Parrot", 35, "Squawk"));
        Animals.animals.Add(new Animals("reptile", "Turtle", 30, "Cowabunga!"));
        Animals.animals.Add(new Animals("small", "Hamster", 15, "Squeak"));

        // === Menu Choice ===
        while (true)
        {
            MainMenu.ShowMainMenu();
            Console.Write("Choose: ");
            var choice = (Console.ReadLine() ?? "").Trim();

            if (choice == "1")
            {
                Customer.StartNewCustomer();
                MainMenu.RunShoppingLoop();
            }
            else if (choice == "2")
            {
                Console.WriteLine("Goodbye!");
                return;
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine();
        }
    }
    
    public static void ViewCustomerPets()
    {
        var pets = (Customer.customers[2] ?? "").Trim();
        if (string.IsNullOrWhiteSpace(pets))
        {
            Console.WriteLine("You haven't bought any pets yet.");
        }
        else
        {
            Console.WriteLine("Your pets:");
            var petList = pets.Split(';');
            for (int i = 0; i < petList.Length; i++)
            {
                Console.WriteLine($"  {i + 1}. {petList[i]}");
            }
        }
    }

    public static void ShowAnimalSounds()
    {
        var pets = (Customer.customers[2] ?? "").Trim();
        if (string.IsNullOrWhiteSpace(pets))
        {
            Console.WriteLine("You haven't bought any pets yet.");
            return;
        }

        Console.WriteLine("Your pets' sounds:");
        var petList = pets.Split(';');
        foreach (var petName in petList)
        {
            // Find the animal in the Animals list to get its sound
            foreach (var animal in Animals.animals)
            {
                if (animal.type == petName)
                {
                    Console.WriteLine($"  {petName}: {animal.sound}");
                    break;
                }
            }
        }
    }

    // ======= Status =======

    public static void ShowStatus()
    {
        Console.WriteLine($"[{Customer.customers[0]}] Budget: {Customer.customers[1]} coins | Total Spent: {Customer.customers[3]} coins");
    }

    // ======= Helper Methods =======

    public static int ParseInt(string s, int fallback)
    {
        try
        {
            int value = Convert.ToInt32(s);
            return value;
        }
        catch (Exception)
        {
            return fallback;
        }
    }
}