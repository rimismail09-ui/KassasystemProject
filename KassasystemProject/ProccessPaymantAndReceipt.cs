using System.Globalization;

namespace KassasystemProject
{
    internal static class ProccessPaymantAndReceipt
    {
        public class ReceiptManager : IReceiptService
        {
            public void SaveAndPrintReceipt(List<CartItem> cart)
            { 
            if (cart.Count == 0)
            {
                Console.WriteLine("Inga produkter lades till i denna försöljning.");
                return;
            }

            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string fileNameDate = DateTime.Now.ToString("yyyyMMdd");
            string receiptFileName = $"RECEIPT_{fileNameDate}.txt";

            System.Text.StringBuilder receiptBuilder = new System.Text.StringBuilder();
            receiptBuilder.AppendLine("=============================");
            receiptBuilder.AppendLine($"KVITTO - {dateTimeNow}");
            receiptBuilder.AppendLine("==============================");

            decimal total = 0;
            foreach (var item in cart)
            {
                string line = $"{item.Product.Name} | {item.Quantity} {item.Product.Type} | {item.Product.Price:F2}kr/st | Radpris: {item.TotalPrice:F2}kr";
                receiptBuilder.AppendLine(line);
                total += item.TotalPrice;
            }

            receiptBuilder.AppendLine("------------------------------");
            receiptBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "Att betala totalt:{0:F2} kr", total));
            receiptBuilder.AppendLine("------------------------------");

            string finalReceiptText = receiptBuilder.ToString();

            Console.WriteLine("\n" + finalReceiptText);

            File.AppendAllText(receiptFileName, finalReceiptText);
            Console.WriteLine($"Kvittot har sparats till filen:{receiptFileName}");
        }
    }
}