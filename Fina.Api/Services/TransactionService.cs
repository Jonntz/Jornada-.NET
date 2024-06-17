using Fina.Api.Data;
using Fina.Common.Enums;
using Fina.Common.Models;
using Fina.Common.Requests.Transactions;
using Fina.Common.Responses;
using Fina.Common.Services;
using Fina.Common.Shared;
using Microsoft.EntityFrameworkCore;


namespace Fina.Api.Services
{
    public class TransactionService(AppDbContext context) : ITransactionService
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            if(request is {Type: ETransactionType.Withdraw, Amount: >= 0}){
                request.Amount *= -1;
            }

            try{
                var transaction = new Transaction{
                    UserId = request.UserId,
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrReceivedAt = request.PaidOrreceivedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Transação criada com sucesso!");
            } 
            catch {
                return new Response<Transaction?>(null, 500, "Não foi possivel criar uma transação!");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null)
                {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }


                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível excluir sua transação");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetByIdTransactionRequest request)
        {
            try{
                var transactions = await context
                    .Transactions
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transactions is null 
                    ? new Response<Transaction?>(null, 404, "Categoria não encontrada!") 
                    : new Response<Transaction?>(transactions);

            } catch {
                return new Response<Transaction?>(null, 500, "Não foi possivel recuperar categoria!");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetByPeriodTransactionRequest request)
        {
            try {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            } catch {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível determinar a data de início ou término");
            }


            try
            {
               var query = context
                    .Transactions
                    .AsNoTracking()
                    .Where(x => x.PaidOrReceivedAt >= request.StartDate &&
                        x.PaidOrReceivedAt <= request.EndDate &&
                        x.UserId == request.UserId)
                    .OrderBy(x => x.PaidOrReceivedAt);
                
                var transactions = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(
                    transactions,
                    count,
                    request.PageNumber,
                    request.PageSize);
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Não foi possível obter as transações");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 }) 
                request.Amount *= -1;
            
            try
            {
                var transaction = await context
                    .Transactions
                    .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction is null) {
                    return new Response<Transaction?>(null, 404, "Transação não encontrada");
                }

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.PaidOrReceivedAt = request.PaidOrreceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction);
            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Não foi possível atualizar sua transação");
            }
        }
    }
}