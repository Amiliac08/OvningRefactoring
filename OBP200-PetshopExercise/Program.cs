using System;
using System.Collections.Generic;
namespace OBP200_PetshopExercise;

class Program
{
    // ======= Global State =======

    // Customer data: all values as strings
    // index: 0 Name, 1 Budget, 2 Pets (semicolon-sep), 3 Total Spent
    static string[] Customer = new string[4];

    // Available animals: [type, name, price, sound]
    //static List<string[]> Animals = new List<string[]>();

    // Purchase history: [date, animal, price]
    static List<string[]> PurchaseHistory = new List<string[]>();

    // Random
    static Random Rng = new Random();

    // ======= Main =======

    static void Main(string[] args)
    {
        //InitAnimals();
        Animals.AddAnimals(new Animals("mammal", "Cat", 25, "Meow"));
        Animals.AddAnimals(new Animals("mammal", "Dog", 40, "Woof"));
        Animals.AddAnimals(new Animals("bird", "Parrot", 35, "Squawk"));
        Animals.AddAnimals(new Animals("reptile", "Turtle", 30, "Cowabunga!"));
        Animals.AddAnimals(new Animals("small", "Hamster", 15, "Squeak"));

        while (true)
        {
            ShowMainMenu();
            Console.Write("Choose: ");
            var choice = (Console.ReadLine() ?? "").Trim();

            if (choice == "1")
            {
                StartNewCustomer();
                RunShoppingLoop();
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

    // ======= Menu & Init =======

    static void ShowMainMenu()
    {
        Console.WriteLine("=== Pet Shop ===");
        Console.WriteLine("1. New Customer");
        Console.WriteLine("2. Exit");
    }

    static void StartNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(name)) name = "Anonymous";

        Console.Write("Enter budget: ");
        var budgetStr = (Console.ReadLine() ?? "").Trim();
        int budget = ParseInt(budgetStr, 100);

        Customer[0] = name;
        Customer[1] = budget.ToString();
        Customer[2] = ""; // no pets yet
        Customer[3] = "0"; // nothing spent yet

        PurchaseHistory.Clear();

        Console.WriteLine($"Welcome, {name}! Your budget is {budget} coins.");
        ShowStatus();
    }

    static void RunShoppingLoop()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Available Animals ===");
            DisplayAnimals();

            Console.WriteLine();
            Console.WriteLine("[1-5] Buy animal  [V] View pets  [S] Animal sounds  [Q] Quit");
            Console.Write("Choose: ");
            var choice = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (choice == "Q")
            {
                Console.WriteLine("Thank you for shopping!");
                break;
            }
            else if (choice == "V")
            {
                ViewCustomerPets();
            }
            else if (choice == "S")
            {
                ShowAnimalSounds();
            }
            else if (choice.Length == 1 && char.IsDigit(choice[0]))
            {
                int index = int.Parse(choice) - 1;
                if (index >= 0 && index < Animals.Count)
                {
                    BuyAnimal(index);
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }

    // ======= Shopping =======

    static void InitAnimals()
    {
        //Animals.Clear();
        //Animals.Add(new[] { "mammal", "Cat", "25", "Meow" });
        //Animals.Add(new[] { "mammal", "Dog", "40", "Woof" });
        //Animals.Add(new[] { "bird", "Parrot", "35", "Squawk" });
        //Animals.Add(new[] { "reptile", "Turtle", "30", "Cowabunga!" });
        //Animals.Add(new[] { "small", "Hamster", "15", "Squeak" });
    }

    static void DisplayAnimals()
    {
        for (int i = 0; i < Animals.Count; i++)
        {
            var animal = Animals.animals[i];
            Console.WriteLine($"({i + 1}) {animal.type} ({animal.species}) - {animal.costs} coins");
        }
    }

    static void BuyAnimal(int index)
    {
        var animal = Animals.animals[index];
        int price = animal.costs
        //int price = ParseInt(animal.type, 10);
        ;
        // 10);

    int budget = ParseInt(Customer[1], 0);

        if (budget < price)
        {
            Console.WriteLine($"Insufficient funds. You need {price - budget} more coins.");
            return;
        }

        // Purchase the animal
        budget -= price;
        Customer[1] = budget.ToString();

        int spent = ParseInt(Customer[3], 0) + price;
        Customer[3] = spent.ToString();

        // Add to pet list
        var pets = (Customer[2] ?? "").Trim();
        Customer[2] = string.IsNullOrEmpty(pets) ? animal.type : (pets + ";" + animal.type);

        // Record purchase
        PurchaseHistory.Add(new[] { DateTime.Now.ToString("HH:mm"), animal.type, price.ToString() });

        Console.WriteLine($"You bought a {animal.type} for {price} coins!");
        ShowStatus();
    }

    static void ViewCustomerPets()
    {
        var pets = (Customer[2] ?? "").Trim();
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

    static void ShowAnimalSounds()
    {
        var pets = (Customer[2] ?? "").Trim();
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

    static void ShowStatus()
    {
        Console.WriteLine($"[{Customer[0]}] Budget: {Customer[1]} coins | Total Spent: {Customer[3]} coins");
    }

    // ======= Helper Methods =======

    static int ParseInt(string s, int fallback)
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