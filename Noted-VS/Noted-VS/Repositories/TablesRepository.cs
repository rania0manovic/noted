using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noted.Data;
using Noted.Entities;
using Noted.ViewModels;

namespace Noted.Repositories
{
    public class TablesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TablesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task EditColorAsync(int tableId, string color, CancellationToken cancellationToken = default)
        {
            var table = await _dbContext.Tables.FindAsync(new object?[] { tableId }, cancellationToken: cancellationToken);
            if (table != null)
            {
                table.Color = "#" + color;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async void EditName(int tableId, string name, CancellationToken cancellationToken = default)
        {
            var table = await _dbContext.Tables.FindAsync(new object?[] { tableId }, cancellationToken: cancellationToken);
            if (table != null)
            {
                table.Name = name;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Table> AddAsync(TableAddVM tableVM, CancellationToken cancellationToken = default)
        {
            var table = new Table()
            {
                Name = tableVM.Name,
                Description = "",
                RepositoryId = tableVM.RepositoryId,
                Color = "#ededed"
            };
            await _dbContext.Tables.AddAsync(table, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return table;
        }

        public async Task<Table> AddInitialAsync(TableAddVM tableVM, CancellationToken cancellationToken = default)
        {
            var table = new Table()
            {
                Name = tableVM.Name,
                Description = "",
                RepositoryId = tableVM.RepositoryId,
                NumberOfColumns = tableVM.NumberOfColumns

            };
            await _dbContext.Tables.AddAsync(table, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var headerRow = new TableRow()
            {
                TableId = table.Id,
            };
            await _dbContext.TableRows.AddAsync(headerRow, cancellationToken);
            var bodyRow = new TableRow()
            {
                TableId = table.Id,
            };
            await _dbContext.TableRows.AddRangeAsync(headerRow, bodyRow);
            await _dbContext.SaveChangesAsync(cancellationToken);
            for (int i = 0; i < tableVM.NumberOfColumns; i++)
            {
                var rowData = new TableRowData()
                {
                    TableRowId = headerRow.Id,
                    Data = "Column Name"
                };
                await _dbContext.TableRowDatas.AddAsync(rowData, cancellationToken);
            }
            for (int i = 0; i < tableVM.NumberOfColumns; i++)
            {
                var rowData = new TableRowData()
                {
                    TableRowId = bodyRow.Id,
                    Data = ""
                };
                await _dbContext.TableRowDatas.AddAsync(rowData, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return table;
        }

        public async Task<List<TableGetVM>> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var tables = await _dbContext.Tables.Where(t => t.RepositoryId == id).ToListAsync(cancellationToken: cancellationToken);
            var list = new List<TableGetVM>();
            foreach (var table in tables)
            {
                list.Add(new TableGetVM()
                {
                    Id = table.Id,
                    Name = table.Name,
                });
            }
            return list;
        }

        public async Task<List<TableGetFullDataVM>> GetFullAsync(int id, CancellationToken cancellationToken = default)
        {
            var tables = await _dbContext.Tables.Include(t => t.TableRows).ThenInclude(td => td.TableRowDatas)
                .Where(t => t.Id == id).Select(t => new TableGetFullDataVM()
                {
                    Description = t.Description,
                    Id = t.Id,
                    Name = t.Name,
                    Repository = t.Repository,
                    RepositoryId = t.RepositoryId,
                    TableHeaderRow = t.TableRows.First(),
                    TableBodyRows = t.TableRows.Skip(1).ToList(),
                    NumberOfColumns = t.NumberOfColumns,
                    Color = t.Color

                }).ToListAsync(cancellationToken: cancellationToken);
            return tables;
        }

        public async Task DeleteAsync(int tableId, CancellationToken cancellationToken = default)
        {
            var table = await _dbContext.Tables.FindAsync(new object?[] { tableId }, cancellationToken: cancellationToken);
            if (table != null)
            {
                var rows = await _dbContext.TableRows.Where(r => r.TableId == tableId).ToListAsync(cancellationToken: cancellationToken);
                foreach (var row in rows)
                {
                    var data = await _dbContext.TableRowDatas.Where(td => td.TableRowId == row.Id).ToListAsync(cancellationToken: cancellationToken);
                    foreach (var item in data)
                    {
                        _dbContext.TableRowDatas.Remove(item);
                    }
                    _dbContext.TableRows.Remove(row);
                }
                _dbContext.Tables.Remove(table);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        }

    }
}
