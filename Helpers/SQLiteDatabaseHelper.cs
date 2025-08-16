using MauiAppMinhasCompras.Models;
using SQLite;


namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;
        public SQLiteDatabaseHelper(string path) 
        { 
        _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p) 
        { 
            return _connection.InsertAsync(p); //retonar uma tarefa pararela, task é aguardavel
        }

        public Task<List<Produto>> Update(Produto p)  //tarefa que será uma lista de produtos
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _connection.QueryAsync<Produto>( //Query solicita qual a tabela ou model está sendo atualizado
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id  //marcadores       
                ); 
        }

        public Task<int> Delete(int id) 
        { 
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>>GetAll()  //tarefa que vai retornar uma lista de produtos
        {
            return _connection.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string query) 
        {
            string sql = "SELECT * Produto WHERE descricao LIKE'%" + query + "%'";

            return _connection.QueryAsync<Produto>(sql);
        }


    }
}
