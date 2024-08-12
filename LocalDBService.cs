using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqliteDemo1925597
{
    public class LocalDBService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //Le indica al sistema que crea la tabla de nuestro contexto
            _connection.CreateTableAsync<Cliente>();
        }

        //Método para listar los registros de nuestra tabla
        public async Task<List<Cliente>> GetClientes()
        {
            return await _connection.Table<Cliente>().ToListAsync();
        }

        //Método para listar los registros por id
        public async Task<Cliente> GetById(int id)
        {
            return await _connection.Table<Cliente>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Método para crear registros
        public async Task Create(Cliente cliente)
        {
            await _connection.InsertAsync(cliente);
        }

        //Método para actualizar
        public async Task Update(Cliente cliente)
        {
            await _connection.UpdateAsync(cliente);
        }

        //Método para eliminar
        public async Task Delete(Cliente cliente)
        {
            await _connection.DeleteAsync(cliente);
        }
    }
}
