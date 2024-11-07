using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace SalesWebMvc.Models;

public class Seller
{
        public int Id { get; set; }
    [Required(ErrorMessage = "{0} required")]
    [StringLength(50,MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]
        public string Name { get; set; }
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Enter a valid Email ")]
    [Required(ErrorMessage = "{0} required")]
        public string Email { get; set; }
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0} required")]
        public DateTime BirthDate { get; set; }
    [Display(Name = "Base Salary")]
    [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0} required")]
    [Range(100.0,50000.0, ErrorMessage = "{0} must be between {1} and {2}")]
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();


    public Seller()
    {
    }

    public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        BaseSalary = baseSalary;
        Department = department;
    }

    public void AddSales(SalesRecord sr)
    {
        Sales.Add(sr);
    }

    public void RemoveSales(SalesRecord sr)
    {
        Sales.Remove(sr);
    }

    public double TotalSales(DateTime initial, DateTime final)
    {
        return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
    }
}