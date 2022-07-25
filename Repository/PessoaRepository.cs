using Microsoft.Extensions.Options;
using Mongo.Models;
using MongoDB.Driver;

namespace Mongo.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly IMongoCollection<Pessoa> _pessoa;

        public PessoaRepository(IOptions<PessoaStoreSetting> setting)
        {
            var client = new MongoClient(setting.Value.ConnectionString);
            var database = client.GetDatabase(setting.Value.DatabaseName);
            _pessoa = database.GetCollection<Pessoa>(setting.Value.PessoaCollectionName);
        }

        public async Task<List<Pessoa>> GetAsync() 
        {
            return await _pessoa.Find(_ => true).ToListAsync();
        }

        public async Task<Pessoa?> GetAsync(string id)
        {
            return await _pessoa.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
            
        public async Task CreateAsync(Pessoa pessoa)
        {
            await _pessoa.InsertOneAsync(pessoa);
        }

        public async Task UpdateAsync(Pessoa pessoa)
        {
            await _pessoa.ReplaceOneAsync(x => x.Id == pessoa.Id, pessoa);
        }

        public async Task RemoveAsync(string id)
        {
            await _pessoa.DeleteOneAsync(x => x.Id == id);
        }
    }
}