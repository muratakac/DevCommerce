using DevCommerce.Entities;
using System.Collections.Generic;

namespace DevCommerce.WebUI.Utilities
{
    public interface IBasket<T> where T : BasketProduct
    {
        void AddBasket(T product);
        IList<T> BasketProducts { get; }
        decimal TotalAmount { get; }
        void SepettenKaldir(int productId);
        int Quantity { get; }
        int TotalQuantity { get; }
        void UpdateBasket(int productId, int quantity);
    }
}
