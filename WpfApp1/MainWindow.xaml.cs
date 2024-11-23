using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
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

        private const string ERROR_TEXT = "Ошибка ввода данных!\nПовторите снова.";
        private const string ERROR_CAPTION = "Ошибка ввода данных";

        public MainWindow()
        {
            InitializeComponent();

            ChooseTable.SelectionChanged += ChooseTable_SelectionChanged;
            SearchList.SelectionChanged += SearchList_SelectionChanged;
            SearchText.TextChanged += SearchText_TextChanged;

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
            Grid.SetColumn(textBox, column);
            Grid.SetRow(textBox, row);
            grid.Children.Add(textBox);

            return textBox;

        }

        private Button CreateButton(Grid grid, Action func, int column = 0, int row = 0, string text = "")
        {

            var button = new Button();
            button.Content = text;
            button.Click += (o, e) => func.Invoke();
            Grid.SetColumn(button, column);
            Grid.SetRow(button, row);
            grid.Children.Add(button);

            return button;

        }

        private void GetRealtors(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetRealtors(sortTag, tableTag).ToList();
            
            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 6);
            AddColumnDefinitions(Header, CELL_W_SIZE, 6);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Изменить");
            CreateLabel(Header, 5, 0, "Удалить");

            int i = 0;
            foreach(var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var FirstName = CreateTextBox(Data, 0, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 1, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 2, i, item.LastName);
                var Phone = CreateTextBox(Data, 3, i, item.Phone);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetRealtors(sortTag, tableTag);

                }, 4, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    Database.DeleteRealtor(item.Id);
                    GetRealtors(sortTag, tableTag);

                }, 5, i, "Удалить");

                i++;

            }

        }

        private void GetSellers(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetSellers(sortTag, tableTag);

            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 6);
            AddColumnDefinitions(Header, CELL_W_SIZE, 6);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Изменить");
            CreateLabel(Header, 5, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var FirstName = CreateTextBox(Data, 0, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 1, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 2, i, item.LastName);
                var Phone = CreateTextBox(Data, 3, i, item.Phone);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetSellers(sortTag, tableTag);

                }, 4, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    Database.DeleteSeller(item.Id);
                    GetSellers(sortTag, tableTag);

                }, 5, i, "Удалить");

                i++;

            }

        }

        private void GetCustomers(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetCustomers(sortTag, tableTag);

            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 8);
            AddColumnDefinitions(Header, CELL_W_SIZE, 8);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Id");
            CreateLabel(Header, 1, 0, "Имя");
            CreateLabel(Header, 2, 0, "Фамилия");
            CreateLabel(Header, 3, 0, "Отчество");
            CreateLabel(Header, 4, 0, "Серия");
            CreateLabel(Header, 5, 0, "Номер");
            CreateLabel(Header, 6, 0, "Изменить");
            CreateLabel(Header, 7, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var FirstName = CreateTextBox(Data, 0, i, item.FirstName);
                var MiddleName = CreateTextBox(Data, 1, i, item.MiddleName);
                var LastName = CreateTextBox(Data, 2, i, item.LastName);
                var Phone = CreateTextBox(Data, 3, i, item.Phone);
                var PassportSeries = CreateTextBox(Data, 4, i, item.PassportSeries);
                var PassportNumber = CreateTextBox(Data, 5, i, item.PassportNumber);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.FirstName = FirstName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.MiddleName = MiddleName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.LastName = LastName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.Phone = Phone.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (PassportSeries.Text.Length > 0 && Regex.IsMatch(PassportSeries.Text, "^[0-9]{4}$")) item.PassportSeries = PassportSeries.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (PassportNumber.Text.Length > 0 && Regex.IsMatch(PassportNumber.Text, "^[0-9]{6}$")) item.PassportNumber = PassportNumber.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetCustomers(sortTag, tableTag);

                }, 6, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    Database.DeleteCustomer(item.Id);
                    GetCustomers(sortTag, tableTag);

                }, 7, i, "Удалить");

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
                var HasBalcony = CreateTextBox(Data, 6, i, item.PropertyHasBalcony.ToString());
                var Cost = CreateTextBox(Data, 7, i, item.PropertyCost.ToString());
                var Address = CreateTextBox(Data, 8, i, item.PropertyAddress);

                var EditButton = CreateButton(Data, () =>
                {

                    if (FirstName.Text.Length > 0 && Regex.IsMatch(FirstName.Text, "^[А-Я][а-ё]*$")) item.SellerFirstName = FirstName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (MiddleName.Text.Length > 0 && Regex.IsMatch(MiddleName.Text, "^[А-Я][а-ё]*$")) item.SellerMiddleName = MiddleName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (LastName.Text.Length > 0 && Regex.IsMatch(LastName.Text, "^[А-Я][а-ё]*$")) item.SellerLastName = LastName.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Phone.Text.Length > 0 && Regex.IsMatch(Phone.Text, "^[0-9]{11}$")) item.SellerPhone = Phone.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (RoomNumber.Text.Length > 0 && Regex.IsMatch(RoomNumber.Text, "^[0-9]{4}$")) item.PropertyRoomNumber = int.Parse(RoomNumber.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Square.Text.Length > 0 && Regex.IsMatch(Square.Text, "^[0-9]{4}$")) item.PropertySquare = int.Parse(Square.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (HasBalcony.Text.Length > 0 && Regex.IsMatch(HasBalcony.Text, "^[0-9]{4}$")) item.PropertyHasBalcony = bool.Parse(HasBalcony.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Cost.Text.Length > 0 && Regex.IsMatch(Cost.Text, "^[0-9]{4}$")) item.PropertyCost = int.Parse(Cost.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Address.Text.Length > 0 && Regex.IsMatch(Address.Text, "^[А-Яа-ё,\\s]*$")) item.PropertyAddress = Address.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetPropertiesWithSellers(sortTag, tableTag);

                }, 9, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    Database.DeleteProperty(item.PropertyId);
                    GetPropertiesWithSellers(sortTag, tableTag);

                }, 10, i, "Удалить");

                i++;

            }

        }

        private void GetProperties(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetProperties(sortTag, tableTag);

            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 7);
            AddColumnDefinitions(Header, CELL_W_SIZE, 7);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Кол-во комнат");
            CreateLabel(Header, 1, 0, "Площадь");
            CreateLabel(Header, 2, 0, "Наличие балкона");
            CreateLabel(Header, 3, 0, "Стоимость");
            CreateLabel(Header, 4, 0, "Адрес");
            CreateLabel(Header, 5, 0, "Изменить");
            CreateLabel(Header, 6, 0, "Удалить");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                var RoomNumber = CreateTextBox(Data, 0, i, item.RoomNumber.ToString());
                var Square = CreateTextBox(Data, 1, i, item.Square.ToString());
                var HasBalcony = CreateTextBox(Data, 2, i, item.HasBalcony.ToString());
                var Cost = CreateTextBox(Data, 3, i, item.Cost.ToString());
                var Address = CreateTextBox(Data, 4, i, item.Address);

                var EditButton = CreateButton(Data, () =>
                {

                    if (RoomNumber.Text.Length > 0 && Regex.IsMatch(RoomNumber.Text, "^[0-9]{4}$")) item.RoomNumber = int.Parse(RoomNumber.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Square.Text.Length > 0 && Regex.IsMatch(Square.Text, "^[0-9]{4}$")) item.Square = int.Parse(Square.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (HasBalcony.Text.Length > 0 && Regex.IsMatch(HasBalcony.Text, "^[0-9]{4}$")) item.HasBalcony = bool.Parse(HasBalcony.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Cost.Text.Length > 0 && Regex.IsMatch(Cost.Text, "^[0-9]{4}$")) item.Cost = int.Parse(Cost.Text);
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    if (Address.Text.Length > 0 && Regex.IsMatch(Address.Text, "^[А-Яа-ё,\\s]*$")) item.Address = Address.Text;
                    else MessageBox.Show(ERROR_TEXT, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

                    Database.Save();
                    GetProperties(sortTag, tableTag);

                }, 5, i, "Изменить");

                var DeleteButton = CreateButton(Data, () =>
                {

                    Database.DeleteProperty(item.Id);
                    GetProperties(sortTag, tableTag);

                }, 6, i, "Удалить");

                i++;

            }

        }

        private void GetDeals(SorterOption sortTag = SorterOption.None, string tableTag = "")
        {

            var res = Database.GetDeals(sortTag, tableTag);

            ClearGrid(Data);
            ClearGrid(Header);

            AddColumnDefinitions(Data, CELL_W_SIZE, 19);
            AddColumnDefinitions(Header, CELL_W_SIZE, 19);

            AddRowDefinitions(Header, CELL_H_SIZE);

            CreateLabel(Header, 0, 0, "Имя клиента");
            CreateLabel(Header, 1, 0, "Фамилия клиента");
            CreateLabel(Header, 2, 0, "Отчество клиента");
            CreateLabel(Header, 3, 0, "Номер клиента");
            CreateLabel(Header, 4, 0, "Серия");
            CreateLabel(Header, 5, 0, "Номер");
            CreateLabel(Header, 6, 0, "Имя риелтора");
            CreateLabel(Header, 7, 0, "Фамилия риелтора");
            CreateLabel(Header, 8, 0, "Отчество риелтора");
            CreateLabel(Header, 9, 0, "Номер риелтора");
            CreateLabel(Header, 10, 0, "Имя продовца");
            CreateLabel(Header, 11, 0, "Фамилия продовца");
            CreateLabel(Header, 12, 0, "Отчество продовца");
            CreateLabel(Header, 13, 0, "Номер продовца");
            CreateLabel(Header, 14, 0, "Кол-во комнат");
            CreateLabel(Header, 15, 0, "Площадь");
            CreateLabel(Header, 16, 0, "Наличие балкона");
            CreateLabel(Header, 17, 0, "Стоимость");
            CreateLabel(Header, 18, 0, "Адрес");

            int i = 0;
            foreach (var item in res)
            {

                AddRowDefinitions(Data, CELL_H_SIZE);

                CreateTextBox(Data, 0, i, item.CustomerFirstName);
                CreateTextBox(Data, 1, i, item.CustomerMiddleName);
                CreateTextBox(Data, 2, i, item.CustomerLastName);
                CreateTextBox(Data, 3, i, item.CustomerPhone);
                CreateTextBox(Data, 4, i, item.PassportSeries);
                CreateTextBox(Data, 5, i, item.PassportNumber);
                CreateTextBox(Data, 6, i, item.RealtorFirstName);
                CreateTextBox(Data, 7, i, item.RealtorMiddleName);
                CreateTextBox(Data, 8, i, item.RealtorLastName);
                CreateTextBox(Data, 9, i, item.RealtorPhone);
                CreateTextBox(Data, 10, i, item.SellerFirstName);
                CreateTextBox(Data, 11, i, item.SellerMiddleName);
                CreateTextBox(Data, 12, i, item.SellerLastName);
                CreateTextBox(Data, 13, i, item.SellerPhone);
                CreateTextBox(Data, 14, i, item.PropertyRoomNumber.ToString());
                CreateTextBox(Data, 15, i, item.PropertySquare.ToString());
                CreateTextBox(Data, 16, i, item.PropertyHasBalcony.ToString());
                CreateTextBox(Data, 17, i, item.PropertyCost.ToString());
                CreateTextBox(Data, 18, i, item.PropertyAddress);

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
