using Syncfusion.XlsIO;
using System.Dynamic;
using System.Text.Json;

namespace ConfereSorteios
{
    internal static class OpenFile
    {
        public static string? FilePath { get; set; }

        public static IWorkbook? OpenExcelFile()
        {
            if (!File.Exists(FilePath))
                return null;

            // Crie uma instância do aplicativo Excel
            ExcelEngine excelEngine = new ExcelEngine();

            // Abra o arquivo Excel
            IApplication application = excelEngine.Excel;
            FileStream sampleFile = new FileStream(FilePath, FileMode.Open);
            IWorkbook workbook = application.Workbooks.Open(sampleFile);

            return workbook;
        }

        public static string[] CreateColumnsArray(IWorkbook workbook)
        {
            // Acesse a primeira planilha do workbook
            IWorksheet worksheet = workbook.Worksheets[0];

            // Obtenha o número total de linhas e colunas usadas na planilha
            int totalRows = worksheet.UsedRange.LastRow;
            int totalColumns = worksheet.UsedRange.LastColumn;

            // Crie um array para armazenar os títulos das colunas
            string[] columnTitles = new string[totalColumns];

            // Preencha o array dos títulos das colunas com os valores da primeira linha da planilha
            for (int i = 1; i <= totalColumns; i++)
            {
                columnTitles[i - 1] = worksheet[1, i].Value.ToString();
            }

            return columnTitles;
        }

        public static object[,] CreateRowsArray(IWorkbook workbook)
        {
            // Acesse a primeira planilha do workbook
            IWorksheet worksheet = workbook.Worksheets[0];

            // Obtenha o número total de linhas e colunas usadas na planilha
            int totalRows = worksheet.UsedRange.LastRow;
            int totalColumns = worksheet.UsedRange.LastColumn;

            // Crie um array para armazenar as linhas da planilha
            object[,] rows = new object[totalRows - 1, totalColumns];

            // Preencha o array das linhas com os valores das demais linhas da planilha
            for (int i = 2; i <= totalRows; i++)
            {
                for (int j = 1; j <= totalColumns; j++)
                {
                    rows[i - 2, j - 1] = worksheet[i, j].Value;
                }
            }

            return rows;
        }

        public static List<dynamic> TransformArraysToListOfRows(string[] columnTitles, object[,] rows)
        {
            // Crie uma lista para armazenar os objetos criados
            List<dynamic> objetos = new List<dynamic>();

            // Itere sobre o array das linhas
            for (int i = 0; i < rows.GetLength(0); i++)
            {
                // Crie um objeto dinâmico usando a classe ExpandoObject
                dynamic objeto = new ExpandoObject();

                IDictionary<string, object> dicionario = objeto;

                // Atribua os valores das células às propriedades do objeto usando os títulos das colunas
                for (int j = 0; j < columnTitles.Length; j++)
                {
                    dicionario[columnTitles[j]] = rows[i, j];
                }

                // Adicione o objeto à lista
                objetos.Add(objeto);
            }

            return objetos;
        }

        public static string CreateJSON(List<dynamic> objetos)
        {
            return JsonSerializer.Serialize(objetos);
        }

        public static void CloseExcelWorkbook(IWorkbook workbook)
        {
            workbook.Close();
        }
    }
}
