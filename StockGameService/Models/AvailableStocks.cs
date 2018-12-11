using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone;

namespace StockGameService.Models
{
    public class AvailableStocks
    {
        public List<Stock> _stocks = new List<Stock>();
        public AvailableStocks(List<Stock> stocks)
        {
            _stocks = stocks;
        }
        
        /// <summary>
        /// Sets the List<Stock> AvailStocks to the stocks the user is holding
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stocks"></param>
        public AvailableStocks(List<Stock> stocks, bool isUser)
        {
            _stocks = stocks;
        }
        public List<Stock> AvailStocks {
            get
            {
                return _stocks;
            }
        }


    }
}
