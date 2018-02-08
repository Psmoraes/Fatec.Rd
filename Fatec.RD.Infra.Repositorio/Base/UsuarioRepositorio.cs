using Dapper;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fatec.RD.Dominio.Modelos;
using System;

namespace Fatec.RD.Infra.Repositorio.Base
{
    public class UsuarioRepositorio
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;
        public UsuarioRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }
         //SELECIONAR TODOS
        public List<Usuario> SelecionarTodos()
        {
            var sqlCommand = @"SELECT Id, Nome, CarteiraTrabalho,DataNascimento, Email, Tipo FROM  Usuario";

            return _connection.Query<Usuario>(sqlCommand).ToList();
        }
        public Usuario SelecionarTodos(string email)
        {
            var sqlCommand = @"SELECT Id, Nome, CarteiraTrabalho,DataNascimento, Email, Tipo FROM  Usuario WHERE Email=@email";

            return _connection.Query<Usuario>(sqlCommand, new { email = $"%{email}%" }).FirstOrDefault();
        }
        //SELECIONAR POR ID
        public Usuario SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT Id,Senha, Nome, CarteiraTrabalho,DataNascimento, Email, Tipo FROM  Usuario WHERE Id=@id";

            return _connection.Query<Usuario>(sqlCommand, new { id }).FirstOrDefault();
        }

        //DELETAR
        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Despesa where IdUsuario =@Id", new { Id = id });
            _connection.Execute("DELETE FROM Usuario WHERE Id = @Id", new { Id = id });
        }
        //Atualizar
        public void Atualizar( Usuario obj)
        {
            _connection.Execute("UPDATE Usuario SET CarteiraTrabalho=@CarteiraTrabalho," +
                " DataNascimento=@DataNascimento, Email=@Email, Senha=@Senha, Tipo=@Tipo, Nome=@Nome WHERE id=@id",obj);
        }

        //ADICIONAR
        public int Adicionar(Usuario obj)
        {
            return _connection.Query<int>(@"INSERT INTO Usuario VALUES(@CarteiraTrabalho,@DataNascimento,@Email,@Senha,@Tipo,@Nome)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

    }
}
