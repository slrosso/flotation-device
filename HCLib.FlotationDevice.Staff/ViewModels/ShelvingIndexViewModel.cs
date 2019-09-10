using HCLib.FlotationDevice.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Staff.ViewModels
{
  public class ShelvingIndexViewModel : BaseViewModel
  {
    public IEnumerable<Shelving>  slist { get; set; }
    public IEnumerable<ShelfType> stypes { get; set; }
    public IEnumerable<LocationCollection> loccol { get; set; }
    public IEnumerable<Collection> col { get; set; }
    public string location { get; set; }
  }
}