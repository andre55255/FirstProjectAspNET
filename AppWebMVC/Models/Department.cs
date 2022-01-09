using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace AppWebMVC.Models {
    public class Department {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do departamento deve conter de 3 a 100 caracteres")]
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() { }
        public Department(int id, string name) {
            Id = id;
            Name = name;
        }

        public void AddSeller(Seller seller) {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
