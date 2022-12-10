using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PracticeStudy.Components
{
    public partial class ProductCountry
    {
        private static BrushConverter _brushConverter = new BrushConverter();
        private Brush _brush;
        public Brush brush
        {
            get
            {
                if (_brush == null)
                    _brush = (SolidColorBrush)_brushConverter.ConvertFrom($"#{ManufacturerCountry.Color}");
                return _brush;
            }
        }
    }
}
