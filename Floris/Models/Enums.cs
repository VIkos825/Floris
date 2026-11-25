namespace Floris.Models
{
    /// <summary>Статус заказа. Определяет этап жизненного цикла заказа.</summary>
    public enum OrderStatus
    {
        /// <summary>Заказ создан, ожидает подтверждения.</summary>
        New,
        /// <summary>Заказ подтверждён магазином.</summary>
        Confirmed,
        /// <summary>Флорист собирает букет.</summary>
        Assembling,
        /// <summary>Заказ собран (готов к выдаче или передаче курьеру).</summary>
        Ready,
        /// <summary>Курьер доставляет заказ.</summary>
        Delivering,
        /// <summary>Заказ вручён получателю или выдан при самовывозе.</summary>
        Delivered,
        /// <summary>Заказ отменён.</summary>
        Cancelled
    }

    /// <summary>Способ получения заказа.</summary>
    public enum DeliveryMethod
    {
        /// <summary>Доставка курьером по адресу.</summary>
        Delivery,
        /// <summary>Самовывоз из магазина.</summary>
        Pickup
    }

    /// <summary>Способ оплаты.</summary>
    public enum PaymentMethod
    {
        /// <summary>Банковская карта.</summary>
        Card,
        /// <summary>Система быстрых платежей.</summary>
        Sbp,
        /// <summary>Наличными при получении.</summary>
        Cash
    }

    /// <summary>Статус платежа.</summary>
    public enum PaymentStatus
    {
        /// <summary>Ожидает оплаты.</summary>
        Pending,
        /// <summary>Оплачен.</summary>
        Success,
        /// <summary>Платёж не прошёл.</summary>
        Failed,
        /// <summary>Средства возвращены.</summary>
        Refunded
    }

    /// <summary>Тип товара в каталоге.</summary>
    public enum ProductType
    {
        /// <summary>Цветок или зелень (продаётся поштучно).</summary>
        Flower,
        /// <summary>Готовый букет или композиция.</summary>
        Bouquet,
        /// <summary>Сопутствующий товар: открытка, ваза, упаковка.</summary>
        Accessory
    }

    /// <summary>Статус партии товара на складе магазина.</summary>
    public enum StockStatus
    {
        /// <summary>В наличии.</summary>
        Available,
        /// <summary>Остаток мал.</summary>
        Low,
        /// <summary>Нет в наличии.</summary>
        OutOfStock,
        /// <summary>Уценка (товар скоро потеряет товарный вид).</summary>
        Markdown
    }

    /// <summary>Тип фотографии в фото-отчёте.</summary>
    public enum PhotoType
    {
        /// <summary>Итоговое фото букета.</summary>
        Final,
        /// <summary>Фото деталей.</summary>
        Detail,
        /// <summary>Фото упаковки.</summary>
        Packaging
    }

    /// <summary>Роль компонента в собранном букете.</summary>
    public enum BouquetComponentRole
    {
        /// <summary>Основные цветы.</summary>
        Base,
        /// <summary>Дополнительные цветы.</summary>
        Filler,
        /// <summary>Зелень.</summary>
        Greenery,
        /// <summary>Упаковка и декор.</summary>
        Packaging
    }
}
