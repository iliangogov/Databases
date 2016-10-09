namespace MongDB.Driver.Demos.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using MongDB.Driver.Demos.Models;

    public interface IRepository<T>
        where T : IEntity
    {
        Task Add(T value);

        Task<IQueryable<T>> All();

        Task Delete(object id);

        Task Delete(T obj);
    }
}