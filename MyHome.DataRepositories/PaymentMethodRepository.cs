﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyHome.DataClasses;
using MyHome.Persistence;

namespace MyHome.DataRepository
{
    public class PaymentMethodRepository
    {
        private readonly AccountingDataContext _context;

        public PaymentMethodRepository(AccountingDataContext context)
        {
            _context = context;
        }

        public PaymentMethod GetById(int id)
        {
            return _context.PaymentMethods.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public PaymentMethod GetByName(string name)
        {
            return _context.PaymentMethods.AsNoTracking().FirstOrDefault(p => p.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<PaymentMethod> GetAll()
        {
            return _context.PaymentMethods.AsNoTracking().ToList();
        }

        public void Save(PaymentMethod paymentMethod)
        {
            if (paymentMethod.Id != 0)
            {
                Update(paymentMethod);
            }
            else
            {
                Create(paymentMethod);
            }
        }

        public void Update(PaymentMethod paymentMethod)
        {
            var dbCat = _context.PaymentMethods.FirstOrDefault(ec => ec.Id == paymentMethod.Id);
            if (dbCat != null)
            {
                dbCat.Name = paymentMethod.Name;
            }
            _context.SaveChanges();
        }

        public void Create(PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
            {
                return;
            }
            _context.PaymentMethods.Add(paymentMethod);
            _context.SaveChanges();
        }

        public void RemoveByName(string name)
        {
            var existing = _context.PaymentMethods.FirstOrDefault(p => p.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));
            if (existing == null) return;
            _context.PaymentMethods.Remove(existing);
        }
    }
}
