using Mongo.Models;

namespace Mongo.Repository
{
    public interface IPessoaRepository
    {
        Task<List<Pessoa>> GetAsync();
        Task<Pessoa?> GetAsync(string id);
        Task CreateAsync(Pessoa pessoa);
        Task UpdateAsync(Pessoa pessoa);
        Task RemoveAsync(string id);
    }
}