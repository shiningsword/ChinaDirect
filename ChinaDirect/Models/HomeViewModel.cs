using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChinaDirect.Models
{
    public class HomeViewModel
    {
        public double ReceiveUSD { get; set; }
        public double ReceiveCNY { get; set; }
        public double SendAmount = 1000;
        public double SaveCNY { get; set; }
        public double SaveUSD { get; set; }
        public HomeViewModel(double exchangeRate)
        {
            ReceiveCNY = SendAmount * exchangeRate;
            ReceiveUSD = SendAmount / exchangeRate;
            SaveCNY = .01 * ReceiveCNY;
            SaveUSD = .01 * ReceiveUSD;
        }
    }
}