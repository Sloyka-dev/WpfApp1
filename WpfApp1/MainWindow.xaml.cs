using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Models;

namespace WpfApp1
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const int CELL_W_SIZE = 150;
        private const int CELL_H_SIZE = 30;

        private const string DELETE_TEXT = "Вы точно хотите удалить запись?";
        private const string DELETE_CAPTION = "Удаление записи";
        private const string ERROR_TEXT = "Ошибка ввода {0}!\nПовторите снова.";
        private const string ERROR_CAPTION = "Ошибка ввода данных";

        public MainWindow()
        {
            InitializeComponent();

            ChooseTable.SelectionChanged += ChooseTable_SelectionChanged;
            SearchList.SelectionChanged += SearchList_SelectionChanged;
            SearchText.TextChanged += SearchText_TextChanged;
            CreateBtn.Click += CreateBtn_Click;

        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

            var tableTag = ((ComboBoxItem)ChooseTable.SelectedValue).Tag;

            switch (tableTag.ToString())
            {

                case "Realtors":

                    CreateRealtors();

                    break;

                case "Sellers":

                    CreateSellers();

                    break;

                case "Customers":

                    CreateCustomers();

                    break;

                case "Properties":

                    CreateProperties();

                    break;

                case "Deals":

                    CreateDeals();

                    break;

                default:

                    MessageBox.Show($"Невозможно создать обьект типа {tableTag}");

                    break;

            }

        }

        private void CreateRealtors()
        {

            ClearGrid(Data);
            AddRealtorHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 7);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 5, 0, "Создать");
            CreateLabel(Header, 6, 0, "Удалить");

            AddRowDefinitions(Data, CELL_H_SIZE);

            var FirstName = CreateTextBox(Data, 1, 1);
            var MiddleName = CreateTextBox(Data, 2, 1);
            var LastName = CreateTextBox(Data, 3, 1);
            var Phone = CreateTextBox(Data, 4, 1);

            CreateButton(Data, () =>
            {

                if (FirstName.Text.Length == 0 || !Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MiddleName.Text.Length == 0 || !Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (LastName.Text.Length == 0 || !Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Phone.Text.Length == 0 || !Regex.IsMatch(Phone.Text, "^[0-9]{11}$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var item = new Realtor()
                {

                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text,
                    Phone = Phone.Text

                };

                Database.Add(item);
                Database.Save();

                GetRealtors();

            }, 5, 1, "Создать");
            CreateButton(Data, () =>
            {

                GetRealtors();

            }, 5, 1, "Удалить");

        }

        private void CreateSellers()
        {

            ClearGrid(Data);
            AddSellerHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 8);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 6, 0, "Создать");
            CreateLabel(Header, 7, 0, "Удалить");

            AddRowDefinitions(Data, CELL_H_SIZE);

            var FirstName = CreateTextBox(Data, 1, 1);
            var MiddleName = CreateTextBox(Data, 2, 1);
            var LastName = CreateTextBox(Data, 3, 1);
            var Phone = CreateTextBox(Data, 4, 1);

            CreateButton(Data, () =>
            {

                if (FirstName.Text.Length == 0 || !Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MiddleName.Text.Length == 0 || !Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (LastName.Text.Length == 0 || !Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Phone.Text.Length == 0 || !Regex.IsMatch(Phone.Text, "^[0-9]{11}$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var item = new Seller()
                {

                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text,
                    Phone = Phone.Text

                };

                Database.Add(item);
                Database.Save();

                GetSellers();

            }, 6, 1, "Создать");
            CreateButton(Data, () =>
            {

                GetSellers();

            }, 7, 1, "Удалить");

        }

        private void CreateCustomers()
        {

            ClearGrid(Data);
            AddCustomerHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 9);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 7, 0, "Создать");
            CreateLabel(Header, 8, 0, "Удалить");

            AddRowDefinitions(Data, CELL_H_SIZE);

            var FirstName = CreateTextBox(Data, 1, 1);
            var MiddleName = CreateTextBox(Data, 2, 1);
            var LastName = CreateTextBox(Data, 3, 1);
            var Phone = CreateTextBox(Data, 4, 1);
            var PasportSeries = CreateTextBox(Data, 5, 1);
            var PasportNumber = CreateTextBox(Data, 6, 1);

            CreateButton(Data, () =>
            {

                if (FirstName.Text.Length == 0 || !Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MiddleName.Text.Length == 0 || !Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (LastName.Text.Length == 0 || !Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Phone.Text.Length == 0 || !Regex.IsMatch(Phone.Text, "^[0-9]{11}$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var passport = new Passport()
                {

                    Series = PasportSeries.Text,
                    Number = PasportNumber.Text

                };

                Database.Add(passport);
                Database.Save();

                var item = new Customer()
                {

                    FirstName = FirstName.Text,
                    MiddleName = MiddleName.Text,
                    LastName = LastName.Text,
                    Phone = Phone.Text,
                    PassportId = passport.Id

                };

                Database.Add(item);
                Database.Save();

                GetCustomers();

            }, 7, 1, "Создать");
            CreateButton(Data, () =>
            {

                GetCustomers();

            }, 8, 1, "Удалить");

        }

        private void CreateProperties()
        {

            ClearGrid(Data);
            AddPropertyHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 9);
            AddColumnDefinitions(Header, CELL_W_SIZE, 3);

            CreateLabel(Header, 6, 0, "Продавец");
            CreateLabel(Header, 7, 0, "Изменить");
            CreateLabel(Header, 8, 0, "Удалить");

            AddRowDefinitions(Data, CELL_H_SIZE);

            var RoomNumber = CreateTextBox(Data, 1, 1);
            var Square = CreateTextBox(Data, 2, 1);
            var HasBalcony = CreateCheckBox(Data, 3);
            var Cost = CreateTextBox(Data, 4, 1);
            var Address = CreateTextBox(Data, 5, 1);

            var sellerComboBox = CreateComboBox(Data, Database.GetSellers(), 6, 1);

            CreateButton(Data, () =>
                {

                    if (RoomNumber.Text.Length == 0 || !Regex.IsMatch(RoomNumber.Text, "^[0-9]*$"))
                    {
                        MessageBox.Show(string.Format(ERROR_TEXT, "количество комнат"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (Square.Text.Length == 0 || !Regex.IsMatch(Square.Text, "^[0-9]*$"))
                    {
                        MessageBox.Show(string.Format(ERROR_TEXT, "площадь"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (Cost.Text.Length == 0 || !Regex.IsMatch(Cost.Text, "^[0-9]*$"))
                    {
                        MessageBox.Show(string.Format(ERROR_TEXT, "стоимость"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (Address.Text.Length == 0)
                    {
                        MessageBox.Show(string.Format(ERROR_TEXT, "адрес"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if(sellerComboBox.SelectedItem is null)
                    {
                        MessageBox.Show(string.Format(ERROR_TEXT, "продавец"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var item = new Property()
                    {

                        RoomNumber = int.Parse(RoomNumber.Text),
                        Square = int.Parse(Square.Text),
                        Cost = int.Parse(Cost.Text),
                        Address = Address.Text,
                        Description = "",
                        SellerId = int.Parse(sellerComboBox.SelectedItem.ToString().Split('.')[0])

                    };

                    Database.Add(item);
                    Database.Save();
                    GetProperties();

                }, 7, 1, "Создать");

            CreateButton(Data, () =>
            {

                GetProperties();

            }, 8, 1, "Удалить");


        }

        private void CreateDeals()
        {

            ClearGrid(Data);
            AddDealHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 8);
            AddColumnDefinitions(Header, CELL_W_SIZE, 5);

            CreateLabel(Header, 3, 0, "Квартира");
            CreateLabel(Header, 4, 0, "Риелтор");
            CreateLabel(Header, 5, 0, "Клиент");
            CreateLabel(Header, 6, 0, "Создать");
            CreateLabel(Header, 7, 0, "Удалить");

            AddRowDefinitions(Data, CELL_H_SIZE);

            var RegistrationCode = CreateTextBox(Data, 1, 0);
            var DateText = CreateTextBox(Data, 2, 0, DateTime.Now.ToString());
            var propertyComboBox = CreateComboBox(Data, Database.GetProperties(), 3, 0);
            var realtorComboBox = CreateComboBox(Data, Database.GetRealtors(), 4, 0);
            var customerComboBox = CreateComboBox(Data, Database.GetCustomers(), 5, 0);

            CreateButton(Data, () =>
            {

                if (RegistrationCode.Text.Length == 0 || !Regex.IsMatch(RegistrationCode.Text, "^R[0-9]{10}$"))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "регистрационный код"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (DateTime.TryParse(DateText.Text, out var _))
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "дата"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (propertyComboBox.SelectedItem is null)
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "квартира"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (realtorComboBox.SelectedItem is null)
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "риелтор"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (customerComboBox.SelectedItem is null)
                {
                    MessageBox.Show(string.Format(ERROR_TEXT, "клиент"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var item = new Deal()
                {

                    RegistrationCode = RegistrationCode.Text,
                    Date = DateTime.Parse(DateText.Text),
                    CustomerId = int.Parse(customerComboBox.SelectedItem.ToString().Split('.')[0]),
                    PropertyId = int.Parse(propertyComboBox.SelectedItem.ToString().Split('.')[0]),
                    RealtorId = int.Parse(realtorComboBox.SelectedItem.ToString().Split('.')[0])

                };

                Database.Add(item);
                Database.Save();
                GetProperties();

            }, 7, 1, "Создать");

            CreateButton(Data, () =>
            {

                GetProperties();

            }, 8, 1, "Удалить");

        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e) => SearchList_SelectionChanged(sender, null);

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SorterOption sortTag;
            try
            {
                if(!(SearchList.SelectedValue is null))
                    sortTag = (SorterOption)int.Parse(((ComboBoxItem)SearchList.SelectedValue).Tag.ToString());
                else
                    sortTag = SorterOption.None;

            }
            catch(Exception ex)
            {

                sortTag = SorterOption.None;

            }
            var tableTag = ((ComboBoxItem)ChooseTable.SelectedValue).Tag;
            var searchText = SearchText.Text;

            switch (tableTag.ToString())
            {

                case "Realtors":

                    GetRealtors(sortTag, searchText);

                    break;

                case "Sellers":

                    GetSellers(sortTag, searchText);

                    break;

                case "Customers":

                    GetCustomers(sortTag, searchText);

                    break;

                case "PropertiesWithSellers":

                    GetPropertiesWithSellers(sortTag, searchText);

                    break;

                case "Properties":

                    GetProperties(sortTag, searchText);

                    break;

                case "Deals":

                    GetDeals(sortTag, searchText);

                    break;

            }

        }
        
        private void ClearGrid(Grid grid)
        {

            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            grid.Children.Clear();

        }

        private void AddColumnDefinitions(Grid grid, int width = -1, int count = 1)
        {

            if (width != -1)
                for (int i = 0; i < count; i++)
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(width) });
            else
                for (int i = 0; i < count; i++)
                    grid.ColumnDefinitions.Add(new ColumnDefinition());

        }
        private void AddRowDefinitions(Grid grid, int height = -1, int count = 1)
        {

            if (height != -1)
                for (int i = 0; i < count; i++)
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(height) });
            else
                for (int i = 0; i < count; i++)
                    grid.RowDefinitions.Add(new RowDefinition());

        }

        private Label CreateLabel(Grid grid, int column = 0, int row = 0, string text = "")
        {

            var label = new Label();
            label.Content = text;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(label, column);
            Grid.SetRow(label, row);
            grid.Children.Add(label);

            return label;

        }

        private TextBox CreateTextBox(Grid grid, int column = 0, int row = 0, string text = "")
        {

            var textBox = new TextBox();
            textBox.Text = text;
            textBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            Grid.SetColumn(textBox, column);
            Grid.SetRow(textBox, row);
            grid.Children.Add(textBox);

            return textBox;

        }

        private Button CreateButton(Grid grid, Action func, int column = 0, int row = 0, string text = "")
        {

            var button = new Button();
            button.Content = text;
            button.HorizontalContentAlignment = HorizontalAlignment.Center;
            button.VerticalContentAlignment = VerticalAlignment.Center;
            button.Click += (o, e) => func.Invoke();
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            grid.Children.Add(button);

            return button;

        }

        private CheckBox CreateCheckBox(Grid grid, int column = 0, int row = 0, bool content = false)
        {

            var checkBox = new CheckBox();
            checkBox.IsChecked = content;
            checkBox.HorizontalAlignment = HorizontalAlignment.Center;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(checkBox, column);
            Grid.SetRow(checkBox, row);
            grid.Children.Add(checkBox);

            return checkBox;

        }
        private ComboBox CreateComboBox<T>(Grid grid, IEnumerable<T> content, int column = 0, int row = 0)
        {

            var comboBox = new ComboBox();

            if (content is IEnumerable<Seller>)
            {

                var selers = ((IEnumerable<Seller>)content);
                var items = selers.Select(o => $"{o.Id}. {o.FirstName} {o.MiddleName} {o.LastName}");

                foreach (var item in items)
                    comboBox.Items.Add(item);

            }
            else if (content is IEnumerable<CustomerWithPassport>)
            {

                var selers = ((IEnumerable<CustomerWithPassport>)content);
                var items = selers.Select(o => $"{o.Id}. {o.FirstName} {o.MiddleName} {o.LastName}");

                foreach (var item in items)
                    comboBox.Items.Add(item);

            }
            else if (content is IEnumerable<Realtor>)
            {

                var selers = ((IEnumerable<Realtor>)content);
                var items = selers.Select(o => $"{o.Id}. {o.FirstName} {o.MiddleName} {o.LastName}");

                foreach (var item in items)
                    comboBox.Items.Add(item);

            }
            else if (content is IEnumerable<Property>)
            {

                var selers = ((IEnumerable<Property>)content);
                var items = selers.Select(o => $"{o.Id}. {o.Address}; {o.RoomNumber} комнат {o.Square} м2");

                foreach (var item in items)
                    comboBox.Items.Add(item);

            }

            Grid.SetColumn(comboBox, column);
            Grid.SetRow(comboBox, row);
            grid.Children.Add(comboBox);

            return comboBox;

        }

        private void AddRealtorHeader()
        {

            ClearGrid(Header);
            AddColumnDefinitions(Header, CELL_W_SIZE, 5);
            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Телефон");

        }

        private void GetRealtors(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetRealtors(sortTag, tableTag).ToList();
            
            ClearGrid(Data);
            AddRealtorHeader();


            AddColumnDefinitions(Data, CELL_W_SIZE, 7);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 5, 0, "Изменить");
            CreateLabel(Header, 6, 0, "Удалить");

            int i = 0;
            foreach(var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var Id = CreateLabel(Data, 0, i, item.Id.ToString());
                var FirstName = CreateTextBox(Data, 1, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 2, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 3, i, item.LastName);
                var Phone = CreateTextBox(Data, 4, i, item.Phone);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetRealtors(sortTag, tableTag);

                }, 5, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    if (MessageBox.Show(DELETE_TEXT, DELETE_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.ServiceNotification) == MessageBoxResult.No) return;

                    Database.DeleteRealtor(item.Id);
                    GetRealtors(sortTag, tableTag);

                }, 6, i, "Удалить");

                i++;

            }

        }

        private void AddSellerHeader()
        {

            ClearGrid(Header);
            AddColumnDefinitions(Header, CELL_W_SIZE, 5);
            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Телефон");

        }

        private void GetSellers(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetSellers(sortTag, tableTag);

            ClearGrid(Data);
            AddSellerHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 7);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 5, 0, "Изменить");
            CreateLabel(Header, 6, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                CreateLabel(Data, 0, i, item.Id.ToString());
                var FirstName = CreateTextBox(Data, 1, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 2, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 3, i, item.LastName);
                var Phone = CreateTextBox(Data, 4, i, item.Phone);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetSellers(sortTag, tableTag);

                }, 5, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    if (MessageBox.Show(DELETE_TEXT, DELETE_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.ServiceNotification) == MessageBoxResult.No) return;

                    Database.DeleteSeller(item.Id);
                    GetSellers(sortTag, tableTag);

                }, 6, i, "Удалить");

                i++;

            }

        }

        private void AddCustomerHeader()
        {

            ClearGrid(Header);
            AddColumnDefinitions(Header, CELL_W_SIZE, 7);
            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Телефон");
            CreateLabel(Header, 5, 0, "Серия");
            CreateLabel(Header, 6, 0, "Номер");

        }

        private void GetCustomers(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetCustomers(sortTag, tableTag);

            ClearGrid(Data); 
            AddCustomerHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 9);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 6, 0, "Изменить");
            CreateLabel(Header, 7, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                CreateLabel(Data, 0, i, item.Id.ToString());
                var FirstName = CreateTextBox(Data, 1, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 2, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 3, i, item.LastName);
                var Phone = CreateTextBox(Data, 4, i, item.Phone);
                var PassportSeries = CreateTextBox(Data, 5, i, item.PassportSeries);
                var PassportNumber = CreateTextBox(Data, 6, i, item.PassportNumber);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (PassportSeries.Text.Length > 0 && Regex.IsMatch(PassportSeries.Text, "^[0-9]{4}$")) item.PassportSeries = PassportSeries.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Серия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (PassportNumber.Text.Length > 0 && Regex.IsMatch(PassportNumber.Text, "^[0-9]{6}$")) item.PassportNumber = PassportNumber.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Номер"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetCustomers(sortTag, tableTag);

                }, 7, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    if (MessageBox.Show(DELETE_TEXT, DELETE_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.ServiceNotification) == MessageBoxResult.No) return;

                    Database.DeleteCustomer(item.Id);
                    GetCustomers(sortTag, tableTag);

                }, 8, i, "Удалить");

                i++;

            }

        }

        private void GetPropertiesWithSellers(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetPropertiesWithSellers(sortTag, tableTag);

            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 10);
            AddColumnDefinitions(Header, CELL_W_SIZE, 10);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Имя");
            CreateLabel(Header, 1, 0, "Фамилия");
            CreateLabel(Header, 2, 0, "Отчество");
            CreateLabel(Header, 3, 0, "Телефон");
            CreateLabel(Header, 4, 0, "Кол-во комнат");
            CreateLabel(Header, 5, 0, "Площадь");
            CreateLabel(Header, 6, 0, "Наличие балкона");
            CreateLabel(Header, 7, 0, "Стоимость");
            CreateLabel(Header, 8, 0, "Адрес");
            CreateLabel(Header, 9, 0, "Изменить");
            CreateLabel(Header, 10, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var FirstName = CreateTextBox(Data, 0, i, item.SellerFirstName);
                var MiddleName = CreateTextBox(Data, 1, i, item.SellerMiddleName);
                var LastName = CreateTextBox(Data, 2, i, item.SellerLastName);
                var Phone = CreateTextBox(Data, 3, i, item.SellerPhone);
                var RoomNumber = CreateTextBox(Data, 4, i, item.PropertyRoomNumber.ToString());
                var Square = CreateTextBox(Data, 5, i, item.PropertySquare.ToString());
                var HasBalcony = CreateCheckBox(Data, 6, i, item.PropertyHasBalcony);
                var Cost = CreateTextBox(Data, 7, i, item.PropertyCost.ToString());
                var Address = CreateTextBox(Data, 8, i, item.PropertyAddress);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.SellerFirstName = FirstName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Имя"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.SellerMiddleName = MiddleName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Фамилия"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.SellerLastName = LastName.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Отчество"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.SellerPhone = Phone.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Телефон"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (RoomNumber.Text.Length > 0 && Regex.IsMatch(RoomNumber.Text, "^[0-9]*$")) item.PropertyRoomNumber = int.Parse(RoomNumber.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Количество комнат"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Square.Text.Length > 0 && Regex.IsMatch(Square.Text, "^[0-9]*$")) item.PropertySquare = int.Parse(Square.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Площадь"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    item.PropertyHasBalcony = HasBalcony.IsChecked ?? false;

                    if (Cost.Text.Length > 0 && Regex.IsMatch(Cost.Text, "^[0-9]*$")) item.PropertyCost = int.Parse(Cost.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Стоимость"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Address.Text.Length > 0) item.PropertyAddress = Address.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Адрес"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetPropertiesWithSellers(sortTag, tableTag);

                }, 9, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    if (MessageBox.Show(DELETE_TEXT, DELETE_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.ServiceNotification) == MessageBoxResult.No) return;

                    Database.DeleteProperty(item.PropertyId);
                    GetPropertiesWithSellers(sortTag, tableTag);

                }, 10, i, "Удалить");

                i++;

            }

        }

        private void AddPropertyHeader()
        {

            ClearGrid(Header);
            AddColumnDefinitions(Header, CELL_W_SIZE, 6);
            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Кол-во комнат");
            CreateLabel(Header, 2, 0, "Площадь");
            CreateLabel(Header, 3, 0, "Наличие балкона");
            CreateLabel(Header, 4, 0, "Стоимость");
            CreateLabel(Header, 5, 0, "Адрес");

        }


        private void GetProperties(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetProperties(sortTag, tableTag);

            ClearGrid(Data);
            AddPropertyHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 8);
            AddColumnDefinitions(Header, CELL_W_SIZE, 2);

            CreateLabel(Header, 6, 0, "Изменить");
            CreateLabel(Header, 7, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                CreateLabel(Data, 0, i, item.Id.ToString());
                var RoomNumber = CreateTextBox(Data, 1, i, item.RoomNumber.ToString());
                var Square = CreateTextBox(Data, 2, i, item.Square.ToString());
                var HasBalcony = CreateCheckBox(Data, 3, i, item.HasBalcony);
                var Cost = CreateTextBox(Data, 4, i, item.Cost.ToString());
                var Address = CreateTextBox(Data, 5, i, item.Address);

                var EditButton = CreateButton(Data, () =>
                {

                    if (RoomNumber.Text.Length > 0 && Regex.IsMatch(RoomNumber.Text, "^[0-9]{4}$")) item.RoomNumber = int.Parse(RoomNumber.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Количество комнат"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Square.Text.Length > 0 && Regex.IsMatch(Square.Text, "^[0-9]{4}$")) item.Square = int.Parse(Square.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Площадь"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    item.HasBalcony = HasBalcony.IsChecked ?? false;

                    if (Cost.Text.Length > 0 && Regex.IsMatch(Cost.Text, "^[0-9]{4}$")) item.Cost = int.Parse(Cost.Text);
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Стоимость"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Address.Text.Length > 0 && Regex.IsMatch(Address.Text, "^[А-Яа-ё,\\s]*$")) item.Address = Address.Text;
                    else MessageBox.Show(string.Format(ERROR_TEXT, "Адрес"), ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetProperties(sortTag, tableTag);

                }, 6, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    if (MessageBox.Show(DELETE_TEXT, DELETE_CAPTION, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.ServiceNotification) == MessageBoxResult.No) return;

                    Database.DeleteProperty(item.Id);
                    GetProperties(sortTag, tableTag);

                }, 7, i, "Удалить");

                i++;

            }

        }

        private void AddDealHeader()
        {

            ClearGrid(Header);
            AddColumnDefinitions(Header, CELL_W_SIZE, 3);
            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Регистрационный код");
            CreateLabel(Header, 2, 0, "Дата");

        }

        private void GetDeals(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetDeals(sortTag, tableTag);

            ClearGrid(Data);
            AddDealHeader();

            AddColumnDefinitions(Data, CELL_W_SIZE, 22);
            AddColumnDefinitions(Header, CELL_W_SIZE, 19);

            CreateLabel(Header, 3, 0, "Имя клиента");
            CreateLabel(Header, 4, 0, "Фамилия клиента");
            CreateLabel(Header, 5, 0, "Отчество клиента");
            CreateLabel(Header, 6, 0, "Номер клиента");
            CreateLabel(Header, 7, 0, "Серия");
            CreateLabel(Header, 8, 0, "Номер");
            CreateLabel(Header, 9, 0, "Имя риелтора");
            CreateLabel(Header, 10, 0, "Фамилия риелтора");
            CreateLabel(Header, 11, 0, "Отчество риелтора");
            CreateLabel(Header, 12, 0, "Номер риелтора");
            CreateLabel(Header, 13, 0, "Имя продовца");
            CreateLabel(Header, 14, 0, "Фамилия продовца");
            CreateLabel(Header, 15, 0, "Отчество продовца");
            CreateLabel(Header, 16, 0, "Номер продовца");
            CreateLabel(Header, 17, 0, "Кол-во комнат");
            CreateLabel(Header, 18, 0, "Площадь");
            CreateLabel(Header, 19, 0, "Наличие балкона");
            CreateLabel(Header, 20, 0, "Стоимость");
            CreateLabel(Header, 21, 0, "Адрес");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                CreateLabel(Data, 0, i, item.Id.ToString());
                CreateTextBox(Data, 1, i, item.RegistrationCode);
                CreateTextBox(Data, 2, i, item.Date.ToString());
                CreateTextBox(Data, 3, i, item.CustomerFirstName);
                CreateTextBox(Data, 4, i, item.CustomerMiddleName);
                CreateTextBox(Data, 5, i, item.CustomerLastName);
                CreateTextBox(Data, 6, i, item.CustomerPhone);
                CreateTextBox(Data, 7, i, item.PassportSeries);
                CreateTextBox(Data, 8, i, item.PassportNumber);
                CreateTextBox(Data, 9, i, item.RealtorFirstName);
                CreateTextBox(Data, 10, i, item.RealtorMiddleName);
                CreateTextBox(Data, 11, i, item.RealtorLastName);
                CreateTextBox(Data, 12, i, item.RealtorPhone);
                CreateTextBox(Data, 13, i, item.SellerFirstName);
                CreateTextBox(Data, 14, i, item.SellerMiddleName);
                CreateTextBox(Data, 15, i, item.SellerLastName);
                CreateTextBox(Data, 16, i, item.SellerPhone);
                CreateTextBox(Data, 17, i, item.PropertyRoomNumber.ToString());
                CreateTextBox(Data, 18, i, item.PropertySquare.ToString());
                CreateCheckBox(Data, 19, i, item.PropertyHasBalcony);
                CreateTextBox(Data, 20, i, item.PropertyCost.ToString());
                CreateTextBox(Data, 21, i, item.PropertyAddress);

                i++;

            }

        }

        private void ChooseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedItem = (ComboBoxItem)ChooseTable.SelectedItem;
            SearchText.Text = "";
            SearchList.Items.Clear();

            switch (selectedItem.Tag)
            {

                case "Realtors":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.FirstName,
                        Content = "Имя"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.MiddleName,
                        Content = "Фамилия"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.LastName,
                        Content = "Отчество"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Phone,
                        Content = "Телефон"
                    });

                    GetRealtors();

                    break;

                case "Sellers":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.FirstName,
                        Content = "Имя"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.MiddleName,
                        Content = "Фамилия"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.LastName,
                        Content = "Отчество"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Phone,
                        Content = "Телефон"
                    });

                    GetSellers();

                    break;

                case "Customers":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.FirstName,
                        Content = "Имя"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.MiddleName,
                        Content = "Фамилия"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.LastName,
                        Content = "Отчество"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Phone,
                        Content = "Телефон"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.PassportNumber,
                        Content = "Номер паспорта"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.PassportSeries,
                        Content = "Серия паспорта"
                    });

                    GetCustomers();

                    break;

                case "PropertiesWithSellers":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.FirstName,
                        Content = "Имя"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.MiddleName,
                        Content = "Фамилия"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.LastName,
                        Content = "Отчество"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Phone,
                        Content = "Телефон"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.RoomNumber,
                        Content = "Количество комнат"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Square,
                        Content = "Площадь"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Address,
                        Content = "Адрес"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Cost,
                        Content = "Стоимость"
                    });

                    GetPropertiesWithSellers();

                    break;

                case "Properties":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.RoomNumber,
                        Content = "Количество комнат"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Square,
                        Content = "Площадь"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Address,
                        Content = "Адрес"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Cost,
                        Content = "Стоимость"
                    });

                    GetProperties();

                    break;

                case "Deals":

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Id,
                        Content = "Id"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.RoomNumber,
                        Content = "Количество комнат"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Square,
                        Content = "Площадь"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Address,
                        Content = "Адрес"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Cost,
                        Content = "Стоимость"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.Date,
                        Content = "Дата"
                    });

                    SearchList.Items.Add(new ComboBoxItem()
                    {
                        Tag = (int)SorterOption.RegistrationCode,
                        Content = "Регистрационный код"
                    });

                    GetDeals();

                    break;

            }

        }
    }
}
