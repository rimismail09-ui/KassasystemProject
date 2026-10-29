using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Net.NetworkInformation;

namespace KassasystemProject
{
    class Program
    {
        static Dictionary<int, Product> products = new Dictionary<int, Product>();
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            LoadProducts("products.txt");

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nKASSASYTEM");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("2. Admin");
                Console.WriteLine("0. Avsluta");
                Console.WriteLine("val:  ");
                string välj = Console.ReadLine();

                switch (välj)
                {
                    case "1":
                        StartNewSale();
                        break;
                    case "2":
                        Console.WriteLine("Admin läget är inte implementerat för G-nivå.");
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Avslutar programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;

                }
            }
        }

        static void LoadProducts(string filepath)
        {
            if (!File.Exists(filepath))
            {
                File.WriteAllLines(filepath, new string[]
                {
                        "300;Bananer;29.90;kg",
                        "301;Mjölk;15.90;styck",
                        "302;kaffe;59.90;styck"
                });
            }

            foreach (var line in File.ReadAllLines(filepath))
            {
                var parts = line.Split(';');
                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[0], out int id) && decimal.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                    {
                        products[id] = new Product(id, parts[1], price, parts[3]);
                    }
                }
            }
        }

        static void StartNewSale()

        {
            List<CartItem> cart = new List<CartItem>();
            Console.WriteLine("\n--- Ny kund(Skriv 'Pay' eller'klar'när du är färdig) ---");

            while (true)
            {
                Console.Write("Ange produkt-ID och antal (t.ex. 300 2 eller 300 1.5):");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                if (input.Equals("Pay", StringComparison.OrdinalIgnoreCase) || input.Equals("klar", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }


                var parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2)
                {
                    Console.WriteLine("Felaktigt format. Använd formatet: [ID] [Antal], t.ex '300 2'");
                    continue;
                }
                if (!int.TryParse(parts[0], out int id) || !decimal.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal quantity))
                {
                    Console.WriteLine("Ogiltiga värden. kontrollera ID och antal.");
                    continue;

                }

                if (!products.ContainsKey(id))
                {
                    Console.WriteLine($"Producten med ID {id}finns inte i systemet.");
                    continue;
                }
                var prod = products[id];

                cart.Add(new CartItem(prod, quantity));
                Console.WriteLine($"Lagt till: {quantity} {prod.Type} av {prod.Name} (Totalt: {prod.Price * quantity:F2}kr)");
            }

            IReceiptService receiptService = new ReceiptManager();
            receiptService.saveAndPrintReceipt(cart);

        }
    }
}







      

       