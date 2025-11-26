using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmacyStore.Models
{
    [Serializable]
    public class Cart
    {
        private readonly List<CartItem> _items = new List<CartItem>();
        public IReadOnlyCollection<CartItem> Items => _items;

        public void Add(CartItem item, int qty = 1)
        {
            var exist = _items.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (exist == null)
            {
                item.Quantity = Math.Max(qty, 1);
                _items.Add(item);
            }
            else
            {
                exist.Quantity += Math.Max(qty, 1);
            }
        }

        public void Update(int productId, int qty)
        {
            var line = _items.FirstOrDefault(x => x.ProductId == productId);
            if (line == null) return;
            if (qty <= 0) _items.Remove(line);
            else line.Quantity = qty;
        }

        public void Remove(int productId)
        {
            var line = _items.FirstOrDefault(x => x.ProductId == productId);
            if (line != null) _items.Remove(line);
        }

        public void Clear() => _items.Clear();

        public int TotalQuantity => _items.Sum(x => x.Quantity);
        public decimal TotalAmount => _items.Sum(x => x.LineTotal);
        public bool IsEmpty => !_items.Any();
    }
}
