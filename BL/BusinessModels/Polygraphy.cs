using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BL.BusinessModels
{
    public class Polygraphy
    {
        public Polygraphy(int Edition)
        {
            _value = Edition;
        }
        private int _value = 0;
        public decimal Value { get { return _value; } }
        public decimal GetSummarizedPrice(decimal price)
        {
            return _value * price;
        }
    }
}