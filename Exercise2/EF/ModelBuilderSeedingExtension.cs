using Exercise2.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Exercise2.EF
{
    public static class ModelBuilderSeedingExtension 
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = 1,
                    Name = "Mentor"
                },
                new Position
                {
                    Id= 2,
                    Name = "OM"
                },
                new Position
                {
                    Id = 3,
                    Name = "Developer"
                },
                new Position
                {
                    Id = 4,
                    Name = "Tester"
                }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Nguyen Quoc Au",
                    PositionID = 1
                },
                new Employee
                {
                    Id = 2,
                    Name = "Nguyen Thanh Hai",
                    PositionID = 2
                },
                new Employee
                {
                    Id = 3,
                    Name = "Huynh Tan Thinh",
                    PositionID = 3
                },
                new Employee
                {
                    Id = 4,
                    Name = "Vu Ngoc Thach",
                    PositionID = 4
                }
            );
        }
    }
}
