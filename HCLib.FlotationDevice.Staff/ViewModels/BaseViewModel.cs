using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Staff.ViewModels
{
  public class BaseViewModel : staff_assets.ViewModels.BaseViewModel
  {
    public BaseViewModel()
    {
      this.ApplicationName = "Flotation device";
      this.PageTitle = "Flotation device";
    }
  }
}