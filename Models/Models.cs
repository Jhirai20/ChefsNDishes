using System;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ChefsNDishes.Models
{
    public class MyContext :DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }

    public class Chef
    {
        [Key]
        public int idchefs { get; set; }

        [Required]
        public string name { get; set; }
        [Required]
        [CheckAge]
        public DateTime dob { get; set; }
        public List<Dish> CreatedDishes { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;

        public class CheckAge : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                int age = CalculateAge((DateTime)value);
                if (age < 18)
                {
                    return new ValidationResult("Chef must be 18 or older!");
                }
                return ValidationResult.Success;
            }
        }
        public static int CalculateAge(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Year - dob.Year;
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            {
                age--;
            }
            return age;
        }
    }
    public class Dish
    {
        [Key]
        public int dishid { get; set; }
        [Required]
        public string name{ get; set; }
        [Required]
        public int tastiness{ get; set; }
        [Required]
        public int calories{ get; set; }
        [Required]
        public string description { get; set; }
        public int ChefId { get; set; }
        public Chef Creator { get; set; }
        public DateTime created_at { get; set; } = DateTime.Now;
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}