using System;
using System.Collections.Generic;

namespace Automotores.Kiosco.Models;

public partial class SI_STICKER
{
    public decimal StCodigo { get; set; }

    public decimal? MoCodigo { get; set; }

    public string? StNumeMoto { get; set; }

    public string? StNumeChas { get; set; }

    public string? StSticker { get; set; }

    public decimal? AiCodigo { get; set; }

    public decimal? UsCodigo { get; set; }

    public DateTime? StFechMovi { get; set; }

    public decimal? OrCodigo { get; set; }

    public short? CoCodigo { get; set; }

    public short? AñCodigo { get; set; }

    public string? StPlaca { get; set; }

    public string? StChevy { get; set; }

    public string? StDisco { get; set; }
}
