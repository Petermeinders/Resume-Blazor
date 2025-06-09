public class Person
{
    public string Name { get; set; }
    public string BirthYear { get; set; }
    public string EyeColor { get; set; }
    public string Gender { get; set; }
    public string HairColor { get; set; }
    public int Height { get; set; }
    public int Mass { get; set; }
    public string SkinColor { get; set; }
    public string Homeworld { get; set; }
    public List<string> Films { get; set; }
    public List<string> Species { get; set; }
    public List<string> Starships { get; set; }
    public List<string> Vehicles { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}


public class Vehicle
{
    public string Name { get; set; }
    public string Model { get; set; }
    public string VehicleClass { get; set; }
    public string Manufacturer { get; set; }
    public string Length { get; set; }
    public string CostInCredits { get; set; }
    public string Crew { get; set; }
    public string Passengers { get; set; }
    public string MaxAtmospheringSpeed { get; set; }
    public string CargoCapacity { get; set; }
    public string Consumables { get; set; }
    public List<string> Films { get; set; }
    public List<string> Pilots { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}

public class Planet
{
    public string Name { get; set; }
    public string Diameter { get; set; }
    public string RotationPeriod { get; set; }
    public string OrbitalPeriod { get; set; }
    public string Gravity { get; set; }
    public string Population { get; set; }
    public string Climate { get; set; }
    public string Terrain { get; set; }
    public string SurfaceWater { get; set; }
    public List<string> Residents { get; set; }
    public List<string> Films { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}


public class Species
{
    public string Name { get; set; }
    public string Classification { get; set; }
    public string Designation { get; set; }
    public string AverageHeight { get; set; }
    public string AverageLifespan { get; set; }
    public string EyeColors { get; set; }
    public string HairColors { get; set; }
    public string SkinColors { get; set; }
    public string Language { get; set; }
    public string Homeworld { get; set; }
    public List<string> People { get; set; }
    public List<string> Films { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}


public class Starship
{
    public string Name { get; set; }
    public string Model { get; set; }
    public string StarshipClass { get; set; }
    public string Manufacturer { get; set; }
    public string CostInCredits { get; set; }
    public string Length { get; set; }
    public string Crew { get; set; }
    public string Passengers { get; set; }
    public string MaxAtmospheringSpeed { get; set; }
    public string HyperdriveRating { get; set; }
    public string MGLT { get; set; }
    public string CargoCapacity { get; set; }
    public string Consumables { get; set; }
    public List<string> Films { get; set; }
    public List<string> Pilots { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}


public class Film
{
    public string Title { get; set; }
    public int EpisodeId { get; set; }
    public string OpeningCrawl { get; set; }
    public string Director { get; set; }
    public string Producer { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<string> Species { get; set; }
    public List<string> Starships { get; set; }
    public List<string> Vehicles { get; set; }
    public List<string> Characters { get; set; }
    public List<string> Planets { get; set; }
    public string Url { get; set; }
    public DateTime Created { get; set; }
    public DateTime Edited { get; set; }
}
