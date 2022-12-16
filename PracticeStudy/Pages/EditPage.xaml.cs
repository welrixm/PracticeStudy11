using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PracticeStudy.Components;
using System.IO;


namespace PracticeStudy.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        Components.Product product;
       
        public EditPage(Components.Product _product)
        {

            InitializeComponent();
            CountryList.ItemsSource = DBConnect.db.ManufacturerCountry.ToList();
            product = _product;
            DataContext = product;
            UnitCbx.ItemsSource = DBConnect.db.Unit.ToList();
            UnitCbx.DisplayMemberPath = "Name";
            //CountryCbx.ItemsSource = DBConnect.db.ManufacturerCountry.ToList();
            //CountryCbx.DisplayMemberPath = "Name";
        }
        private void ClearCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            int countryid = 0;
            if (CountryList.SelectedItems != null)
            {
                var itemCountry = CountryList.SelectedIndex + 1;
                countryid = itemCountry;
                var productCountry = DBConnect.db.ProductCountry.Where(x => x.ManufacturerCountryId == itemCountry && x.ProductId == product.Id).FirstOrDefault();
                DBConnect.db.ProductCountry.Remove(productCountry);
                DBConnect.db.SaveChanges();
                MessageBox.Show("Вы удалили страну");
            }
            else
            {
                MessageBox.Show("Выберите страну");
            }
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountryList.SelectedItem != null)
            {
                var itemCountry = CountryList.SelectedItem as ManufacturerCountry;
                ProductCountry productCountry = new ProductCountry
                {
                    ManufacturerCountry = itemCountry,
                    Product = product
                };
                DBConnect.db.ProductCountry.Add(productCountry);
                DBConnect.db.SaveChanges();
                MessageBox.Show("Вы выбрали страну/страны");
            }
            else
            {
                MessageBox.Show("Выберите страну");
            }
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(product.Id == 0)
            {
                
                DBConnect.db.Product.Add(product);

            }
            
            DBConnect.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!");

        }

        private void AddImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                product.MainImage = File.ReadAllBytes(openFileDialog.FileName);
                ProductImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        //private void ClearCountryBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    if(CountryList.SelectedItem != null)
        //    {
        //        List<ManufacturerCountry> list = new List<ManufacturerCountry>();
        //        foreach(var itemManufacturerCountry in CountryList.Items)
        //        {
        //            if (itemManufacturerCountry != CountryList.SelectedItem)
        //                list.Remove(itemManufacturerCountry as ManufacturerCountry);
        //        }
        //        CountryList.ItemsSource = list;
        //        MessageBox.Show("Удалено");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите страну");
        //    }
        //}

        //private void AddBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    string selected = this.CountryCbx.GetItemText(this.CountryCbx.SelectedItem);
        //    CountTbx.Text = selected;
        //}
    }
}
