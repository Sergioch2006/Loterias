namespace ConfereSorteios
{
    //crie uma classe genérica para conferir sorteio e conferir repetidos

    public class ConfereSorteios<T> where T : class, ILoteria
    {
        public List<T> ConfereRepetidos(List<T> listOfLoteria)
        {
            //Verifique se Bola1, Bola2, Bola3, Bola4, Bola5 e Bola6 estão repetidos em linhas diferentes
            return listOfLoteria.Where(x => listOfLoteria.Count(y => y.Bola1 == x.Bola1 && y.Bola2 == x.Bola2 && y.Bola3 == x.Bola3 && y.Bola4 == x.Bola4 && y.Bola5 == x.Bola5 && y.Bola6 == x.Bola6) > 1).ToList();
        }

        public List<T> ConfereSorteio(List<T> listOfLoteria, List<T> myNumbers)
        {
            return listOfLoteria.Where(x => myNumbers.Count(y => y.Bola1 == x.Bola1 && y.Bola2 == x.Bola2 && y.Bola3 == x.Bola3 && y.Bola4 == x.Bola4 && y.Bola5 == x.Bola5 && y.Bola6 == x.Bola6) > 1).ToList();
        }
    }

}
