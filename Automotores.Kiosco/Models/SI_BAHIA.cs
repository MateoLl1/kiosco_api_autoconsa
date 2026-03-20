using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_BAHIA
{
    public decimal BhCodigo { get; set; }

    public string? BhNombre { get; set; }

    public decimal? TeCodigo { get; set; }

    public decimal? TlCodigo { get; set; }

    public bool? BhStatus { get; set; }

    public decimal? AgCodigo { get; set; }

    public string? BhDescripcion { get; set; }

    public string? BhTipo { get; set; }

    public decimal? BhOrden { get; set; }

    /// <summary>
    /// 1 BAHIAS BLOQUEASAS PARA CALL CENTER
    /// 0 DAFAULT
    /// </summary>
    public bool? BhBahiaTipo { get; set; }
}
