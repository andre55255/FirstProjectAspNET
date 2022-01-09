using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppWebMVC.Data;
using AppWebMVC.Models;
using AppWebMVC.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AppWebMVC.Services
{
    public class SellerService
    {
        private readonly AppWebMVCContext _context;

        public SellerService(AppWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);

            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == id);

        }

        public async Task RemoveAsync(int id)
        {
            Seller obj = await _context.Seller.FindAsync(id);   

            try
            {
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Este vendedor possui vendas associadas");
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(s => s.Id == seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
               await  _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
