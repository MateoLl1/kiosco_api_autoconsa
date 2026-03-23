using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_ASIG_TURNO
{
    public decimal AsgCodigo { get; set; }

    public string? TuId { get; set; }

    public decimal? UsCodigo { get; set; }

    public DateTime? AsgFechMovi { get; set; }

    public string? AsgModulo { get; set; }

    public decimal? CiCodigo { get; set; }

    public decimal? AgCodigo { get; set; }

    public string? AsgEstado { get; set; }

    public DateTime? AsgFechAsig { get; set; }

    public decimal? AsgTime { get; set; }

    public decimal? AsgTimeEspe { get; set; }

    public decimal? AsgCita { get; set; }

    public decimal? AsgLlegada { get; set; }

    public decimal? ClCodiAseg { get; set; }
}
