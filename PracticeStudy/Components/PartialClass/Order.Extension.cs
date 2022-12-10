using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeStudy.Components
{
    public partial class Order
    {
        public ObservableCollection<ProductOrder> ProductOrders
        {
            get
            {
                return new ObservableCollection<ProductOrder>(ProductOrder);
            }
        }
        public int? Quantity
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Quantity);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Quantity * x.Product.Cost);
            }
        }
    }
}
