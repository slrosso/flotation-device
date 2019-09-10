using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HCLib.FlotationDevice.Service.Models.DAL
{
  public interface IUnitOfWork : IDisposable
  {
    GenericRepository<Collection> CollectionRepository { get; }
    GenericRepository<Location> LocationRepository { get; }
    GenericRepository<LocationCollection> LocationCollectionRepository { get; }
    GenericRepository<ShelfType> ShelfTypeRepository { get; }
    GenericRepository<Shelving> ShelvingRepository { get; }
    void Save();
  }
}