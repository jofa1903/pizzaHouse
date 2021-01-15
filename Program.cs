using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace pizzaProjekt
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }
        private static bool MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  /$$$$$$  /$$ /$$$$$$$$ /$$$$$$$$  /$$$$$$ ");
            Console.WriteLine(" /$$__  $$| $$|____ /$$/|____ /$$/ |____  $$");
            Console.WriteLine("| $$  | $$| $$   /$$$$/    /$$$$/   /$$$$$$$");
            Console.WriteLine("| $$  | $$| $$  /$$__/    /$$__/   /$$__  $$");
            Console.WriteLine("| $$$$$$$/| $$ /$$$$$$$$ /$$$$$$$$|  $$$$$$$");
            Console.WriteLine("| $$____/ |__/|________/|________/ _______/");
            Console.WriteLine("| $$                                        ");
            Console.WriteLine("| $$                                        ");
            Console.WriteLine("| $$                                        ");
            Console.WriteLine("|__/                                        ");
            
            // print options to console
            
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("1) Pizzameny");
            Console.WriteLine("2) Ta bort pizza ur beställningen");
            Console.WriteLine("3) Betala");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("4) Avsluta");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine("Nuvarande beställning:");
            Console.ForegroundColor = ConsoleColor.Yellow;

            // path to json file
            string jsonPath = @"pizza.json";

            var jsonData = System.IO.File.ReadAllText(jsonPath);

            // deserialize json
            var pizzaList = JsonConvert.DeserializeObject<List<CreatePizza>>(jsonData)
            ?? new List<CreatePizza>();

            // checking to see if data exists in JSON-file
            if (File.Exists(jsonPath))
            {

                int indx = 1;
                foreach (var pizza in pizzaList)
                {
                    Console.WriteLine($"[{indx}] {pizza.Name} - {pizza.Price} kr");
                    indx++;
                }
            };
            Console.ResetColor();

            // user options
            switch (Console.ReadLine())
            {
                case "1":
                    // create pizza
                    var freshPizza = new CreatePizza();

                    freshPizza.CreateNewPizza(out string Name, out int Price);
                    pizzaList.Add(new CreatePizza()
                    {
                        Name = Name,
                        Price = Price

                    });

                    jsonData = JsonConvert.SerializeObject(pizzaList);
                    File.WriteAllText(jsonPath, jsonData);
                    // checking to see if data exists in JSON-file and printing new data to console
                    if (File.Exists(jsonPath))
                    {

                        int indx = 1;
                        foreach (var pizza in pizzaList)
                        {
                            Console.WriteLine($"[{indx}] {pizza.Name} - {pizza.Price}kr");
                            indx++;
                        }
                    };
                    return true;

                case "2":
                    // delete pizza
                    var removepizza = new DeletePizza();
                    removepizza.DeleteMyPizza();
                    return true;

                case "3":
                    var paypizza = new PayPizza();
                    paypizza.payMyPizza();
                    return true;

                case "4":
                    // if chosen exit app

                    // deletes old-JSON-file so basket is empty upon startup
                    File.Delete(@"pizza.json");
                    // creates a new empty JSON-file to prevent app from crashing upon startup
                    if (!File.Exists(jsonPath))
                    {
                        FileStream fs = File.Create(jsonPath);
                    }
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ciao!");
                    Console.WriteLine("");
                    Console.ResetColor();
                    return false;

                default:
                    return true;
            }

        }

    }
}





