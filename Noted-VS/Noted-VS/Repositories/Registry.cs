using Noted.Common;
using Noted.Security;

namespace Noted.Repositories
{
    public static class Registry
    {

        public static void AddInfrastructure(this IServiceCollection Services)
        {
            Services.AddScoped<EmailService>();
            Services.AddScoped<ColorsRepository>();
            Services.AddScoped<PhotosRepository>();
            Services.AddScoped<QuotesRepository>();
            Services.AddScoped<UsersRepository>();
            Services.AddScoped<HashService>();
            Services.AddScoped<RepositoriesRepository>();
            Services.AddScoped<TableRowsRepository>();
            Services.AddScoped<TableRowDatasRepository>();
            Services.AddScoped<TablesRepository>();
            Services.AddScoped<NotesRepository>();
            Services.AddScoped<ConfirmEmailRequestsRepository>();
            Services.AddScoped<ChecklistsRepository>();
            Services.AddScoped<ChecklistItemsRepository>();
        }
    }
}
