using Core.Entities.Contracts;
using Core.UseCase.Util;
using System;

namespace Core.UseCase.Base
{
    public interface IUnitOfWork : IDisposable
    {
        ISistema Sistema { get; }
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit(IInteractor interactor);
    }
}
