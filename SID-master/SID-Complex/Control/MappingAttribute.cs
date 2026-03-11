using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SID.Complex.Control.Tables;
using System.Data;

namespace SID.Complex.Control
{
    /// <summary>
    /// Classe de mapemaneto de atributos
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        /// <summary>
        /// Nome da coluna vinculada
        /// </summary>
        public string ColumnName = null;

        /// <summary>
        /// Tipo de variavel
        /// </summary>
        public MappingType Type = MappingType.Null;

        /// <summary>
        /// Variavel não pode ser nula
        /// </summary>
        public bool NotNull = false;

        /// <summary>
        /// Tabela estrageira ligada ao campo
        /// </summary>
        public ForeignTable ForeignTable = ForeignTable.Null;

        /// <summary>
        /// Variavel de vinculo não editavél
        /// </summary>
        public bool Constant = false;
        
    }


    /// <summary>
    /// Tipo de campo
    /// </summary>
    public enum MappingType
    {
        /// <summary>
        /// Campo nulo
        /// </summary>
        Null,
        /// <summary>
        /// Chave Primaria
        /// </summary>
        PrimaryKey,
        /// <summary>
        /// Valores Inteiro
        /// </summary>
        Integer,
        /// <summary>
        /// Valores reais
        /// </summary>
        Real,
        /// <summary>
        /// Texto
        /// </summary>
        Text,
        /// <summary>
        /// Tipo Booleana
        /// </summary>
        Boolean,
        /// <summary>
        /// Chave Estrangeira
        /// </summary>
        ForeignKey
    }

    /// <summary>
    /// Tabela estrageira ligada ao campo
    /// </summary>
    public enum ForeignTable
    {
        /// <summary>
        /// Chave nula
        /// </summary>
        Null,
        /// <summary>
        /// Tabela de Areas
        /// </summary>
        Area,
        /// <summary>
        /// Tabela de Controladores
        /// </summary>
        Controller,
        /// <summary>
        /// Tabela de Painel
        /// </summary>
        Cabinet,
        /// <summary>
        /// Tabela de tipo de entrada analogica
        /// </summary>
        AnalogInputType,
        /// <summary>
        /// Tabela de tipo de Saida analogica
        /// </summary>
        AnalogOutputType,
        /// <summary>
        /// Tabela de unidades de engenharia
        /// </summary>
        EngineeringUnit,
        /// <summary>
        /// Tabela de tipos de motores
        /// </summary>
        MotorType,
        /// <summary>
        /// Tabela de tipo de controladores
        /// </summary>
        ControllerType,
        /// <summary>
        ///Tabela de tipo de totalizadores
        /// </summary>
        TotalizerType,
        /// <summary>
        /// Tabela de tipo de Válvulas
        /// </summary>
        ValveType,
        /// <summary>
        /// Tabela de tipo de VSD
        /// </summary>
        VSDType,
        /// <summary>
        /// Tabela de fornecedores de VSD
        /// </summary>
        VSDVendor,
        /// <summary>
        /// Tabela de entradas Analogicas
        /// </summary>
        CmAnalogIn,
        /// <summary>
        /// Ttbela de saida Analogicas
        /// </summary>
        CmAnalogOut,
        /// <summary>
        /// Classes de fase
        /// </summary>
        PhaseClass,
        /// <summary>
        /// Tabela de texto para Alarme/Avisos/Infos
        /// </summary>
        PhaseAlarmWarningInfosText,
        /// <summary>
        /// Enumeração de grupo
        /// </summary>
        EnumerationGroup
    }

}
