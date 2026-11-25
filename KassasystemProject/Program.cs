using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace KassasystemProject
{
    class Program
    {
        private static string filePath = "products.txt";
        private static List<Product> products = new List<Product>();
        private static List<CartItem> cart = new List<CartItem>();

        static void Main(string[] args)
        {

            LoadProducts();
            IReceiptService receiptService = new ReceiptManager();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\n---KASSASYTEM---");
                Console.WriteLine("1. Visa produkter");
                Console.WriteLine("2. Lägg till produkt i kundvagn");
                Console.WriteLine("3. Betala ( PAY)");
                Console.WriteLine("4.Avsluta:  ");
                Console.WriteLine("Välj ett alternativ: ");

                string välj = Console.ReadLine();

                switch (välj)
                {
                    case "1":
                        ShowProducts();
                        break;

                    case "2":
                        Console.Write("Ange produKt-ID:");
                        string idInput = Console.ReadLine();
                        if (!int.TryParse(idInput, out int productId))
                        { Console.WriteLine("Fel: Ogiltig inmatning. Ange endast siffror föt ID.");
                            break;

                        }
                        var product = products.FirstOrDefault(p => p.Id == productId);
                        if (product == null)
                        { Console.WriteLine(" Fel: Product-ID finns inte i systemet.");
                            break;
                        }

                        Console.Write("Ange antal (t.ex. 1 eller 1.5):");
                        string qtyInput = Console.ReadLine();

                        if (!decimal.TryParse(qtyInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal quantity) || quantity <= 0)

                        { Console.WriteLine("Fel: Ogiltigt antal . Ange ett tal större än 0.");
                            break;
                        }

                        cart.Add(new CartItem(product, quantity));
                        Console.WriteLine($"lade till {quantity} {product.Type} av {product.Name} i kundvagnen.");
                        break;

                    case "3":
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("Kundvagnen är tom!");
                            break;
                        }

                        receiptService.SaveAndPrintReceipt(cart);
                        cart.Clear();
                        Console.WriteLine("köp slutfört! kvittot har sparats.");
                        break;

                    case "4":

                        running = false;
                        Console.WriteLine("Avslutar programmet...");
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;

                }
            }
        }



        private static void LoadProducts()
        {
            try
            {

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Varning: Produktfilen saknas. skapar en ny standarfil . . .");

                    File.WriteAllLines(filePath, new[]
                {
                        "1, Äpple, 15.50, kg",
                        "2, Banan, 12.00, kg",
                        "3, Mjölk, 20.00, kg",
                });
                }


                products.Clear();
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 3 &&
                        int.TryParse(parts[0].Trim(), out int id) &&
                        decimal.TryParse(parts[2].Trim(), System.Globalization.CultureInfo.InvariantCulture, out decimal price))

                    {
                        string type = parts.Length > 3 ? parts[3].Trim() : "st";
                        products.Add(new Product(id, parts[1].Trim(), price, type));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid Läsning av produktfil: {ex.Message}");
            }
        }

        private static void ShowProducts()
        {
            Console.WriteLine("\n---Tillgängliga Produkter ---");
            foreach (var p in products)
            {
                Console.WriteLine($"ID: {p.Id} | {p.Name} | Pris: {p.Price:C} | Typ: {p.Type}");
            }
        }
    }
}