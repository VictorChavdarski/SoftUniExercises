namespace BorderControl
{
    public class Citizen : IIdentifiable
    {
        public Citizen(string name,string age,string id)
        {
            this.Name = name;
            this.Age = age;
            this.Id = id;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
    }
}
