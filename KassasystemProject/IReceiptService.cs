using System;
using System.Collections.Generic;
using System.Text;

namespace KassasystemProject
{
    public internal interface IReceiptService
    {
        void SaveAndPrintReceipt(List<CartItem> cart);
    }
}
