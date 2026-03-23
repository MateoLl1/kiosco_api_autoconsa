using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_TURNO
{
    public decimal TuCodigo { get; set; }

    public decimal? AgCodigo { get; set; }

    public decimal? TuConCita { get; set; }

    public decimal? TuSinCita { get; set; }

    public decimal? TuFlota { get; set; }

    public decimal? TuCitaLato { get; set; }
}
