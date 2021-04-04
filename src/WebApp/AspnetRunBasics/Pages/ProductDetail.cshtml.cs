﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.ApiCollection.Interfaces;
using AspnetRunBasics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        private readonly ICatalogApi _catalogApi;
        private readonly IBasketRepository _basketRepository;

        public ProductDetailModel(ICatalogApi catalogApi, IBasketRepository basketRepository)
        {
            _catalogApi = catalogApi ?? throw new ArgumentNullException(nameof(catalogApi));
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogApi.GetCatalog(productId);
            if (Product == null)
            {
                return NotFound();
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
            TempData["BasketInfoDetail"] = product.Name + " Sepete Eklendi";
            return RedirectToPage("ProductDetail", new { categoryName = product.Category });
        }
    }
}