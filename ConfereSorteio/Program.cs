// Usando a biblioteca Syncfusion.XlsIO
using ConfereSorteios;
using Syncfusion.XlsIO;
using System.Text.Json;

ConfereArrayNumerosSena.GeraEConfere();
return;

string JSON_PATH = "C:\\Users\\sergi\\source\\repos\\ConfereSorteio\\ConfereSorteio\\Mega-Sena.json";
string EXCEL_PATH = "C:\\Users\\sergi\\source\\repos\\ConfereSorteio\\ConfereSorteio\\Mega-Sena.xlsx";
if (!File.Exists(EXCEL_PATH))
    return;

OpenFile.FilePath = EXCEL_PATH;

IWorkbook? workbook = OpenFile.OpenExcelFile();

if (workbook == null)
    return;

string[] columnTitles = OpenFile.CreateColumnsArray(workbook);

object[,] rows = OpenFile.CreateRowsArray(workbook);

List<dynamic> objetos = OpenFile.TransformArraysToListOfRows(columnTitles, rows);

string json = OpenFile.CreateJSON(objetos);

//Deserializar o JSON para a lista de objetos
List<MegaSena>? megaSenas = JsonSerializer.Deserialize<List<MegaSena>>(json);

//Confere se há repetidos
ConfereSorteios<MegaSena> confereMegasena = new ConfereSorteios<MegaSena>();

if (!confereMegasena.ConfereRepetidos(megaSenas).Any())
    Console.WriteLine("Não há números repetidos");
else
    foreach (MegaSena repetido in confereMegasena.ConfereRepetidos(megaSenas))
    {
        Console.WriteLine($"Repetidos Concurso: {repetido.Concurso} - Bola1: {repetido.Bola1} - Bola2: {repetido.Bola2} - Bola3: {repetido.Bola3} - Bola4: {repetido.Bola4} - Bola5: {repetido.Bola5} - Bola6: {repetido.Bola6}");
    }

//Confere se meus números já foram sorteados
List<MegaSena> meusNumeros = new List<MegaSena>()
{
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "7", Bola2 = "12", Bola3 = "33", Bola4 = "47", Bola5 = "56", Bola6 = "60" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "14", Bola2 = "19", Bola3 = "22", Bola4 = "27", Bola5 = "39", Bola6 = "44" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "7", Bola2 = "11", Bola3 = "36", Bola4 = "39", Bola5 = "44", Bola6 = "47" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "8", Bola2 = "38", Bola3 = "44", Bola4 = "50", Bola5 = "56", Bola6 = "60" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "5", Bola2 = "13", Bola3 = "28", Bola4 = "39", Bola5 = "44", Bola6 = "46" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "5", Bola2 = "23", Bola3 = "33", Bola4 = "38", Bola5 = "40", Bola6 = "57" },
    new MegaSena() { Concurso = "", Sorteio = "", Bola1 = "2", Bola2 = "17", Bola3 = "23", Bola4 = "39", Bola5 = "54", Bola6 = "59" }
};

if (!confereMegasena.ConfereSorteio(megaSenas, meusNumeros).Any())
    Console.WriteLine("Não foi dessa vez!");
else
    foreach (MegaSena sorteado in confereMegasena.ConfereSorteio(megaSenas, meusNumeros))
    {
        Console.WriteLine($"Acerto Concurso: {sorteado.Concurso} - Bola1: {sorteado.Bola1} - Bola2: {sorteado.Bola2} - Bola3: {sorteado.Bola3} - Bola4: {sorteado.Bola4} - Bola5: {sorteado.Bola5} - Bola6: {sorteado.Bola6}");
    }

// Escreva a string JSON em um arquivo
if (!File.Exists(JSON_PATH))
    File.WriteAllText(JSON_PATH, json);
