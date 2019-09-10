using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Staff.ViewModels
{
  public class ShelvingViewModel : BaseViewModel
  {
    public int ShelvingID { get; set; }
    public int LocationCollectionID { get; set; }
    public int ShelfTypeID { get; set; }
    public int ShelfQty { get; set; }
    public int Length { get; set; }
    public int RowQty { get; set; }
    public string Note { get; set; }
    public DateTime Updated { get; set; }
  }
}