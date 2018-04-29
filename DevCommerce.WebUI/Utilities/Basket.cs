using DevCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevCommerce.WebUI.Utilities
{
    public class Basket<T> where T : BasketProduct
    {
        private static readonly Lazy<Dictionary<int, T>> _myBasket = new Lazy<Dictionary<int, T>>();
        public void AddBasket(T product)
        {
            if (_myBasket.Value.ContainsKey(product.ProductId))
                _myBasket.Value[product.ProductId].Quantity += product.Quantity;
            else
                _myBasket.Value.Add(product.ProductId, product);
        }
        public IList<T> BasketProducts
        {
            get { return _myBasket.Value.Values.ToList(); }
        }
        public decimal TotalAmount
        {
            get { return _myBasket.Value.Values.Select(x => x.SubTotal).Sum(); }

        }
        public void SepettenKaldir(int productId)
        {
            _myBasket.Value.Remove(productId);
        }
        public int Quantity
        {
            get { return _myBasket.Value.Values.Count; }
        }
        public int TotalQuantity
        {
            get { return _myBasket.Value.Values.Select(x => x.Quantity).Sum(); }
        }
        public void UpdateBasket(int productId, int quantity)
        {
            _myBasket.Value[productId].Quantity = quantity;
            if (_myBasket.Value[productId].Quantity <= 0)
                SepettenKaldir(productId);
        }
    }
}
