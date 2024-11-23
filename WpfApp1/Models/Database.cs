using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Models
{
    internal class Database
    {

        private const string ErrorMessageFormat = "Опция {0} не поддерживается для сортировки данной таблицы!\nДанные не будут отсортированы";
        private const string ErrorCaption = "Ошибка сортировки";

        private static RealtorAgentEntities context;

        static Database()
        {

            context = new RealtorAgentEntities();

        }

        public static void Save() => context.SaveChanges();

        public static void DeleteRealtor(int id)
        {

            var item = context.Realtor.First(x => x.Id == id);
            context.Realtor.Remove(item);
            context.SaveChanges();

        }
        public static void DeleteCustomer(int id)
        {

            var item = context.Customer.First(x => x.Id == id);
            context.Customer.Remove(item);
            context.SaveChanges();

        }
        public static void DeleteSeller(int id)
        {

            var item = context.Seller.First(x => x.Id == id);
            context.Seller.Remove(item);
            context.SaveChanges();

        }
        public static void DeleteProperty(int id)
        {

            var item = context.Property.First(x => x.Id == id);
            context.Property.Remove(item);
            context.SaveChanges();

        }



        private static IQueryable<Realtor> GetRealtors() => GetRealtors(SorterOption.None, "", false);

        public static IQueryable<Realtor> GetRealtors(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Realtor.Take(context.Realtor.Count());

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.Id.ToString().Contains(sorterString));

                    break;

                case SorterOption.FirstName:

                    res = res.Where(o => o.FirstName.Contains(sorterString));

                    break;

                case SorterOption.LastName:

                    res = res.Where(o => o.LastName.Contains(sorterString));

                    break;

                case SorterOption.MiddleName:

                    res = res.Where(o => o.MiddleName.Contains(sorterString));

                    break;

                case SorterOption.Phone:

                    res = res.Where(o => o.Phone.Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if(enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

        private static IQueryable<Seller> GetSellers() => GetSellers(SorterOption.None, "", false);
        public static IQueryable<Seller> GetSellers(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Seller.Take(context.Seller.Count());

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.Id.ToString().Contains(sorterString));

                    break;

                case SorterOption.FirstName:

                    res = res.Where(o => o.FirstName.Contains(sorterString));

                    break;

                case SorterOption.LastName:

                    res = res.Where(o => o.LastName.Contains(sorterString));

                    break;

                case SorterOption.MiddleName:

                    res = res.Where(o => o.MiddleName.Contains(sorterString));

                    break;

                case SorterOption.Phone:

                    res = res.Where(o => o.Phone.Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if(enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

        private static IQueryable<CustomerWithPassport> GetCustomers() => GetCustomers(SorterOption.None, "", false);
        public static IQueryable<CustomerWithPassport> GetCustomers(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Customer.Join(context.Passport, o => o.PassportId, o => o.Id, (customer, passport) => new CustomerWithPassport
            { 
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                Phone = customer.Phone,

                PassportNumber = passport.Number,
                PassportSeries = passport.Series
            });

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.Id.ToString().Contains(sorterString));

                    break;

                case SorterOption.FirstName:

                    res = res.Where(o => o.FirstName.Contains(sorterString));

                    break;

                case SorterOption.LastName:

                    res = res.Where(o => o.LastName.Contains(sorterString));

                    break;

                case SorterOption.MiddleName:

                    res = res.Where(o => o.MiddleName.Contains(sorterString));

                    break;

                case SorterOption.Phone:

                    res = res.Where(o => o.Phone.Contains(sorterString));

                    break;

                case SorterOption.PassportNumber:

                    res = res.Where(o => o.PassportNumber.Contains(sorterString));

                    break;

                case SorterOption.PassportSeries:

                    res = res.Where(o => o.PassportSeries.Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if(enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

        private static IQueryable<PropertyWithSeller> GetPropertiesWithSellers() => GetPropertiesWithSellers(SorterOption.None, "", false);
        public static IQueryable<PropertyWithSeller> GetPropertiesWithSellers(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Property.Join(context.Seller, o => o.SellerId, o => o.Id, (property, seller) => new PropertyWithSeller
            {
                PropertyAddress = property.Address,
                PropertyCost = property.Cost,
                PropertyDescription = property.Description,
                PropertyHasBalcony = property.HasBalcony,
                PropertyId = property.Id,
                PropertyRoomNumber = property.RoomNumber,
                PropertySquare = property.Square,

                SellerId = seller.Id,
                SellerFirstName = seller.FirstName,
                SellerLastName = seller.LastName,
                SellerMiddleName = seller.MiddleName,
                SellerPhone = seller.Phone
            });

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.PropertyId.ToString().Contains(sorterString));

                    break;

                case SorterOption.FirstName:

                    res = res.Where(o => o.SellerFirstName.Contains(sorterString));

                    break;

                case SorterOption.LastName:

                    res = res.Where(o => o.SellerLastName.Contains(sorterString));

                    break;

                case SorterOption.MiddleName:

                    res = res.Where(o => o.SellerMiddleName.Contains(sorterString));

                    break;

                case SorterOption.Phone:

                    res = res.Where(o => o.SellerPhone.Contains(sorterString));

                    break;

                case SorterOption.RoomNumber:

                    res = res.Where(o => o.PropertyRoomNumber.ToString().Contains(sorterString));

                    break;

                case SorterOption.Square:

                    res = res.Where(o => o.PropertySquare.ToString().Contains(sorterString));

                    break;

                case SorterOption.Address:

                    res = res.Where(o => o.PropertyAddress.Contains(sorterString));

                    break;

                case SorterOption.Cost:

                    res = res.Where(o => o.PropertyCost.ToString().Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if(enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

        private static IQueryable<Property> GetProperties() => GetProperties(SorterOption.None, "", false);
        public static IQueryable<Property> GetProperties(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Property.Take(context.Property.Count());

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.Id.ToString().Contains(sorterString));

                    break;

                case SorterOption.RoomNumber:

                    res = res.Where(o => o.RoomNumber.ToString().Contains(sorterString));

                    break;

                case SorterOption.Square:

                    res = res.Where(o => o.Square.ToString().Contains(sorterString));

                    break;

                case SorterOption.Address:

                    res = res.Where(o => o.Address.ToString().Contains(sorterString));

                    break;

                case SorterOption.Cost:

                    res = res.Where(o => o.Cost.ToString().Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if(enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

        public static IQueryable<FullDeal> GetDeals(SorterOption option = SorterOption.None, string sorterString = "", bool enableErrorMessage = true)
        {

            var res = context.Deal.Join(GetCustomers(), o => o.CustomerId, o => o.Id, (deal, customer) => new
            {
                deal = deal,
                customer = customer
            }).Join(GetRealtors(), o => o.deal.RealtorId, o => o.Id, (dc, realtor) => new
            {

                deal = dc.deal,
                customer = dc.customer,
                realtor = realtor

            }).Join(GetPropertiesWithSellers(), o => o.deal.PropertyId, o => o.PropertyId, (dcr, ps) => new FullDeal
            {

                Id = dcr.deal.Id,
                CustomerId = dcr.customer.Id,
                PropertyId = ps.PropertyId,
                RealtorId = dcr.realtor.Id,
                SellerId = ps.SellerId,

                RegistrationCode = dcr.deal.RegistrationCode,
                Date = dcr.deal.Date,

                CustomerFirstName = dcr.customer.FirstName,
                CustomerLastName = dcr.customer.LastName,
                CustomerMiddleName = dcr.customer.MiddleName,
                CustomerPhone = dcr.customer.Phone,
                PassportNumber = dcr.customer.PassportNumber,
                PassportSeries = dcr.customer.PassportSeries,

                SellerFirstName = ps.SellerFirstName,
                SellerLastName = ps.SellerLastName,
                SellerMiddleName = ps.SellerMiddleName,
                SellerPhone = ps.SellerPhone,

                RealtorFirstName = dcr.realtor.FirstName,
                RealtorLastName = dcr.realtor.LastName,
                RealtorMiddleName = dcr.realtor.MiddleName,
                RealtorPhone = dcr.realtor.Phone,

                PropertyAddress = ps.PropertyAddress,
                PropertyCost = ps.PropertyCost,
                PropertyDescription = ps.PropertyDescription,
                PropertyHasBalcony = ps.PropertyHasBalcony,
                PropertyRoomNumber = ps.PropertyRoomNumber,
                PropertySquare = ps.PropertySquare


            });

            switch (option)
            {

                case SorterOption.Id:

                    res = res.Where(o => o.Id.ToString().Contains(sorterString));

                    break;

                case SorterOption.PassportNumber:

                    res = res.Where(o => o.PassportNumber.ToString().Contains(sorterString));

                    break;

                case SorterOption.PassportSeries:

                    res = res.Where(o => o.PassportSeries.ToString().Contains(sorterString));

                    break;

                case SorterOption.RoomNumber:

                    res = res.Where(o => o.PropertyRoomNumber.ToString().Contains(sorterString));

                    break;

                case SorterOption.Address:

                    res = res.Where(o => o.PropertyAddress.ToString().Contains(sorterString));

                    break;

                case SorterOption.Cost:

                    res = res.Where(o => o.PropertyCost.ToString().Contains(sorterString));

                    break;

                case SorterOption.Date:

                    res = res.Where(o => o.Date.ToString().Contains(sorterString));

                    break;

                case SorterOption.RegistrationCode:

                    res = res.Where(o => o.RegistrationCode.ToString().Contains(sorterString));

                    break;

                case SorterOption.Square:

                    res = res.Where(o => o.PropertySquare.ToString().Contains(sorterString));

                    break;

                case SorterOption.None: break;

                default:

                    var errorText = string.Format(ErrorMessageFormat, option);
                    if (enableErrorMessage) MessageBox.Show(errorText, ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

            }

            return res;

        }

    }
}
