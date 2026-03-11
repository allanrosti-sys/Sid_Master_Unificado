using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SID.Standard.Control;

namespace SID.Complex.Control
{
    /// <summary>
    /// Funções basicas
    /// </summary>
    public class Function
    {
        /// <summary>
        /// Mapeamento de tabelas do banco de dados para classe  
        /// </summary>
        /// <typeparam name="T">Classe tipo</typeparam>
        /// <param name="reader">Valores retornados da leitura do banco</param>
        /// <returns>Retorna os dados no formado da classe tipo</returns>
        public static T MapToClass<T>(SqlDataReader reader) where T : class
        {
            T returnedObject = Activator.CreateInstance<T>();
            List<PropertyInfo> modelProperties = returnedObject.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
            for (int i = 0; i < modelProperties.Count; i++)
                modelProperties[i].SetValue(returnedObject, Convert.ChangeType(reader.GetValue(i), modelProperties[i].PropertyType), null);
            return returnedObject;
        }

        /// <summary>
        /// Mapeamento de tabelas de dados para classe 
        /// </summary>
        /// <typeparam name="T">Objeto classe a ser preechida</typeparam>
        /// <param name="reader">Valores a serem utilizados através de uma tabela</param>
        /// <returns>Retorna os dados no formado da classe tipo</returns>
        public static T MapToClass<T>(DataTable reader) where T : class
        {
            T returnedObject = Activator.CreateInstance<T>();
            List<PropertyInfo> modelProperties = returnedObject.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
            for (int i = 0; i < modelProperties.Count; i++)
                modelProperties[i].SetValue(returnedObject, Convert.ChangeType(reader.Rows[0][i], modelProperties[i].PropertyType), null);
            return returnedObject;
        }

        /// <summary>
        /// Cria string de Update para o banco atrevés de um objeto classe
        /// </summary>
        /// <typeparam name="T">Classe tipo</typeparam>
        /// <param name="input">Classe com os dados a serem lidos</param>
        /// <param name="tableName">Nome da tabela para fazer Update</param>
        /// <param name="Condition">Inseri as condições em formato SQL</param>
        /// <returns>Retorna a string sem formato SQL</returns>
        public static string Update_String<T>(T input, string tableName, string Condition) where T : class
        {
            string returnedString = $"UPDATE {tableName} SET %VALUELIST% WHERE {Condition};";
            List<PropertyInfo> modelProperties = input.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
            for (int i = 1; i < modelProperties.Count; i++)
            {
                PropertyInfo pi = modelProperties[i];
                object value = input.GetType().GetProperty(pi.Name).GetValue(input);

                if (value.ToString() != "")
                {
                    returnedString = returnedString.Replace("%VALUELIST%", $"{pi.Name} = '{value}', %VALUELIST%");
                }
                else
                {
                    returnedString = returnedString.Replace("%VALUELIST%", $"{pi.Name} = null, %VALUELIST%");
                }
            }

            returnedString = returnedString.Replace(", %VALUELIST%", $"");
            return returnedString;
        }

        /// <summary>
        /// Cria string de insert para o banco através de um objeto classe
        /// </summary>
        /// <typeparam name="T">Classe tipo</typeparam>
        /// <param name="input">Classe com os dados a serem lidos</param>
        /// <param name="tableName">Nome da tabela para fazer a inserção</param>
        /// <returns>Retorna a string sem formato SQL</returns>
        public static string Insert_String<T>(T input, string tableName) where T : class
        {
            string returnedString = $"INSERT INTO {tableName} (%NAME%) VALUES (%VALUE%);";
            List<PropertyInfo> modelProperties = input.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
            for (int i = 0; i < modelProperties.Count; i++)
            {
                PropertyInfo pi = modelProperties[i];
                object value = input.GetType().GetProperty(pi.Name).GetValue(input);

                returnedString = returnedString.Replace("%NAME%", $"{pi.Name}, %NAME%");
                if (value.ToString() != "")
                {
                    returnedString = returnedString.Replace("%VALUE%", $"'{value}', %VALUE%");
                }
                else
                {
                    returnedString = returnedString.Replace("%VALUE%", $"null, %VALUE%");
                }
            }
            returnedString = returnedString.Replace(", %NAME%", $"");
            returnedString = returnedString.Replace(", %VALUE%", $"");
            return returnedString;
        }

        /// <summary>
        /// Busca o ultimo inteiro de um tabela confome os campo solicitado
        /// </summary>
        /// <param name="tableName">Nome da tabela</param>
        /// <param name="field">Campo solicitado</param>
        /// <param name="condition">Condições em SQL para a busca do Campo</param>
        /// <returns></returns>
        public static string SqlLastID(string tableName, string field, string condition)
        {
            string stringCommand;
            if (condition != "")
            {
                stringCommand = $"SELECT TOP 1 {field} FROM {tableName} WHERE {condition} ORDER BY {field} DESC ; ";
            }
            else
            {
                stringCommand = $"SELECT TOP 1 {field} FROM {tableName} ORDER BY {field} DESC; ";
            }
            return stringCommand;
        }

       
    }
}
