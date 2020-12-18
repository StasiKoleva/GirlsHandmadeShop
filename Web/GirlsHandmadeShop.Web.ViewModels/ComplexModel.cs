using System;
using System.Collections.Generic;
using System.Text;

namespace GirlsHandmadeShop.Web.ViewModels
{
    public class ComplexModel<InputType, ViewType>
    {
        public InputType InputModel { get; set; }

        public ViewType ViewModel { get; set; }
    }
}
