using System;

namespace Automotores.Kiosco.Models.dto;

public class TurnoGeneradoDto
    {
        public decimal AsgCodigo { get; set; }
        public string Turno { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }