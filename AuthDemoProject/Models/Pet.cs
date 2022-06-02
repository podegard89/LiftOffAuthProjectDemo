namespace AuthDemoProject.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        // how have we set up one-to-many relationships in the past?
        // in TechJobs, we described the relationship between Jobs and Employers by
        // giving the Job model class an Employer property and an EmployerId property
        // Thus, setting up a relationship of one Employer to many Jobs
        // That means we need to give this pet class a user property and a userId property!
        // Then, there will be a relationship setup of one User to many Pets

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public Pet() { }

        public Pet(string name, string species)
        {
            Name = name;
            Species = species;
        }
    }
}
