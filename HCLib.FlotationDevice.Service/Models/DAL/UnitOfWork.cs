using System;
using System.Data.Entity;
using HCLib.FlotationDevice.Service.Models;

namespace HCLib.FlotationDevice.Service.Models.DAL
{
  public class UnitOfWork : IDisposable, IUnitOfWork
  {
    private FlotationDeviceEntities context = new FlotationDeviceEntities();

    private GenericRepository<Collection> _CollectionRepository;
    private GenericRepository<Location> _LocationRepository;
    private GenericRepository<LocationCollection> _LocationCollectionRepository;
    private GenericRepository<ShelfType> _ShelfTypeRepository;
    private GenericRepository<Shelving> _ShelvingRepository;

    public UnitOfWork() { }

    public UnitOfWork(FlotationDeviceEntities context)
    {
      this.context = context;
    }

    public GenericRepository<Collection> CollectionRepository
    {
      //get or create if null
      get
      {
        if (this._CollectionRepository == null)
        {
          this._CollectionRepository = new GenericRepository<Collection>(context);
        }
        return _CollectionRepository;
      }
    }
    public GenericRepository<Location> LocationRepository
    {
      //get or create if null
      get
      {
        if (this._LocationRepository == null)
        {
          this._LocationRepository = new GenericRepository<Location>(context);
        }
        return _LocationRepository;
      }
    }
    public GenericRepository<LocationCollection> LocationCollectionRepository
    {
      //get or create if null
      get
      {
        if (this._LocationCollectionRepository == null)
        {
          this._LocationCollectionRepository = new GenericRepository<LocationCollection>(context);
        }
        return _LocationCollectionRepository;
      }
    }
    public GenericRepository<ShelfType> ShelfTypeRepository
    {
      //get or create if null
      get
      {
        if (this._ShelfTypeRepository == null)
        {
          this._ShelfTypeRepository = new GenericRepository<ShelfType>(context);
        }
        return _ShelfTypeRepository;
      }
    }
    public GenericRepository<Shelving> ShelvingRepository
    {
      //get or create if null
      get
      {
        if (this._ShelvingRepository == null)
        {
          this._ShelvingRepository = new GenericRepository<Shelving>(context);
        }
        return _ShelvingRepository;
      }
    }

    public void Save()
    {
      context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
      if (!this.disposed)
      {
        if (disposing)
        {
          context.Dispose();
        }
      }
      this.disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}