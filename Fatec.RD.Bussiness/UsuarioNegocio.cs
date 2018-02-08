using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
   public sealed class UsuarioNegocio
    {
        UsuarioRepositorio _usuarioRepositorio;
        public UsuarioNegocio()
        {
            _usuarioRepositorio = new UsuarioRepositorio();
        }

        /// <summary>
        /// Método que seleciona uma lista com todos os Usuarios...
        /// </summary>
        /// <returns></returns>
        public List<Usuario> SelecionarTodos()
        {
          return  _usuarioRepositorio.SelecionarTodos();
        }
        /// <summary>
        /// Método que seleciona um Usuario Utilizando Id...
        /// </summary>
        /// <returns></returns>
        public Usuario SelecionarPorId(int id)
        {
            return _usuarioRepositorio.SelecionarPorId(id);
        }
       
   
        /// <summary>
        /// Método que Atualiza um Usuario Utilizando Id...
        /// </summary>
        /// <returns></returns>
        public Usuario Atualizar( int id, UsuarioInput input)
        {
            var obj = this.SelecionarPorId(id);
            obj.CarteiraTrabalho = input.CarteiraTrabalho;
            obj.DataNascimento = input.DataNascimento;
            obj.Email = input.Email;
            obj.Nome = input.Nome;
            obj.Tipo = input.Tipo;
            obj.Senha = input.senha;
            obj.Id = id;
            
            _usuarioRepositorio.Atualizar(obj);
            return obj;
        }
        /// <summary>
        /// Método que Deleta um Usuario Utilizando Id...
        /// </summary>
        /// <returns></returns>
        public void Deletar(int id)
        {
            _usuarioRepositorio.Delete(id);
        }
     
       

        public Usuario Adicionar(UsuarioInput obj)
        {
            var condicao = _usuarioRepositorio.SelecionarTodos(obj.Email);

            if (condicao != null)
                throw new ConflitoException($"Já existe um usuario com o email: {condicao.Email}, cadastrado!");
            var data = obj.DataNascimento;
            var novoObj = new Usuario()
            {
                Email = obj.Email,
                DataNascimento = data,
                Nome = obj.Nome,
                Senha = obj.senha,
                CarteiraTrabalho = obj.CarteiraTrabalho,
                Tipo = obj.Tipo
                

            };

            novoObj.Validar();

            var retorno = _usuarioRepositorio.Adicionar(novoObj);


            return _usuarioRepositorio.SelecionarPorId(retorno);
        }





    }
}
