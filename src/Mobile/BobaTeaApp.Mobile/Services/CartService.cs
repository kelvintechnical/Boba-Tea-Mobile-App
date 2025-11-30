using System.Collections.ObjectModel;
using BobaTeaApp.Shared.DTOs;

namespace BobaTeaApp.Mobile.Services;

public sealed class CartService
{
    private readonly ObservableCollection<CartItemDto> _items = new();

    public ReadOnlyObservableCollection<CartItemDto> Items { get; }
    public event EventHandler? CartChanged;

    public CartService()
    {
        Items = new ReadOnlyObservableCollection<CartItemDto>(_items);
    }

    public void AddOrUpdate(CartItemDto item)
    {
        var existingIndex = _items.ToList().FindIndex(i => i.ProductId == item.ProductId && i.SelectedSize == item.SelectedSize);
        if (existingIndex >= 0)
        {
            var existing = _items[existingIndex];
            var newQuantity = existing.Quantity + item.Quantity;
            var updated = existing with { Quantity = newQuantity, LineTotal = newQuantity * existing.UnitPrice };
            _items[existingIndex] = updated;
        }
        else
        {
            _items.Add(item);
        }

        CartChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateQuantity(Guid productId, int quantity)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existing == null) return;

        if (quantity <= 0)
        {
            _items.Remove(existing);
        }
        else
        {
            var updated = existing with { Quantity = quantity, LineTotal = quantity * existing.UnitPrice };
            _items[_items.IndexOf(existing)] = updated;
        }

        CartChanged?.Invoke(this, EventArgs.Empty);
    }

    public decimal Subtotal => _items.Sum(i => i.LineTotal);

    public void Clear()
    {
        _items.Clear();
        CartChanged?.Invoke(this, EventArgs.Empty);
    }
}
