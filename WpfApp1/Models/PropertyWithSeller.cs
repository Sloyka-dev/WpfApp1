using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class PropertyWithSeller
    {

        public int SellerId { get; set; }
        public string SellerLastName { get; set; }
        public string SellerFirstName { get; set; }
        public string SellerMiddleName { get; set; }
        public string SellerPhone { get; set; }

        public int PropertyId { get; set; }
        public int PropertyRoomNumber { get; set; }
        public int PropertySquare { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyDescription { get; set; }
        public bool PropertyHasBalcony { get; set; }
        public decimal PropertyCost { get; set; }

    }
}
