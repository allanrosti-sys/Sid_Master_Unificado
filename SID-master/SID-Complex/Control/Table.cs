using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SID.Complex.Control.Model;
using System.Data;


namespace SID.Complex.Control
{
    /// <summary>
    /// Controle de tabelas do banco
    /// </summary>
    public class Table
    {
        protected string field01;
        protected string field02;
        protected string field03;
        protected string field04;
        protected string field05;
        protected string field06;

        /// <summary>
        /// Nome visivel da tabela
        /// </summary>
        protected string name;
        /// <summary>
        /// Nome da tabela no banco
        /// </summary>
        protected string tableName;
        /// <summary>
        /// Conector Complex para manipulação dos dados com o banco
        /// </summary>
        protected Ctrl_Complex complex;

        /// <summary>
        /// Nome visivel da tabela
        /// </summary>
        public string Name { get => name; }

        /// <summary>
        /// Nome da tabela no banco
        /// </summary>
        public string TableName { get => tableName; }

        /// <summary>
        /// Conector Complex para manipulação dos dados com o banco
        /// </summary>
        public Ctrl_Complex Complex { get => complex; set => complex = value; }

        /// <summary>
        /// Mapeador de Classe através de Tabela de dados
        /// </summary>
        /// <param name="dataTable">Tabela de dados</param>
        /// <returns></returns>
        public virtual object MapModel(DataTable dataTable)
        {
            return null;
        }

        /// <summary>
        /// Seleciona todos registro da tabela do banco, com as colunas para vizualização em tabela
        /// </summary>
        /// <returns></returns>
        public virtual DataTable Select()
        {
            if (field03 != "" && field03 != null)
            {
                string sqlStringViewer = $"SELECT {field01},{field02},{field03} FROM {tableName} ORDER BY {field01};";
                return complex.Connection.Select(sqlStringViewer);
            }
            else
            {
                string sqlStringViewer = $"SELECT {field01},{field02} FROM {tableName} ORDER BY {field01};";
                return complex.Connection.Select(sqlStringViewer);
            }
        }

        /// <summary>
        /// Seleciona apanas uma linha na tabela do banco
        /// </summary>
        /// <param name="id">ID da linha desejada</param>
        /// <returns></returns>
        public virtual DataTable SelectRow(int id)
        {
            string sqlSelectRow = $"SELECT * FROM {tableName} WHERE {field01} = {id};";
            return complex.Connection.SelectRow(sqlSelectRow);
        }

        /// <summary>
        /// Seleciona apanas uma linha na tabela do banco
        /// </summary>
        /// <param name="id">ID da linha desejada</param>
        /// <returns></returns>
        public virtual DataTable SelectRow(string condition)
        {
            string sqlSelectRow = $"SELECT * FROM {tableName} WHERE {condition};";
            return complex.Connection.SelectRow(sqlSelectRow);
        }

        /// <summary>
        /// Selecionas todas as linhas na tabela do banco, com apenas as colunas de visualização para selecão
        /// </summary>
        /// <returns></returns>
        public virtual DataTable SelectView()
        {
            string sqlStringViewer = $"SELECT {field01},{field02} FROM {tableName} ORDER BY {field01};";
            return complex.Connection.Select(sqlStringViewer);
        }

        /// <summary>
        /// Realiza atualização de um regsitro no banco
        /// </summary>
        /// <param name="input">Registro com as informações a serem atualizadas</param>
        /// <returns></returns>
        public virtual int Update(object input)
        {
            return 0;
        }

        /// <summary>
        /// Realiza a inserção de um registro no banco
        /// </summary>
        /// <param name="input">Registro com as informações a serem inseridas</param>
        /// <returns></returns>
        public virtual int Insert(object input)
        {
            return 0;
        }

        /// <summary>
        /// Deleta um registro na tabela confome ID
        /// </summary>
        /// <param name="id">ID do registro a ser deletado</param>
        /// <returns></returns>
        public virtual int DeleteRow(int id)
        {
            string sqlString = $"DELETE FROM {tableName} WHERE {field01} = {id};";
            return complex.Connection.Delete(sqlString);
        }

        /// <summary>
        /// Criação de um novo modelo, sem preenchimento 
        /// </summary>
        /// <returns></returns>
        public virtual object NewModel()
        {
            return null;
        }

        /// <summary>
        /// Duplica um registro para um novo, com os campos preenchidos
        /// </summary>
        /// <returns></returns>
        public virtual object DuplicateModel(int id)
        {
            return null;
        }

    }




}
