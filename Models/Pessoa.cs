using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Mongo.Models
{
    public class Pessoa
    {
        [BsonElement("Id")]
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [BsonElement("Nome")]
        [JsonPropertyName("Nome")]
        public string Nome { get; set; }

        [BsonElement("Ativo")]
        [JsonPropertyName("Ativo")]
        public bool Ativo { get; set; }
    }
}