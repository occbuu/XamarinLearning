﻿using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Demo.ViewModels
{
    using Models;
    using Services;

    public class ProductsVM2 : BaseVM
    {
        ProductsService2 _productsRepository = new ProductsService2();

        private IEnumerable<ProductModel> _products;

        public IEnumerable<ProductModel> Products
        {
            get
            {
                return _products;
            }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public double ProductPrice { get; set; }

        public string ProductTitle { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Products = await _productsRepository.GetAllAsync();
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var product = new ProductModel
                    {
                        Title = ProductTitle,
                        Price = ProductPrice,
                    };
                    await _productsRepository.AddAsync(product);
                });
            }
        }
    }
}