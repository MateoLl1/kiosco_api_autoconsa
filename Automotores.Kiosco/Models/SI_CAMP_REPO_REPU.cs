using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_CAMP_REPO_REPU
{
    public decimal RrCodigo { get; set; }

    public decimal RrTipo { get; set; }

    public string RrNombre { get; set; } = null!;

    public string? RrNombReal { get; set; }

    public string? RrNombRealS { get; set; }

    public string? RrProcedencia { get; set; }

    public bool RrTotalizar { get; set; }

    public bool RrCabecera { get; set; }

    public string? RrNombRealS1 { get; set; }

    public decimal? RrNombRealS2 { get; set; }

    public DateTime? RrFecha { get; set; }

    public decimal? RrValoNume { get; set; }
}
