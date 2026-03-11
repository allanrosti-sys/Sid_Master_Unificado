using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SID.Standard.Control;
using SID.Complex.Control.Tables;

namespace SID.Complex.Control
{
    public class Ctrl_Complex
    {
        #region Constantes

        #endregion

        #region Campos

        /// <summary>
        /// Conexão utilizada para execução do Complex
        /// </summary>
        Connection connection;

        #endregion

        #region Propriedades

        /// <summary>
        /// Conexão utilizada para execução do Complex
        /// </summary>
        public Connection Connection { get => connection; set => connection = value; }

        #endregion

        #region Construtores

        /// <summary>
        /// Controle do Complex
        /// </summary>
        public Ctrl_Complex()
        {
            connection = new Connection();

        }

        #endregion



        #region Funções 

        /// <summary>
        /// Função para buscar dados de tabelas estrageiras
        /// </summary>
        /// <param name="foreignTable">Tabela extrangeira</param>
        /// <returns>Retorna tabelas de dados preechido com a tabela estrageira</returns>
        public DataTable GetForeignTable(ForeignTable foreignTable)
        {
            DataTable dt = new DataTable();
            switch (foreignTable)
            {
                case ForeignTable.Null:
                    break;
                case ForeignTable.Area:
                    dt = new Table_tblAreas() { Complex = this }.SelectView();
                    break;
                case ForeignTable.Controller:
                    dt = new Table_tblControllers() { Complex = this }.SelectView();
                    break;
                case ForeignTable.Cabinet:
                    dt = new Table_tblCabinets() { Complex = this }.SelectView();
                    break;
                case ForeignTable.AnalogInputType:
                    dt = new Table_tblCmAnalogInputType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.EngineeringUnit:
                    dt = new Table_tblEngineeringUnits() { Complex = this }.SelectView();
                    break;
                case ForeignTable.AnalogOutputType:
                    dt = new Table_tblCmAnalogOutputType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.MotorType:
                    dt = new Table_tblCmMotorType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.ControllerType:
                    dt = new Table_tblControllerTypes() { Complex = this }.SelectView();
                    break;
                case ForeignTable.TotalizerType:
                    dt = new Table_tblCmTotalizerType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.ValveType:
                    dt = new Table_tblCmValveType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.VSDType:
                    dt = new Table_tblCmVSDType() { Complex = this }.SelectView();
                    break;
                case ForeignTable.VSDVendor:
                    dt = new Table_tblCmVSDVendor() { Complex = this }.SelectView();
                    break;
                case ForeignTable.CmAnalogIn:
                    dt = new Table_tblCmAnalogInput() { Complex = this }.SelectView();
                    break;
                case ForeignTable.CmAnalogOut:
                    dt = new Table_tblCmAnalogOutput() { Complex = this }.SelectView();
                    break;
                case ForeignTable.PhaseClass:
                    dt = new Table_tblPhaseClass() { Complex = this }.SelectView();
                    break;
                case ForeignTable.PhaseAlarmWarningInfosText:
                    dt = new Table_tblPhaseAlarmWarningInfosText() { Complex = this }.SelectView();
                    break;
                case ForeignTable.EnumerationGroup:
                    dt = new Table_tblEnumerationGroup() { Complex = this }.SelectView();
                    break;
                default:
                    break;
            }
            dt.Columns[0].ColumnName = "id";
            dt.Columns[1].ColumnName = "text";

            return dt;
        }

        #endregion



