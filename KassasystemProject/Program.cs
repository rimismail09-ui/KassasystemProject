using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace KassasystemProject
{
    class Program
    {
        private static string filePath = "products.txt";
        private static List<Product> products = new List<Product>();
        private static List<CartItem> Cart = new List<CartItem>();

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
                        var product = products.FirstOrDefault(p => p - Id == productId);
                        if (product == null)
                        { Console.WriteLine(" Fel: Product-ID finns inte i systemet.");
                            break;
                        }

                        Console.Write("Ange antal:");
                        string qtyInput = Console.ReadLine();

                        if (!int.TryParse(qtyInput, out int quantity) || quantity <= 0)
                        { Console.WriteLine("Fel: Ogiltigt antal . Ange ett helal större än 0.");
                            break;
                        }

                        Cart.Add(new CartItem(product, quantity));
                        Console.WriteLine($"lade till {quantity}st {product.Name} i kundvagnen");
                        break;

                    case "3":
                        if (cart.Count == 0)
                        {
                            Console.WriteLine("Kundvagnen är tom!");
                            break
                        }
                        decimal grandTotal = Cart.Sum(item => item.TotalPrice);
                        receiptService.SaveReceipt(cart, grandTotal);
                        Cart.Clear();
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

                if (!File.Exists(filepath))
                {
                    Console.WriteLine("Varning: Produktfilen saknas. skapar en ny standarfil . . .");

                    File.WriteAllLines(filepath, new[]
                {
                        "1, Äpple, 15.50",
                        "2, Banan, 12.00",
                        "3, Mjölk, 20.00",
                });
                }


                products.Clear();
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 &&
                        int.TryParse(parts[0].Trim(), out int id) &&
                        decimal.TryParse(parts[2].Trim(), System.Globalization.CultureInfo.InvariantCulture, out decimal price))

                    {
                        products.Add(new Product { ID = id, Name = parts[1].Trim(), Price = price });

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
                Console.WriteLine($"ID: {p.Id} | {p.Name} - {p.Price:C}");
            }
        }
    }