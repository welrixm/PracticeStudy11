using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PracticeStudy.Components
{
    partial class Product
    {
       
        public string ActiveText
        {
            get
            {
                if (IsActive == true)
                    return "Актуален";
                else
                    return "Нет в наличии";
                //return (IsActive) ? "Актуален" : "Нет в наличии";
            }
        }
        public string Color
        {
            get
            {
                if (IsActive == true)
                    return "#FDFDFD";
                else
                    return "#E7E3DF";
                //return (IsActive) ? "Актуален" : "Нет в наличии";
            }
        }
        public Visibility BtnVisible
        {
            get
            {
                if (Navigation.AuthUser.RoleId == 2)//client
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        //private static BrushConverter _brushConverter = new BrushConverter();
        //private Brush _brush;
        //public Brush brush
        //{
        //    get
        //    {
        //        if (_brush == null)
        //            _brush = (SolidColorBrush)_brushConverter.ConvertFrom($"#{Color}");
        //        return _brush;
        //    }
        }
    }

