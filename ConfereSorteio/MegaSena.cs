namespace ConfereSorteios
{
    internal class MegaSena : ILoteria
    {
        //Crie uma propriedade para cada coluna do JSON {"Concurso":"1","Sorteio":"11/03/1996","Bola1":"4","Bola2":"5","Bola3":"30","Bola4":"33","Bola5":"41","Bola6":"52","Ganhadores6":"0","CidadeUF":"","Rateio6":"0","Ganhadores5":"17","Rateio5":"39158,92","Ganhadores4":"2016","Rateio4":"330,21","Acumulado6":"1714650,23","ArrecadacaoTotal":"0","EstimativaPremio":"0","AcumuladoVirada":"0"}
        public required string Concurso { get; set; }
        public required string Sorteio { get; set; }
        public required string Bola1 { get; set; }
        public required string Bola2 { get; set; }
        public required string Bola3 { get; set; }
        public required string Bola4 { get; set; }
        public required string Bola5 { get; set; }
        public required string Bola6 { get; set; }
        public string? Ganhadores6 { get; set; }
        public string? CidadeUF { get; set; }
        public string? Rateio6 { get; set; }
        public string? Ganhadores5 { get; set; }
        public string? Rateio5 { get; set; }
        public string? Ganhadores4 { get; set; }
        public string? Rateio4 { get; set; }
        public string? Acumulado6 { get; set; }
        public string? ArrecadacaoTotal { get; set; }
        public string? EstimativaPremio { get; set; }
        public string? AcumuladoVirada { get; set; }

    }
}
