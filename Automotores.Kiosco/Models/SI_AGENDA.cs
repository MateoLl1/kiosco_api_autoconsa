using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_AGENDA
{
    public decimal AaCodigo { get; set; }

    public decimal? BhCodigo { get; set; }

    public decimal? AgCodigo { get; set; }

    public DateTime? AaFechaInicio { get; set; }

    public DateTime? AaFechaFin { get; set; }

    public decimal? TeCodigo { get; set; }

    /// <summary>
    /// 0. Sin Estatus 1.Entregado/Terminado 2. No Asistio
    /// </summary>
    public short? AaStatus { get; set; }

    public decimal? UsCodigo { get; set; }

    public decimal? ClCodigo { get; set; }

    public decimal? HcCodigo { get; set; }

    public string? AaObservacion { get; set; }

    public DateTime? AaFechConf { get; set; }

    public DateTime? AaFechCanc { get; set; }

    public DateTime? AaFechLleg { get; set; }

    public bool? AaConfirmado { get; set; }

    /// <summary>
    /// 1. Agenda Entrega Vendedores 2. Agenda Accesorios
    /// </summary>
    public short? AaTipoAgen { get; set; }

    public decimal? HtCodigo { get; set; }

    public DateTime? AaFechaFinEsti { get; set; }

    public decimal? AtCodigo { get; set; }

    public int? AaCaliClie { get; set; }

    public string? AaComeClie { get; set; }

    public DateTime? AaFechIngr { get; set; }

    /// <summary>
    /// Contiene los mails de los asistentes a  la reunion de la agenda
    /// </summary>
    public string? AaMails { get; set; }

    public bool? AaPuntualidad { get; set; }

    public decimal? StdCodigo { get; set; }

    public decimal? PrCodigo { get; set; }

    public decimal? UsCodiLogi { get; set; }

    public decimal? UsCodiCall { get; set; }

    public decimal? RfoCodigo { get; set; }

    public int? AaTiemAgen { get; set; }

    public int? AaProcAgen { get; set; }

    public string? AaIdCale { get; set; }

    /// <summary>
    /// Asesor servicio en entrega de vehículo
    /// </summary>
    public decimal? TeCodiAgen { get; set; }

    /// <summary>
    /// Asesor servicio asiste a entrega de vehículo
    /// </summary>
    public decimal? TeCodiAgenAsis { get; set; }

    public decimal? OiCodigo { get; set; }

    public decimal? StCodigo { get; set; }

    public bool AaSaliConc { get; set; }

    public decimal? GuCodigo { get; set; }

    public int AaKiloIni { get; set; }

    public bool AaValiCedu { get; set; }

    public bool AaValiLice { get; set; }
}
