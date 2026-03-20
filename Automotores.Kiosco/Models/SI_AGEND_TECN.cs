using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_AGEND_TECN
{
    public decimal AtCodigo { get; set; }

    public decimal? BhCodigo { get; set; }

    public DateTime? AtFecha { get; set; }

    public string? AtHoraInicio { get; set; }

    public string? AtTiempo { get; set; }

    public string? AtHoraFin { get; set; }

    public decimal? TlCodigo { get; set; }

    /// <summary>
    /// 1 CITA O AGENDADO
    /// 2 CONFIRMADO 
    /// 3 CONCESIONARIO 
    /// 4 ATENDIDO 
    /// 5 CANCELADO 
    /// 6 NO LLEGO 
    /// 7 TERMINADO
    /// 8 REPUESTOS
    /// 9 AUTORIZACION
    /// 10 LAVADORA
    /// 11 CAMBIO AREA
    /// </summary>
    public decimal? AtStatus { get; set; }

    public decimal? UsCodigo { get; set; }

    public decimal? ClCodigo { get; set; }

    public decimal? StCodigo { get; set; }

    public string? PmCodigo { get; set; }

    public string? IsCodigo { get; set; }

    public string? ReCodigo { get; set; }

    public decimal? HtCodigo { get; set; }

    public bool? AtTaxi { get; set; }

    public string? AtHoraTaxi { get; set; }

    public decimal? KiCodigo { get; set; }

    public string? AtObservacion { get; set; }

    public decimal? AtCodiRela { get; set; }

    public DateTime? AtFechConf { get; set; }

    public DateTime? AtFechCanc { get; set; }

    public DateTime? AtFechLleg { get; set; }

    public DateTime? AtFechHoja { get; set; }

    public string? AtEstado { get; set; }

    public DateTime? AtFechFina { get; set; }

    public string? AtTiempoReal { get; set; }

    /// <summary>
    /// --NOMENCLATURA
    ///  NULL  CUANDO SOLO SE HISO LA CITA O ES REAGENDA
    ///  T HOJA DE TRABAJO SE ABRIO EN BAHIA
    ///  C HOJA DE TRABAJO SE ABRIO EN LA CENTRAL  ATENDIMIENTO.
    /// </summary>
    public string? AtUbicCrea { get; set; }

    /// <summary>
    /// 1	NO CONTESTA
    /// 2	REAGENDA
    /// 3	NO EXISTE INTERÉS
    /// 4	ELIGE OTRO TALLER
    /// </summary>
    public int? AtMotiAnu { get; set; }

    public DateTime? AtFechCc1 { get; set; }

    public short? AtEstaCc2 { get; set; }

    public string? AtEstaCc { get; set; }

    public decimal? FsCodigo { get; set; }

    public string? AtEstaCc1 { get; set; }

    public decimal? FaCodigo { get; set; }

    public decimal? IoCodigo { get; set; }

    public string? AtFrecuencia { get; set; }

    public string? AtPreeTrab { get; set; }

    public decimal? PfCodigo { get; set; }

    public decimal? AtIntePrea { get; set; }

    public bool? AtInteStat { get; set; }

    public DateTime? AtFechCrea { get; set; }

    public decimal? CmcCodigo { get; set; }

    public short? TccCodigo { get; set; }

    public decimal? AtKilome { get; set; }

    public decimal? EecCodigo { get; set; }

    public decimal? UsCodigoCarc { get; set; }

    public DateTime? AtFechGest { get; set; }

    public decimal? PeCodiCita { get; set; }

    public decimal? PeCodiLlam { get; set; }

    public decimal? UsCodigoGest { get; set; }

    public decimal? FsCodigoPrea { get; set; }

    public decimal? FaCodigoPrea { get; set; }

    public decimal? FltCodigo { get; set; }

    public decimal? CmCodigo { get; set; }
}
