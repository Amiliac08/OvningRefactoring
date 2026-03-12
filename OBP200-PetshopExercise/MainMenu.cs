using System;

namespace OBP200_PetshopExercise;

public class MainMenu
{
    // ======= Menu & Init =======

    public static void ShowMainMenu()
    {
        Console.WriteLine("=== Pet Shop ===");
        Console.WriteLine("1. New Customer");
        Console.WriteLine("2. Exit");
    }

    public static void RunShoppingLoop()
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
                Program.ViewCustomerPets();
            }
            else if (choice == "S")
            {
                Program.ShowAnimalSounds();
            }
            else if (choice.Length == 1 && char.IsDigit(choice[0]))
            {
                int index = int.Parse(choice) - 1;
                if (index >= 0 && index < Animals.Count)
                {
                    Customer customers = new Customer();
                    customers.BuyAnimal(index);
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
    // Show animals in menu
    static void DisplayAnimals()
    {
        for (int i = 0; i < Animals.Count; i++)
        {
            var animal = Animals.animals[i];
            Console.WriteLine($"({i + 1}) {animal.type} ({animal.species}) - {animal.costs} coins");
        }
    }
}