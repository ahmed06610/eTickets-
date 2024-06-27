using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Linq.Expressions;

namespace eTickets.Services.Base
{
    public interface IGenericService<T> where T : class,IGService, new()
    {
        //CRUD
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties);
        Task<T> Get(int id);
        Task Create(T model);
        Task Update(int id,T model);
        Task<bool> Delete(int id);
    }
}
