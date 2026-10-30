using System;
using System.Collections.Generic;
using System.Text;

namespace KassasystemProject
{
    public interface IReceiptService
    {
        void SaveAndPrintReceipt(List<CartItem> cart);
    }
}
