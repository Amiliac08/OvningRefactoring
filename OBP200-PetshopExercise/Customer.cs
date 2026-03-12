using System;
using System.Collections.Generic;
namespace OBP200_PetshopExercise;

public class Customer : IBuyAnimal
{
    public static string[] customers = new string[4];
    
    public static void StartNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(name)) name = "Anonymous";

        Console.Write("Enter budget: ");
        var budgetStr = (Console.ReadLine() ?? "").Trim();
        int budget = Program.ParseInt(budgetStr, 100);

        customers[0] = name;
        customers[1] = budget.ToString();
        customers[2] = ""; // no pets yet
        customers[3] = "0"; // nothing spent yet

        Program.PurchaseHistory.Clear();

        Console.WriteLine($"Welcome, {name}! Your budget is {budget} coins.");
        Program.ShowStatus();
    }
    public void BuyAnimal(int index)
    {
        var animal = Animals.animals[index];
        int price = animal.costs;
        

        int budget = Program.ParseInt(customers[1], 0);
    

        if (budget < price)
        {
            Console.WriteLine($"Insufficient funds. You need {price - budget} more coins.");
            return;
        }
        // Purchase the animal
        
        budget -= price;
        customers[1] = budget.ToString();

        int spent = Program.ParseInt(customers[3], 0) + price;
        customers[3] = spent.ToString();
        

        // Add to pet list
        var pets = (customers[2] ?? "").Trim();
        customers[2] = string.IsNullOrEmpty(pets) ? animal.type : (pets + ";" + animal.type);

        // Record purchase
        Program.PurchaseHistory.Add(new[] { DateTime.Now.ToString("HH:mm"), animal.type, price.ToString() });

        Console.WriteLine($"You bought a {animal.type} for {price} coins!");
        Program.ShowStatus();
    }
}