        public void Magic()
        {
            Directory.CreateDirectory("GEN//Model");
            Directory.CreateDirectory("GEN//Tables");

            DataTable dt0 = connection.Select("SELECT table_name, table_schema, table_type FROM information_schema.tables ORDER BY table_name ASC; ");


            foreach (DataRow item in dt0.Rows)
            {
                string modelName = item[0].ToString();

                #region Lista Ignore

                List<string> ignore = new List<string>();
                ignore.Add("tblCmAnalogInput");
                ignore.Add("tblCmAnalogInputType");
                ignore.Add("tblAreas");
                ignore.Add("tblControllers");
                ignore.Add("tblControllerTypes");
                ignore.Add("tblCabinets");
                ignore.Add("tblEngineeringUnits");
                ignore.Add("tblCmAnalogOutputType");
                ignore.Add("tblCmAnalogOutput");
                ignore.Add("tblCmDigitalInput");
                ignore.Add("tblCmDigitalOutput");
                ignore.Add("tblCmMotorType");
                ignore.Add("tblCmMotor");
                ignore.Add("tblCmTotalizerType");
                ignore.Add("tblCmTotalizer");
                ignore.Add("tblCmValveType");
                ignore.Add("tblCmValve");
                ignore.Add("tblCmVSDType");
                ignore.Add("tblCmVSDVendor");
                ignore.Add("tblCmVSD");
                ignore.Add("tblCmPID");
                ignore.Add("tblPhaseClass");







                bool pula = false;
                foreach (string ig in ignore)
                {
                    if (ig == modelName)
                    {
                        pula = true;
                        break;
                    }

                }
                if (pula)
                {
                    continue;
                }

                #endregion

                DataTable dt = Connection.Select($"SELECT * FROM {item[1].ToString() + "." + item[0].ToString()};");

                #region Model
                StreamWriter doc = new StreamWriter($"GEN//Model//{modelName}.cs");
                doc.WriteLine("using System;");
                doc.WriteLine("using System.Collections.Generic;");
                doc.WriteLine("using System.Linq;");
                doc.WriteLine("using System.Text;");
                doc.WriteLine("using System.Threading.Tasks;");
                doc.WriteLine();

                doc.WriteLine("namespace SID.Complex.Control.Model");
                doc.WriteLine("{");
                doc.WriteLine($"    public class {modelName}");
                doc.WriteLine("    {");

                foreach (DataColumn column in dt.Columns)
                {


                    string name = column.Caption;
                    string type = "";
                    string MappingType = "";
                    switch (name[0])
                    {
                        case 'i':
                            {
                                type = "int";
                                if (name == "iID")
                                {
                                    MappingType = "PrimaryKey";
                                }
                                else if (name[name.Length - 2] == 'I' && name[name.Length - 1] == 'D')
                                {

                                    string foreignTable = name.Substring(1, name.Length - 3);
                                    MappingType = $"ForeignKey, ForeignTable = ForeignTable.{foreignTable}";
                                }
                                else
                                {
                                    MappingType = "Integer";
                                }
                                break;
                            }
                        case 's': type = "string"; MappingType = "Text"; break;
                        case 'r': type = "float"; MappingType = "Real"; break;
                        case 'b': type = "bool"; MappingType = "Boolean"; break;

                        default:
                            break;
                    }

                    doc.WriteLine($"        [Mapping(ColumnName = \"{name}\", Type = MappingType.{MappingType})]");
                    doc.WriteLine($"        public {type} {name} {{ get; set; }}");
                    doc.WriteLine();
                }
                doc.WriteLine("    }");
                doc.WriteLine("}");
                doc.Close();
                #endregion

                #region Tables

                string tableName = $"Table_{modelName}";

                StreamWriter doc2 = new StreamWriter($"GEN//Tables//{tableName}.cs");
                doc2.WriteLine("using System;");
                doc2.WriteLine("using System.Data;");
                doc2.WriteLine("using SID.Complex.Control.Model;");
                doc2.WriteLine("using System.Reflection;");
                doc2.WriteLine();

                doc2.WriteLine("namespace SID.Complex.Control.Tables");
                doc2.WriteLine("{");
                doc2.WriteLine($"    public class {tableName} : Table");
                doc2.WriteLine("    {");

                doc2.WriteLine($"            public {tableName}()");
                doc2.WriteLine("        {");
                doc2.WriteLine($"                tableName = \"{modelName}\";");
                doc2.WriteLine("                name = \" \";");
                doc2.WriteLine();

                doc2.WriteLine($"              {modelName} model = new {modelName}();");
                doc2.WriteLine("                field01 = GetColumnName(nameof(model.iID));");
                doc2.WriteLine("                field02 = GetColumnName(nameof(model.sName_1));");
                doc2.WriteLine("                field03 = GetColumnName(nameof(model.sName_2));");
                doc2.WriteLine("                field04 = GetColumnName(nameof(model.iIndexNo));");
                doc2.WriteLine("                field05 = GetColumnName(nameof(model.iControllerID));");
                doc2.WriteLine("                //field06 = GetColumnName(nameof(model.iID));");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("                string GetColumnName(string name)");
                doc2.WriteLine("                {");
                doc2.WriteLine($"                    {modelName} model = new {modelName}();");
                doc2.WriteLine("                    return ((MappingAttribute)model.GetType().GetProperty(name).GetCustomAttribute(typeof(MappingAttribute))).ColumnName;");
                doc2.WriteLine("            } ");

                doc2.WriteLine("        public override object MapModel(DataTable dataTable)");
                doc2.WriteLine("        {");
                doc2.WriteLine($"            {modelName} returnedObject = Function.MapToClass<{modelName}>(dataTable);");
                doc2.WriteLine("            return returnedObject;");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("        public override int Update(object input)");
                doc2.WriteLine("        {");
                doc2.WriteLine($"            {modelName} model = ({modelName})input;");
                doc2.WriteLine($"            string sqlString = Function.Update_String<{modelName}>(model, tableName, $\"{{field01}} = {{model.iID}}\");");
                doc2.WriteLine("            return complex.Connection.Update(sqlString);");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("        public override int Insert(object input)");
                doc2.WriteLine("        {");
                doc2.WriteLine($"            {modelName} model = ({modelName})input;");
                doc2.WriteLine("            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, \"\"))) + 1;");
                doc2.WriteLine("            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $\"{field05}={model.iControllerID}\"))) + 1;");
                doc2.WriteLine($"            string sqlString = Function.Insert_String<{modelName}>(({modelName})input, tableName);");
                doc2.WriteLine("            return complex.Connection.Insert(sqlString);");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("        public override object NewModel()");
                doc2.WriteLine("        {");
                doc2.WriteLine($"            {modelName} model = new {modelName}();");
                doc2.WriteLine("            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, \"\"))) + 1;");
                doc2.WriteLine("            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $\"{field05}={model.iControllerID}\"))) + 1;");
                doc2.WriteLine("            return model;");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("        public override object DuplicateModel(int id)");
                doc2.WriteLine("        {");
                doc2.WriteLine("            DataTable dt = SelectRow(id);");
                doc2.WriteLine($"            {modelName} model = ({modelName})MapModel(dt);");
                doc2.WriteLine("            model.iID = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field01, \"\"))) + 1;");
                doc2.WriteLine("            model.iIndexNo = Convert.ToInt32(complex.Connection.SelectScalar(Function.SqlLastID(tableName, field04, $\"{field05}={model.iControllerID}\"))) + 1;");
                doc2.WriteLine("            return model;");
                doc2.WriteLine("        }");
                doc2.WriteLine();

                doc2.WriteLine("    }");
                doc2.WriteLine("}");
                doc2.Close();


                #endregion 
            }

        }
    }
}

