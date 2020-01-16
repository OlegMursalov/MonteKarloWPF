using System.Collections.Generic;

namespace MonteKarloWPFApp1.DTO
{
    public class CalculationDTO
    {
        public InfoByFormuls InfoByFormuls { get; set; }
        public List<InfoByMonteCarlo> InfoByMonteCarlo { get; set; }

        public CalculationDTO()
        {
            InfoByMonteCarlo = new List<InfoByMonteCarlo>();
        }
    }
}