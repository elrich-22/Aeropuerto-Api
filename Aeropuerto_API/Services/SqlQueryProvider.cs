namespace AeropuertoAPI.Services
{
    public class SqlQueryProvider
    {
        private readonly string _sqlRootPath;

        public SqlQueryProvider(IHostEnvironment environment)
        {
            _sqlRootPath = Path.Combine(environment.ContentRootPath, "Sql");
        }

        public string GetQuery(string relativePath)
        {
            var fullPath = Path.Combine(_sqlRootPath, relativePath);

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"No se encontro el archivo SQL: {relativePath}", fullPath);
            }

            return File.ReadAllText(fullPath);
        }
    }
}
