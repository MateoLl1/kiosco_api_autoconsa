using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_DISP_VEND
{
    public decimal DvCodigo { get; set; }

    public string? DvEstado { get; set; }

    public decimal? TeCodigo { get; set; }

    public decimal? UsCodiCrea { get; set; }

    public DateTime? DvFecha { get; set; }

    public short? DvPrioridad { get; set; }

    public decimal? AgCodigo { get; set; }

    public decimal? DvNumAten { get; set; }

    public decimal? TaCodigo { get; set; }

    public bool? DvBandAten { get; set; }

    public bool? DvTipo { get; set; }

    public DateTime? DvFechUltiAten { get; set; }

    public DateTime? DvFechLogin { get; set; }

    public decimal? UsCodigo { get; set; }

    public decimal? GnCodigo { get; set; }
}
