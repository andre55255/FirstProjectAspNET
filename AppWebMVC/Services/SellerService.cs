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

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);

            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Seller.Include(s => s.Department).FirstOrDefault(s => s.Id == id);

        }

        public void Remove(int id)
        {
            Seller obj = _context.Seller.Find(id);   
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller seller)
        {
            if (!(_context.Seller.Any(s => s.Id == seller.Id)))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
