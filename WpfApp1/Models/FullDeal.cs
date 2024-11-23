using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class FullDeal
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string RegistrationCode { get; set; }

        public int CustomerId { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerMiddleName { get; set; }
        public string CustomerPhone { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }

        public int RealtorId { get; set; }
        public string RealtorLastName { get; set; }
        public string RealtorFirstName { get; set; }
        public string RealtorMiddleName { get; set; }
        public string RealtorPhone { get; set; }

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
