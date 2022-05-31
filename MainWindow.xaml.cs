﻿using ShopFront.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ShopFront
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> products = new List<Product>();
        private Product NewProduct = new Product();
        private string OldName;
        private Product SelectedProduct = new Product();

        public MainWindow()
        {
            InitializeComponent();
            GetProducts();

            AddNewProductGrid.DataContext = NewProduct;
        }

        private void GetProducts()
        {
           ShopProduct.ItemsSource = null;
           ShopProduct.ItemsSource = products;
        }

        private void AddProduct(object s, RoutedEventArgs e)
        {
            products.Add(new Product { ImageUrl = NewProduct.ImageUrl, Name = NewProduct.Name });
            NewProduct = new Product();
            AddNewProductGrid.DataContext = NewProduct;

            GetProducts();
        }

        private void UpdateProductForEdit(object s, RoutedEventArgs e)
        {
            SelectedProduct = (s as FrameworkElement).DataContext as Product;
            OldName = SelectedProduct.Name;
            UpdateProductGrid.DataContext= SelectedProduct;
        }

        private void UpdateProduct(object s, RoutedEventArgs e)
        {
            var product = products.FirstOrDefault(p => p.Name == OldName);
            if (product != null)
            {
                product.Name = SelectedProduct.Name;
                product.ImageUrl = SelectedProduct.ImageUrl;
            }

            GetProducts();
        }
    }
}
