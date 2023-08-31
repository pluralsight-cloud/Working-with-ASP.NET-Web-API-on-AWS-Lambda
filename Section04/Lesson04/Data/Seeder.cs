using Bogus;

namespace WebApiRdsApp.Data
{
    public static class Seeder
    {
        public static void Seed(this SchoolContext schoolContext)
        {
           if(!schoolContext.Students.Any())
           {
            var personFaker = new Faker<Student>()
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.DateOfBirth, f => f.Date.Past(10));
        
            schoolContext.AddRange(personFaker.Generate(10));
            schoolContext.SaveChanges();
           }
        }
    }
}