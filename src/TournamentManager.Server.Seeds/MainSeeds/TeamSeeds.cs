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
        LeaderId = UserSeeds.Admin.Id,
    };
    public static readonly TeamEntity CardWarriors = new(
        "Card warriors", "https://static.wikia.nocookie.net/aliceinwonderland/images/7/7d/March-of-the-cards-3.jpg/revision/latest?cb=20100315140119", false)
    {
        Id = Guid.Parse("F0301306-2679-4995-9B89-4EBBD561F6CC"),
        LeaderId = UserSeeds.AlenaVaf.Id
    };

    public static readonly TeamEntity GalacticTroopers = new(
        "Galactic troopers", "https://m.media-amazon.com/images/I/81YNIGDPx7L._AC_SL1500_.jpg", false)
    {
        Id = Guid.Parse("AB340192-238D-4180-B14D-F5743ECE0B49"),
        LeaderId = UserSeeds.DarthVader.Id
    };
    
    public static void Seed(TournamentManagerDbContext dbContext)
    {
        dbContext.Teams.Add(JohnsTeam);
        dbContext.Teams.Add(Team1);
        dbContext.Teams.Add(Team2);
        dbContext.Teams.Add(Team3);
        dbContext.Teams.Add(Team4);
        dbContext.Teams.Add(Team5);
        dbContext.Teams.Add(CardWarriors);
        dbContext.Teams.Add(GalacticTroopers);
    }
}