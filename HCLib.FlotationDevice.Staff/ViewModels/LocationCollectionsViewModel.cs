using HCLib.FlotationDevice.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Staff.ViewModels
{
  public class LocationCollectionsViewModel : BaseViewModel
  {
    public IEnumerable<LocationCollection> fclist { get; set; }
    public Collection col { get; set; }
    public Location libloc { get; set; }
    public String error { get; set; }
  }
}