using ShopFront.Interfaces.Services;
using ShopFront.Models;
using System.Collections.Generic;
using System.Windows;

namespace ShopFront
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IHTTPService _hTTPService;

        private List<Product> _products;
        private Product _newProduct = new Product();
        private Product _selectedProduct = new Product();
        private string _oldName;
        private string _oldImageUrl;

        public MainWindow(IHTTPService hTTPService)
        {
            _hTTPService = hTTPService;
            _products = _hTTPService.GetProducts();
            InitializeComponent();
            GetProducts();
            AddNewProductGrid.DataContext = _newProduct;
        }

        private void GetProducts()
        {
            _products = _hTTPService.GetProducts();
            ShopProduct.ItemsSource = null;
           ShopProduct.ItemsSource = _products;
        }

        private void AddProduct(object s, RoutedEventArgs e)
        {
            _products.Add(new Product { ImageUrl = _newProduct.ImageUrl, Name = _newProduct.Name });
            _hTTPService.CreateProduct(_newProduct.Name, _newProduct.ImageUrl);
            _newProduct = new Product();
            AddNewProductGrid.DataContext = _newProduct;

            GetProducts();
        }

        private void UpdateProductForEdit(object s, RoutedEventArgs e)
        {
            _selectedProduct = (s as FrameworkElement).DataContext as Product;
            _oldName = _selectedProduct.Name;
            _oldImageUrl = _selectedProduct.ImageUrl;
            UpdateProductGrid.DataContext= _selectedProduct;
            GetProducts();
        }

        private void UpdateProduct(object s, RoutedEventArgs e)
        {
            var result = _hTTPService.UpdateProduct(_selectedProduct.Name, _selectedProduct.ImageUrl, _oldName, _oldImageUrl);
            
            if (result)
            {
                _oldName = _selectedProduct.Name;
                _oldImageUrl = _selectedProduct.ImageUrl;
            }

            GetProducts();
        }

        private void DeleteProduct(object s, RoutedEventArgs e)
        {
            var productToDeleted = (s as FrameworkElement).DataContext as Product;

            if (_products.Remove(productToDeleted))
            {
                _hTTPService.DeleteProduct(productToDeleted.Name, productToDeleted.ImageUrl);
            }

            GetProducts();
        }
    }
}
