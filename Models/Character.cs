namespace RickAndMorty
{
    public class Character
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string Species { get; set; }
        public required string Gender { get; set; }
        public required Origin Origin { get; set; }
        public required Location Location { get; set; }
    }

    // Models/OriginDto.cs
    public class Origin
    {
        public required string Name { get; set; }
    }

    // Models/LocationDto.cs
    public class Location
    {
        public required string Name { get; set; }
    }

}