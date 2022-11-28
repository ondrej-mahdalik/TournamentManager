using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Entities;

namespace TournamentManager.Server.Seeds.MainSeeds;

public static class TeamSeeds
{
    public static readonly TeamEntity JohnsTeam = new(
            "John's team", "https://i.ytimg.com/vi/QQNL83fhWJU/maxresdefault.jpg", false)
    {
        Id = Guid.Parse("B7327A1C-DECE-4251-9467-F6A87D74E349"),
        LeaderId = UserSeeds.JohnDoe.Id,
    };

    public static readonly TeamEntity Team1 = new(
        "UTI", null, false)
    {
        Id = Guid.Parse("6C26A9A3-2A53-47F1-8AA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team2 = new(
        "TTTT", null, false)
    {
        Id = Guid.Parse("6B26A9A4-2A57-47F1-8AA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team3 = new(
        "Team3", null, false)
    {
        Id = Guid.Parse("5526B9A5-2A53-47F1-8BB9-87846AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };
    public static readonly TeamEntity Team4 = new(
        "Team4", null, false)
    {
        Id = Guid.Parse("5526A9A5-2A53-47F1-8BD9-87846AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };
    public static readonly TeamEntity Team5 = new(
        "Team5", null, false)
    {
        Id = Guid.Parse("5526A9A5-2A53-47F1-8DD9-87846AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };
    
    
    public static readonly TeamEntity JohnsTeam2 = new(
        "John's team2", "https://i.ytimg.com/vi/QQNL83fhWJU/maxresdefault.jpg", false)
    {
        Id = Guid.Parse("B7327A1C-DECE-4251-9467-F6A99974E349"),
        LeaderId = UserSeeds.JohnDoe.Id,
    };

    public static readonly TeamEntity Team6 = new(
        "UTI2", null, false)
    {
        Id = Guid.Parse("6C26A9A3-2A53-47F1-9BA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team7 = new(
        "TTTT2", null, false)
    {
        Id = Guid.Parse("6B26A9A4-2ABC-47F1-8AA9-87846AD2C3B6"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static readonly TeamEntity Team8 = new(
        "Team6", null, false)
    {
        Id = Guid.Parse("5526B9A5-2A53-47F1-8BB9-87846AD2C367"),
        LeaderId = UserSeeds.Admin.Id
    };
    public static readonly TeamEntity Team9 = new(
        "Team7", null, false)
    {
        Id = Guid.Parse("6726A9A5-2A53-47F1-8BD9-87846AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };
    public static readonly TeamEntity Team10 = new(
        "Team8", null, false)
    {
        Id = Guid.Parse("5526A9A5-2A53-47F1-8DD9-87526AD2C366"),
        LeaderId = UserSeeds.Admin.Id
    };

    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Teams.Add(JohnsTeam);
        dbContext.Teams.Add(Team1);
        dbContext.Teams.Add(Team2);
        dbContext.Teams.Add(Team3);
        dbContext.Teams.Add(Team4);
        dbContext.Teams.Add(Team5);
        dbContext.Teams.Add(JohnsTeam2);
        dbContext.Teams.Add(Team6);
        dbContext.Teams.Add(Team7);
        dbContext.Teams.Add(Team8);
        dbContext.Teams.Add(Team9);
        dbContext.Teams.Add(Team10);
    }
}