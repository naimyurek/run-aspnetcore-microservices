﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketRepository _basketRepository;

        public ProductModel(ICatalogApi catalogApi, IBasketRepository basketRepository)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        public IEnumerable<CategoryModel> CategoryList { get; set; } = new List<CategoryModel>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var productList = await _catalogApi.GetCatalog();

            //CategoryList = productList.Select(p => p.Category).Distinct();         
            
            CategoryList = from product in productList
                     orderby product.Category
                     group product by product.Category into Category
                     select new CategoryModel(){ Name = Category.Key, Count = Category.Count() };

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                ProductList = productList.Where(p => p.Category == categoryName);                
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
                SelectedCategory = "Tüm Ürünler";
            }

            return Page();
        }
        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var basket = _basketRepository.GetAllBasket();

            var item = basket.Items.Where(x => x.ProductId == productId).FirstOrDefault();
            basket.Items.Remove(item);

            _basketRepository.Update(basket);

            return RedirectToPage("Product", new { categoryName = item.Category });
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _catalogApi.GetCatalog(productId);

            var basket = _basketRepository.GetAllBasket();
            if (basket.Items.Find(i => i.ProductId == productId) == null)
            {
                basket.Items.Add(new BasketItemRepositoryModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImageFile = product.ImageFile,
                    Category = product.Category
                    
                });
            }
            else
            {
                basket.Items.Find(i => i.ProductId == productId).Quantity++;
            }

            _basketRepository.Update(basket);
            TempData["BasketInfo"] = product.Name + " Sepete Eklendi";
            return RedirectToPage("Product", new { categoryName = product.Category });
        }
    }
}