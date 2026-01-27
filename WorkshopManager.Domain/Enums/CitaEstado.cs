using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopManager.Domain.Enums
{
    public enum CitaEstado
    {
        PendienteEntrega = 0,
        EnTaller = 1,
        EnProceso = 2,
        EnEsperaRecogida = 3,
        Finalizada = 4,
        Cancelada = 5
    }
}
